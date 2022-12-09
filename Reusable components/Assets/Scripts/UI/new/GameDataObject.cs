using UnityEngine;

[CreateAssetMenu(fileName = "TestingScriptableObject", menuName = "ScriptableObjects/Testing")]
public class GameDataObject : ScriptableObject
{
    [SerializeField] public int health = 100;
    [SerializeField] public float speed = 4f;

    public bool lol;

    public string Kill => speed.ToString();

}
