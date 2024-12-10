using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

#if UNITY_EDITOR
[Serializable]
[CustomEditor(typeof(CharacterData)), CanEditMultipleObjects]
public class CharacterDataEditor : Editor
{

    private SerializedProperty characterFaces;
    private SerializedProperty characterDialogueAudio;

    public void OnEnable()
    {

        characterFaces = serializedObject.FindProperty("characterFaces");
        characterDialogueAudio = serializedObject.FindProperty("characterDialogueAudio");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.UpdateIfRequiredOrScript();

        //things above normal class serialization
        base.OnInspectorGUI();
        //things under normal class serialz

        CharacterData characterData = (CharacterData)target;
        EditorGUI.indentLevel++;

        EditorGUILayout.PropertyField(characterFaces, new GUIContent("Faces"));


        

        EditorGUI.indentLevel--;

        serializedObject.ApplyModifiedProperties();
    }

    
}
#endif
