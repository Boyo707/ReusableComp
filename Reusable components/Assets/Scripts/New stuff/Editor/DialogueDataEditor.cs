using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueDataObject)), CanEditMultipleObjects]
public class DialogueDataEditor : Editor
{
    private SerializedProperty options;
    private void OnEnable()
    {
        options = serializedObject.FindProperty("options");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.UpdateIfRequiredOrScript();
        base.OnInspectorGUI();

        DialogueDataObject dialogueOptions = (DialogueDataObject)target;


        EditorGUI.indentLevel++;

        EditorGUILayout.PropertyField(options, new GUIContent("Dialogue"));

        EditorGUI.indentLevel--;


        serializedObject.ApplyModifiedProperties();
    }
}
#endif
