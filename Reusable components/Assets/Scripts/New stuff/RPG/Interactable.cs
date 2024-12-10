using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueDataObject dialogue;
    public void Interact()
    {
        DialogueManager.GetInstance().EnterDialogue(dialogue);
        Debug.Log("Interacted");
        Debug.Log("Interacted");
    }
}
