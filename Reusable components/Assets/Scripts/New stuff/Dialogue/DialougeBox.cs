using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using System;
using UnityEditor.U2D.Animation;
using System.Net.NetworkInformation;
using UnityEditor.Experimental.GraphView;
using System.Runtime.CompilerServices;

public enum DialogueState
{
    Writing,
    Loaded,
    Selecting
}

public class DialougeBox : MonoBehaviour
{
    [Header("UI Refrences")]
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private Image _characterImage;
    [SerializeField] private TextMeshProUGUI _dialogueBoxText;
    [SerializeField] private TextMeshProUGUI _characterNameText;

    [SerializeField] private AudioSource _audioSource;
    private AudioClip _audioClip;

    private int _audioPerCharacter;
    private int _writtenCharacter;


    [Header("Dialogue Options")]
    [SerializeField] private bool _startOnAwake = false;

    private float _textSpeed; 

    [SerializeField] private List <DialogueDataObject> _dialogue = new List<DialogueDataObject>();

    [SerializeField] private UnityEvent _onExit;

    //Indexs for the dialogue lists
    private int _currentDialogueSceneIndex = 0;
    private int _currentDialogueIndex = 0;

    private DialogueState _state = DialogueState.Loaded;


    private string currentDialogue;
    private Sprite currentFaceSprite;

   /*BUG**
    * There is a bug where the audio keeps playing after the dialogue has finished.
    * Maybe there is a function to stop the incoming or current audio.
    * */

    private void Awake()
    {
        _dialogueBox.SetActive(false);
        if (_startOnAwake)
        {
            _currentDialogueIndex = 0;
            _currentDialogueSceneIndex = 0;

            currentFaceSprite = _dialogue[_currentDialogueSceneIndex].Options[_currentDialogueIndex].FaceSprite;
            currentDialogue = _dialogue[_currentDialogueSceneIndex].Options[_currentDialogueIndex].DialogueText;

            _textSpeed = _dialogue[_currentDialogueSceneIndex].Options[_currentDialogueIndex].TextSpeed;
            _audioPerCharacter = _dialogue[_currentDialogueSceneIndex].Options[_currentDialogueIndex].AudioPerCharacter;

            _characterNameText.text = _dialogue[_currentDialogueSceneIndex].Options[_currentDialogueIndex].CharacterData.characterName;
            if (_dialogue[_currentDialogueSceneIndex].Options[_currentDialogueIndex].AudioClip != null)
            {
                _audioClip = _dialogue[_currentDialogueSceneIndex].Options[_currentDialogueIndex].AudioClip;
                _audioSource.clip = _audioClip;
            }

            StartDialogue();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _dialogueBoxText.text = string.Empty;
        StartDialogue();

    }

    // Update is called once per frame
    void Update()
    {
        currentDialogue = _dialogue[_currentDialogueSceneIndex].Options[_currentDialogueIndex].DialogueText;
        currentFaceSprite = currentFaceSprite = _dialogue[_currentDialogueSceneIndex].Options[_currentDialogueIndex].FaceSprite;
        _characterImage.sprite = currentFaceSprite;
        

        #region Dialouge Inputs
        switch (_state)
        {
            case DialogueState.Writing:

                if (Input.GetMouseButtonDown(0))
                    LoadFullDialogue();

                if (Input.mouseScrollDelta.y < 0)
                    LoadFullDialogue();

                break;
            

            case DialogueState.Loaded:

                if (Input.GetMouseButtonDown(0))
                {
                    
                    NextDialogue();
                }
                    

                if (Input.mouseScrollDelta.y < 0)
                        NextDialogue();
                break;
                

            case DialogueState.Selecting:
                if (Input.GetMouseButtonDown(0))
                    NextDialogue();
                    //Needs to be select button -- Delete "NextDialogue();"
                break;


        }

        if (Input.GetMouseButtonDown(1))
        {
            //ShowMenu
        }
        if (Input.GetMouseButtonDown(2))
        {
            //removeUI
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            //Showhistory
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SkipDialogue();
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            //SkipDialouge
            //LoadAllText
            //NextDialoufe
        }
        #endregion
    }

    public void StartDialogue()
    {
        _dialogueBox.SetActive(true);
        _currentDialogueIndex = 0;
        _currentDialogueSceneIndex= 0;
        
        StartCoroutine(DrawText());
    }

    private IEnumerator DrawText()
    {

        foreach (char c in currentDialogue.ToCharArray())
        {
            _dialogueBoxText.text += c;
            if (c != ' ')
            {
                _writtenCharacter++;
                if (_writtenCharacter % _audioPerCharacter == 0)
                {
                    _audioSource.PlayOneShot(_audioClip);
                }
            }
            //_audioSource.Stop();
            
            yield return new WaitForSeconds(1f / _textSpeed);
            _state = DialogueState.Writing;
        }
        
    }
    
    private void NextDialogue()
    {
        StopAllCoroutines();

        _dialogueBoxText.text = string.Empty;

        if (_currentDialogueIndex == _dialogue[_currentDialogueSceneIndex].Options.Length - 1)
        {
            if (_currentDialogueSceneIndex == _dialogue.Count - 1)
            {
                StopDialogue();
                return;
            }
            NextDialogueScene();
            return;
        }



        _currentDialogueIndex++;

        _characterImage.sprite = _dialogue[_currentDialogueSceneIndex].Options[_currentDialogueIndex].FaceSprite;
        _characterNameText.text = _dialogue[_currentDialogueSceneIndex].Options[_currentDialogueIndex].CharacterData.characterName;

        StartCoroutine(DrawText());
    }

    private void SkipDialogue()
    {
        StopAllCoroutines();

        _dialogueBoxText.text = string.Empty;

        _currentDialogueIndex++;

        _characterImage.sprite = _dialogue[_currentDialogueSceneIndex].Options[_currentDialogueIndex].FaceSprite;
        _characterNameText.text = _dialogue[_currentDialogueSceneIndex].Options[_currentDialogueIndex].CharacterData.characterName;

        StartCoroutine(DrawText());
    }

    private void LoadFullDialogue()
    {
        StopAllCoroutines();
        _state = DialogueState.Loaded;

        _dialogueBoxText.text = _dialogue[_currentDialogueSceneIndex].Options[_currentDialogueIndex].DialogueText;
    }

    private void NextDialogueScene()
    {
        _currentDialogueSceneIndex++;

        _currentDialogueIndex = 0;

        StartCoroutine(DrawText());
    }

    private void StopDialogue()
    {
        _dialogueBox.SetActive(false);
        _currentDialogueIndex = 0;
        _currentDialogueSceneIndex = 0;
        _onExit.Invoke();
        Debug.Log("Stopped");
    }
    
}


