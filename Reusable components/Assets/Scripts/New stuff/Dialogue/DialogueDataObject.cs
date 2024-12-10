using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEditor.U2D.Animation;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

#if UNITY_EDITOR

using UnityEditor;

#endif

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
    [SerializeField]
    private Sprite startingBackground;

    [SerializeField, HideInInspector]
    private DialogueOptions[] options;

    public DialogueOptions[] Options => options;

    

}

[Serializable]
public class DialogueOptions
{

    [SerializeField, HideInInspector]
    private DialogueType dialogueType = DialogueType.Default;

    

    [SerializeField, HideInInspector]
    private CharacterData characterData;

    [SerializeField, HideInInspector]
    private Sprite faceSprite;

    [SerializeField, HideInInspector]
    private string dialogueText;

    [SerializeField, HideInInspector]
    private Image newBackground;

    [SerializeField, HideInInspector]
    private float textSpeed;

    [SerializeField, HideInInspector]
    private AudioClip textSound;

    [SerializeField, HideInInspector]
    private int audioPerCharacter;

    [SerializeField, HideInInspector]
    private string playerFace;

    [SerializeField, HideInInspector]
    private int selected;

    public DialogueType DialogueType => dialogueType;
    public CharacterData CharacterData => characterData;
    public Sprite FaceSprite => faceSprite;
    public string DialogueText => dialogueText;
    public Image Background => newBackground;
    public float TextSpeed => textSpeed;
    public AudioClip AudioClip => textSound;
    public int AudioPerCharacter => audioPerCharacter;
    public string PlayerFace => playerFace;
    public int Selected => selected;

}


