using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using System;
using UnityEditor.U2D.Animation;
using System.Net.NetworkInformation;

public enum DialogueType
{
    Default,
    OptionSelection,
    Transition
}

public enum DialogueState
{
    Writing,
    Loaded,
    Selecting
}

public class DialougeBox : MonoBehaviour
{
    [Header("UI Refrences")]
    //[SerializeField] private GameObject _DialogueBox;
    [SerializeField] private TextMeshProUGUI _dialogueBoxText;
    [SerializeField] private TextMeshProUGUI _characterNameText;
    [SerializeField] private Image _characterImage;

    [Header("Dialogue Options")]

    [SerializeField] private string _DialogueName;

    [SerializeField][Range(0, 0.5f)] private float _textSpeed; 

    
    [SerializeField] private List<DialogueOptions> _dialogueOptions = new List<DialogueOptions>();

    private int _currentDialogueIndex = 0;

    private DialogueState _state = DialogueState.Loaded;

   

    private void Awake()
    {
        _characterImage.sprite = _dialogueOptions[_currentDialogueIndex].CharacterImage;
         _dialogueBoxText.text = _dialogueOptions[_currentDialogueIndex].Dialogue;
         _characterNameText.text = _dialogueOptions[_currentDialogueIndex].CurrentCharacterName;
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
                    NextDialogue();

                if (Input.mouseScrollDelta.y < 0)
                        NextDialogue();
                break;

            case DialogueState.Selecting:
                if (Input.GetMouseButtonDown(0))
                    NextDialogue();
                    //select button -- Delete "NextDialogue();"
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

    private IEnumerator DrawText()
    {
        
        foreach (char c in _dialogueOptions[_currentDialogueIndex].Dialogue.ToCharArray())
        {
            _dialogueBoxText.text += c;
            yield return new WaitForSeconds(_textSpeed);
            _state = DialogueState.Writing;
        }
    }

    private void StartDialogue()
    {
        _currentDialogueIndex = 0;
        StartCoroutine(DrawText());
    }
    
    private void NextDialogue()
    {
        StopAllCoroutines();
        _dialogueBoxText.text = string.Empty;
        _currentDialogueIndex++;
        _characterImage.sprite = _dialogueOptions[_currentDialogueIndex].CharacterImage;
        _characterNameText.text = _dialogueOptions[_currentDialogueIndex].CurrentCharacterName;
        StartCoroutine(DrawText());
    }

    private void SkipDialogue()
    {
        StopAllCoroutines();
        _dialogueBoxText.text = string.Empty;
        _currentDialogueIndex++;
        _characterImage.sprite = _dialogueOptions[_currentDialogueIndex].CharacterImage;
        _characterNameText.text = _dialogueOptions[_currentDialogueIndex].CurrentCharacterName;
        StartCoroutine(DrawText());
    }

    private void LoadFullDialogue()
    {
        StopAllCoroutines();
        _state = DialogueState.Loaded;
        _dialogueBoxText.text = _dialogueOptions[_currentDialogueIndex].Dialogue;
    }

    
}

[Serializable]
public class DialogueOptions
{
    [SerializeField] private DialogueType _dialogueType = DialogueType.Default;

    [SerializeField] private CharacterData _characterData;

    [SerializeField] private Image _newBackground;

    //character Expression


    [SerializeField]
    [TextArea(1, 2)]
    private string _dialogue;

    public DialogueType TypeOfDialogue => _dialogueType;
    public string CurrentCharacterName => _characterData._characterName;
    public Sprite CharacterImage => _characterData._characterDialogueImage;
    public Image Background => _newBackground;
    public string Dialogue => _dialogue;

    
}
