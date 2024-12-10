using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Assertions.Must;
using System;
using UnityEditor.U2D.Animation;

[CustomPropertyDrawer(typeof(characterFacesData))]
public class CharacterFacesDrawer : PropertyDrawer
{
    private SerializedProperty faceName;
    private SerializedProperty faceImage;

    public override void OnGUI(Rect position, 
        SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        faceName = property.FindPropertyRelative("faceName");
        faceImage = property.FindPropertyRelative("faceImage");


        Rect foldoutBox = new Rect(position.min.x, position.min.y,
            position.size.x, EditorGUIUtility.singleLineHeight);
        property.isExpanded = EditorGUI.Foldout(foldoutBox, property.isExpanded, label, true);

        if (property.isExpanded)
        {
            DrawNameProperty(position);
            DrawImageProperty(position);
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight
        (SerializedProperty property, GUIContent label)
    {
        float totalLines = 1;

        if (property.isExpanded)
        {
            totalLines += 1.5f;
        }

        return (EditorGUIUtility.singleLineHeight * totalLines);
    }

    private void DrawNameProperty(Rect position)
    {
        EditorGUIUtility.labelWidth = 70;
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight;
        float width = position.size.x * .4f;
        float height = EditorGUIUtility.singleLineHeight;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, faceName, new GUIContent("Face Name"));
    }
    private void DrawImageProperty(Rect position)
    {
        EditorGUIUtility.labelWidth = 40;
        Rect drawArea = new Rect(position.min.x + (position.width * .5f),
            position.min.y + EditorGUIUtility.singleLineHeight,
            position.size.x * .5f, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(drawArea, faceImage, new GUIContent("Image"));
        
    }

    

    
}
