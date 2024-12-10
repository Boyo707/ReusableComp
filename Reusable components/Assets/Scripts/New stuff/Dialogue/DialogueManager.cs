using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    [Header("UI Refrences")]

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Image speakerImage;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerName;
    [SerializeField] private Timer dialogueBlinker;

    [Header("AudioSource")]
    [SerializeField] private AudioSource audioSource;

    private AudioClip audioClip;

    private static DialogueManager instance;

    public bool DialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;

    private Coroutine DrawLineCourotine;

    private DialogueDataObject dialogueData;

    private string currentDialogue;

    private int currentDialogueIndex;

    private int drawnCharacterAmount;

    private int audioPerCharacter;

    private float typingSpeed;

    private bool writeFullDialogue = false;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("FOUND MORE THEN ONE DIALOGUE MANAGER INSIDE THE SCENE");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        DialogueIsPlaying = false;
        currentDialogueIndex = 0;
        dialoguePanel.SetActive(false);
        typingSpeed = 0;
        drawnCharacterAmount = 0;
        dialogueBlinker.StopTimer();
    }

    private void Update()
    {
        if (!DialogueIsPlaying)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && canContinueToNextLine)
        {
            
            currentDialogueIndex++;
            ContinueDialogue();
        }
        else if (Input.GetKeyDown(KeyCode.F) && canContinueToNextLine == false)
        {
            dialogueText.text = currentDialogue;
            writeFullDialogue = true;
        }
    }

    public void EnterDialogue(DialogueDataObject data)
    {
        dialogueData = data;
        DialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        currentDialogueIndex = 0;

        if (dialogueData.Options[currentDialogueIndex] != null)
        {
            typingSpeed = dialogueData.Options[currentDialogueIndex].TextSpeed;
            audioClip = dialogueData.Options[currentDialogueIndex].AudioClip;
            currentDialogue = dialogueData.Options[currentDialogueIndex].DialogueText;
            audioPerCharacter = dialogueData.Options[currentDialogueIndex].AudioPerCharacter;
            speakerImage.sprite = dialogueData.Options[currentDialogueIndex].FaceSprite;
            speakerName.text = dialogueData.Options[currentDialogueIndex].CharacterData.characterName;

            StartCoroutine(DrawLine(currentDialogue));
        }
        else if(dialogueData.Options[0] == null)
        {
            Debug.LogError("THERE IS NO DIALOGUE FOUND");
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void ContinueDialogue()
    {
        if (currentDialogueIndex <= dialogueData.Options.Length - 1)
        {
            Debug.Log(currentDialogueIndex + "   :   " + dialogueData.Options.Length );
            if (DrawLineCourotine != null)
            {
                StopCoroutine(DrawLineCourotine);
            }
            typingSpeed = dialogueData.Options[currentDialogueIndex].TextSpeed;
            audioClip = dialogueData.Options[currentDialogueIndex].AudioClip;
            currentDialogue = dialogueData.Options[currentDialogueIndex].DialogueText;
            audioPerCharacter = dialogueData.Options[currentDialogueIndex].AudioPerCharacter;
            speakerImage.sprite = dialogueData.Options[currentDialogueIndex].FaceSprite;
            speakerName.text = dialogueData.Options[currentDialogueIndex].CharacterData.characterName;

            DrawLineCourotine = StartCoroutine(DrawLine(currentDialogue));

            
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void ExitDialogueMode()
    {
        DialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        audioClip = null;
        currentDialogue = "";
        dialogueText.text = "";
        audioPerCharacter = 0;
        currentDialogueIndex = 0;
        speakerImage.sprite = null;
        speakerName.text = "";
    }

    private IEnumerator DrawLine(string line)
    {
        dialogueBlinker.StopTimer();
        dialogueText.text = "";

        canContinueToNextLine = false;

        drawnCharacterAmount = 0;

        foreach (char letter in line.ToCharArray())
        {
            if(writeFullDialogue == true)
            {
                break;
            }

            dialogueText.text += letter;

            drawnCharacterAmount++;

            if (drawnCharacterAmount != 0 && audioPerCharacter != 0)
            {
                if (drawnCharacterAmount % audioPerCharacter == 0)
                {
                    audioSource.PlayOneShot(audioClip);
                }
            }
            yield return new WaitForSeconds(1 / typingSpeed);
        }

        dialogueBlinker.StartTimer();
        canContinueToNextLine = true;
        writeFullDialogue = false;
    }
}
