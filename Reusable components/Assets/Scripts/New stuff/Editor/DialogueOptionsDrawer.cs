using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Data;
using static Codice.Client.BaseCommands.Import.Commit;
using NUnit.Framework.Constraints;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using System.Reflection;
using System;
using Newtonsoft.Json;

[CustomPropertyDrawer(typeof(DialogueOptions))]
public class DialogueOptionsDrawer : PropertyDrawer
{
    private SerializedProperty dialogueType;
    private SerializedProperty characterData;
    private SerializedProperty faceSprite;
    private SerializedProperty dialogueText;
    //mischien dit in een andere functie plaatsen.
    private SerializedProperty newBackground;
    private SerializedProperty textSpeed;
    private SerializedProperty textSound;

    //public string playerFace;

    private SerializedProperty selected;

    private bool toggleTextDraw = false;
    private string newText = "00000000000";
    private int writtenCharacter = 0;
    private SerializedProperty audioPerCharacter;

    public override void OnGUI(Rect position,
        SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        //fill property
        dialogueType = property.FindPropertyRelative("dialogueType");
        characterData = property.FindPropertyRelative("characterData");
        selected = property.FindPropertyRelative("selected");
        faceSprite = property.FindPropertyRelative("faceSprite");
        dialogueText = property.FindPropertyRelative("dialogueText");
        textSound = property.FindPropertyRelative("textSound");
        textSpeed = property.FindPropertyRelative("textSpeed");
        audioPerCharacter = property.FindPropertyRelative("audioPerCharacter");

        //replaces the "Element 0" text to "0" so only the index is left
        string text = label.text.Replace("Element ", "");


        Rect foldoutBox = new Rect(position.min.x, position.min.y,
            position.size.x, EditorGUIUtility.singleLineHeight);
        property.isExpanded = EditorGUI.Foldout(foldoutBox, property.isExpanded, new GUIContent("Dialogue " + text), true);


        
        if (property.isExpanded)
        {
            DrawDialogueTypeProperty(position, 1);
            DrawCharacterDataProperty(position, 2);
            if (characterData.objectReferenceValue != null)
            {
                var characterDataSO = new SerializedObject(characterData.objectReferenceValue);
                var characterFaceArray = characterDataSO.FindProperty("characterFaces");

                string[] facesNames = new string[characterFaceArray.arraySize];
                Sprite[] faceSprites = new Sprite[characterFaceArray.arraySize];

                if (characterFaceArray.arraySize != 0)
                {

                    for (int i = 0; i < characterFaceArray.arraySize; i++)
                    {
                        SerializedProperty prop = characterFaceArray.GetArrayElementAtIndex(i);
                        facesNames[i] = prop.FindPropertyRelative("faceName").stringValue;
                        faceSprites[i] = prop.FindPropertyRelative("faceImage").objectReferenceValue as Sprite;
                    }


                    DrawCharacterFacesProperty(position, 3, facesNames);
                    faceSprite.objectReferenceValue = faceSprites[selected.intValue];
                    DrawCharacterFaceImageProperty(position, 4.5f, faceSprites[selected.intValue], 5);
                }

                DrawDialogueTextProperty(position, 5.25f, 5);
                DrawDialogueAudioProperty(position, 11f);
            }
        }
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight
        (SerializedProperty property, GUIContent label)
    {
        float totalLines = 1;

        if (property.isExpanded)
        {
            totalLines += 16f;
        }

        return (EditorGUIUtility.singleLineHeight * totalLines);
    }

    private void DrawDialogueTypeProperty(Rect position, int linePosition)
    {
        EditorGUIUtility.labelWidth = 150;
        Rect drawArea = new Rect(position.min.x,
            position.min.y + EditorGUIUtility.singleLineHeight * linePosition,
            position.size.x, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(drawArea, dialogueType, new GUIContent("Dialogue Type"));
    }

    private void DrawCharacterDataProperty(Rect position, float linePosition)
    {
        EditorGUIUtility.labelWidth = 150;
        Rect drawArea = new Rect(position.min.x,
            position.min.y + EditorGUIUtility.singleLineHeight * linePosition,
            position.size.x, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(drawArea, characterData, new GUIContent("Character Data"));
    }

    private void DrawCharacterFacesProperty(Rect position, float linePosition, string[] options)
    {
        EditorGUIUtility.labelWidth = 150;
        Rect drawArea = new Rect(position.min.x,
            position.min.y + EditorGUIUtility.singleLineHeight * linePosition,
            position.size.x, EditorGUIUtility.singleLineHeight);

        selected.intValue = EditorGUI.Popup(drawArea, "Character Faces", selected.intValue, options);
    }

    private void DrawCharacterFaceImageProperty(Rect position, float linePosition, Sprite face, float size)
    {
        Rect drawAreaImage = new Rect(position.max.x - EditorGUIUtility.singleLineHeight * 5,
            position.min.y + EditorGUIUtility.singleLineHeight * linePosition,
            EditorGUIUtility.singleLineHeight * size , EditorGUIUtility.singleLineHeight * size);

        Rect drawAreaLabel = new Rect(drawAreaImage.xMin + 5, drawAreaImage.yMax, position.size.x, EditorGUIUtility.singleLineHeight);

        GUIStyle style = new GUIStyle();
        style.normal.background = GetSlicedSpriteTexture(face);
        EditorGUI.LabelField(drawAreaImage, GUIContent.none, style);
        EditorGUI.LabelField(drawAreaLabel, new GUIContent("Selected Face"), EditorStyles.boldLabel);
    }

    private void DrawDialogueTextProperty(Rect position, float linePosition, int dialogueHeight)
    {
        EditorGUIUtility.labelWidth = 150;
        Rect drawAreaTextArea = new Rect(position.min.x,
            position.min.y + EditorGUIUtility.singleLineHeight * linePosition,
            position.size.x - EditorGUIUtility.singleLineHeight * 5, EditorGUIUtility.singleLineHeight * dialogueHeight);

        Rect drawAreaLabel = new Rect(position.min.x, drawAreaTextArea.min.y - EditorGUIUtility.singleLineHeight * 1,
            position.size.x, EditorGUIUtility.singleLineHeight );

        dialogueText.stringValue = EditorGUI.TextArea(drawAreaTextArea, dialogueText.stringValue);

        EditorGUI.LabelField(drawAreaLabel, new GUIContent("Dialogue Text"), EditorStyles.boldLabel);
    }

    private void DrawDialogueAudioProperty(Rect position, float linePosition)
    {
        EditorGUIUtility.labelWidth = 150;
        Rect drawAreaAudio = new Rect(position.min.x,
            position.min.y + EditorGUIUtility.singleLineHeight * linePosition,
            position.size.x, EditorGUIUtility.singleLineHeight);

        Rect drawAreaButton = new Rect(position.min.x,
            position.min.y + EditorGUIUtility.singleLineHeight * (linePosition + 1),
            position.size.x, EditorGUIUtility.singleLineHeight);

        Rect drawAreaSlider = new Rect(position.min.x,
            position.min.y + EditorGUIUtility.singleLineHeight * (linePosition + 2.5f),
            position.size.x - 55, EditorGUIUtility.singleLineHeight);

        Rect drawAreaSliderTextInput = new Rect(position.max.x - 50,
            position.min.y + EditorGUIUtility.singleLineHeight * (linePosition + 2.5f),
            50, EditorGUIUtility.singleLineHeight);

        Rect drawAreaAudioCharacter = new Rect(position.min.x,
            position.min.y + EditorGUIUtility.singleLineHeight * (linePosition + 3.5f),
            position.size.x, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(drawAreaAudio, textSound, new GUIContent("Dialogue Audio Clip"));

        if (GUI.Button(drawAreaButton, "Play Audio Clip") && textSound.objectReferenceValue != null)
        {
            StopAllClips();
            PlayClip(textSound.objectReferenceValue as AudioClip);
        }

        //textSpeed.floatValue = GUI.HorizontalSlider(drawAreaSlider, textSpeed.floatValue, 0, 20);

        textSpeed.floatValue = EditorGUI.Slider(drawAreaSlider, Mathf.Round(textSpeed.floatValue * 100f) / 100f, 0, 20);

        EditorGUI.PropertyField(drawAreaAudioCharacter, audioPerCharacter, new GUIContent("Audio Per Character"));

    }


    private void PlayClip(AudioClip clip, int startSample = 0, bool loop = false)
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;

        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod(
            "PlayPreviewClip",
            BindingFlags.Static | BindingFlags.Public,
            null,
            new Type[] { typeof(AudioClip), typeof(int), typeof(bool) },
            null
        );

        method.Invoke(
            null,
            new object[] { clip, startSample, loop }
        );
    }

    private void StopAllClips()
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;

        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod(
            "StopAllPreviewClips",
            BindingFlags.Static | BindingFlags.Public,
            null,
            new Type[] { },
            null
        );

        method.Invoke(
            null,
            new object[] { }
        );
    }
    Texture2D GetSlicedSpriteTexture(Sprite sprite)
    {
        var croppedTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);

        var pixels = sprite.texture.GetPixels((int)sprite.rect.x,
                                                (int)sprite.rect.y,
                                                (int)sprite.rect.width,
                                                (int)sprite.rect.height);

        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();

        return croppedTexture;
    }
}
