using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using System;
using UnityEditor.U2D.Animation;

public enum DialogueType
{
    Default,
    OptionSelection,
    Transition
}

public class DialougeBox : MonoBehaviour
{
    [Header("UI Refrences")]
    //[SerializeField] private GameObject _DialogueBox;
    [SerializeField] private TextMeshProUGUI _dialogueBoxText;
    [SerializeField] private TextMeshProUGUI _characterNameText;

    //[SerializeField] private ScriptableObjectCharacterDataStuff[] _characters; 

    //[SerializeField] private DialogueOptions[] _dialogueOptions;
    [SerializeField] private List<DialogueOptions> _dialogueOptions = new List<DialogueOptions>();

    private int _currentDialogueIndex = 0;

   

    private void Awake()
    {
        
         _dialogueBoxText.text = _dialogueOptions[_currentDialogueIndex].Dialogue;
         _characterNameText.text = _dialogueOptions[_currentDialogueIndex].CurrentCharacterName;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region Dialouge Inputs
        if (Input.GetMouseButtonDown(0))
        {
            NextDialogue();
            //NextDialouge
            //OR
            //LoadAllText
            //OR
            //SelectButton
        }
        if (Input.GetMouseButtonDown(1))
        {
            //ShowMenu
        }
        if (Input.GetMouseButtonDown(2))
        {
            //removeUI
        }
        if(Input.mouseScrollDelta.y < 0)
        {
            //skipDialogue
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            //Showhistory
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //SkipDialogueText;
            //NextDialogue
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            //SkipDialouge
            //LoadAllText
            //NextDialoufe
        }
        #endregion


    }

    private void NextDialogue()
    {
        _currentDialogueIndex++;
        _dialogueBoxText.text = _dialogueOptions[_currentDialogueIndex].Dialogue;
        _characterNameText.text = _dialogueOptions[_currentDialogueIndex].CurrentCharacterName;
    }

    private void DrawDialogueText()
    {

    }
}

[Serializable]
public class DialogueOptions
{
    [SerializeField] private DialogueType _dialogueType = DialogueType.Default;
    [SerializeField] private string _characterTalking;

    [SerializeField] private Image _newBackground;

    //character Expression

    [SerializeField]
    [TextArea(1, 2)]
    private string _dialogue;

    public DialogueType TypeOfDialogue => _dialogueType;
    public string CurrentCharacterName => _characterTalking;
    public Image Background => _newBackground;
    public string Dialogue => _dialogue;
}
