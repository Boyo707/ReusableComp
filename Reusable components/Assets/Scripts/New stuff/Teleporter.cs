using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

using UnityEngine.SceneManagement;
using System;
using UnityEngine.InputSystem.LowLevel;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;

#if UNITY_EDITOR

using UnityEditor;

#endif

public enum TeleportType
{
    Entrance,
    Exit
}

public class Teleporter : MonoBehaviour
{

    [Header("Area Transition Options")]
    [SerializeField] private bool activateOnInteract;
    [SerializeField] TeleportType teleportType;
    [SerializeField] Animator transitionAnimator;

    [SerializeField]
    private GameObject secondTeleporter;

    private Transform entityTransform;

    [SerializeField, HideInInspector]
    private string selectedTag = "";


    public bool hasEntered;

    private string transitionIdle = "Idle";
    private string transitionIn = "Transition IN";
    private string transitionOut = "Transition OUT";

    private string currentState;

    public bool isTransitioning;

    private float transitionDelay = 0.05f;

    public bool hasTeleported = false;

    #region Editor
#if UNITY_EDITOR
    [Serializable]
    [CustomEditor(typeof(Teleporter)), CanEditMultipleObjects]
    public class ShopItemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Teleporter areaTransition = (Teleporter)target;
            areaTransition.selectedTag = EditorGUILayout.TagField("Tag to detect", areaTransition.selectedTag);

        }

        private void DrawDetails(Teleporter areaTransition)
        {

            /*(EditorGUILayout.Space();
            EditorGUILayout.LabelField("ANISDIABIODUABDIOA");
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Name", GUILayout.MaxWidth(50));
            areaTransition.typeName = EditorGUILayout.TextField(areaTransition.typeName);

            EditorGUILayout.LabelField("Cost", GUILayout.MaxWidth(50));
            areaTransition.cost = EditorGUILayout.IntField(areaTransition.cost);

            EditorGUILayout.LabelField("Happiness", GUILayout.MaxWidth(75));
            areaTransition.happiness = EditorGUILayout.FloatField(areaTransition.happiness);

            EditorGUILayout.EndHorizontal();
            */
        }
    }
#endif
    #endregion

    private void Update()
    {
        if(isTransitioning == true)
        {
            Debug.Log("reached 2");
            TeleportPlayer(entityTransform, secondTeleporter.transform);
        }
    }

    private void TeleportPlayer(Transform entityTransform, Transform destination)
    {
        if (transitionAnimator != null)
        {
            if (!IsAnimationPlaying(transitionAnimator, transitionIn))
            {
                ChangeAnimationState(transitionIdle, false);
                entityTransform.position = secondTeleporter.transform.position;
                isTransitioning = false;
            }
        }
        else if (transitionAnimator == null)
        {
            entityTransform.position = secondTeleporter.transform.position;
            hasEntered = false;
            isTransitioning = false;
        }
    }

    private void ChangeAnimationState(string newState, bool playState)
    {

        if (newState == currentState)
        {
            return;
        }

        if (playState == true)
        {
            transitionAnimator.Play(newState);
        }

        currentState = newState;
    }

    private bool IsAnimationPlaying(Animator animator, string stateName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) && stateName == currentState && transitionDelay <= 0)
        {
            return true;
        }
        else if (transitionDelay > 0)
        {
            transitionDelay -= Time.deltaTime;
            return true;
        }
        else
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            {
                transitionDelay = 0.05f;
                return false;
            }
            return true;
        }

        
    }

    public void EnteredTeleport()
    {
        hasTeleported = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(selectedTag) && hasEntered == false && hasTeleported == false)
        {
            Debug.Log("has entered  =  " + gameObject.name);
            hasEntered = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(selectedTag);
        if (teleportType == TeleportType.Entrance && hasEntered == true && transform.position == collision.transform.position)
        {
            if (transitionAnimator != null)
            {
                ChangeAnimationState(transitionIn, true);
            }
            else
            {
                
                entityTransform = collision.transform;
                isTransitioning = true;
                secondTeleporter.GetComponent<Teleporter>().EnteredTeleport();
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(selectedTag) && hasTeleported == true)
        {
            hasTeleported = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (Selection.activeGameObject == transform.gameObject || Selection.activeGameObject == secondTeleporter || Selection.activeGameObject == transform.parent.gameObject)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, secondTeleporter.transform.position);
        }
    }
}



