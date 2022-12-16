using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DialogueType
{
    Default,
    Start,
    OptionSelection,
    Transition,
    Stop
}

[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObjects/DialogueData")]
public class DialogueDataObject : ScriptableObject
{

    public List<DialogueOptions> DialogueOptions = new List<DialogueOptions>();

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
