using UnityEngine;
using UnityEngine.UI;

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

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string _characterName;
    [Space]

    public CharacterGender _characterGender;
    public CharacterTypes _characterType;
    public Sprite _characterDialogueImage;


    [TextArea(1, 25)] public string _characterDescription;


}
