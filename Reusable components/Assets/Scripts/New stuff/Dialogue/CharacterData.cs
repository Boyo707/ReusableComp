using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using static UnityEditor.Progress;
using System.Linq;
using UnityEditor.U2D.Animation;
using static CharacterData;
using UnityEngine.PlayerLoop;

#if UNITY_EDITOR

using UnityEditor;

#endif

public enum CharacterTypes
{
    Ally,
    Enemy,
    Friendly,
    Merchant,
    Special
}

public enum CharacterGender
{
    Male,
    Female,
    NonBinary
}

[System.Serializable]
public class characterFacesData
{
    [SerializeField]
    private string faceName;

    [SerializeField]
    private Sprite faceImage;

    public string FaceName => faceName;
}

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData"), Serializable]
public class CharacterData : ScriptableObject
{
    /*
     * 
     * variables needs to be set more protective with privates or get set
     * 
     */

    public string characterName;
    [Space]

    public CharacterGender characterGender;
    public CharacterTypes characterType;
    public Color textColor;


    [TextArea(1, 25)] public string characterDescription;


    [SerializeField, HideInInspector]
    private characterFacesData[] characterFaces;

    public Sprite characterDialogueImage { get; set; }
    public characterFacesData[] CharacterFaces => characterFaces;

    [SerializeField, HideInInspector]
    private AudioClip characterDialogueAudio;

}



