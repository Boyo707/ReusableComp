using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AttemptEditor : MonoBehaviour
{
    public CharacterData characterData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(characterData.CharacterFaces[0].FaceName);
        /*
        if (areaTransition.transitionType == AreaTransitionOptionType.Teleport)
        {


            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Position to teleport to", EditorStyles.boldLabel, GUILayout.MaxWidth(140));
            //areaTransition.teleportTransform = EditorGUILayout.ObjectField("Transform of the next position", areaTransition.teleportTransform, typeof(Transform), true) as Transform;

            areaTransition.secondTeleporter.SetActive(true);

            if (areaTransition.showBackgrounds)
            {
                EditorGUI.indentLevel++;
                List<GameObject> list = areaTransition.sonic;
                int size = Mathf.Max(0, EditorGUILayout.IntField("Size", list.Count));

                while (size > list.Count)
                {
                    list.Add(null);
                }

                while (size < list.Count)
                {
                    list.RemoveAt(list.Count - 1);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    //if the object comes from in the scene = true. If the object is inside the assets folder/scriptable object = false.
                    list[i] = EditorGUILayout.ObjectField("Element " + i, list[i], typeof(GameObject), true) as GameObject;
                }

                EditorGUI.indentLevel--;
            }
        }
        else if (areaTransition.transitionType == AreaTransitionOptionType.SceneTransition)
        {

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Scene Transition Options", EditorStyles.boldLabel, GUILayout.MaxWidth(160));
            areaTransition.sceneName = EditorGUILayout.TextField("Scene Name", areaTransition.sceneName);

            areaTransition.secondTeleporter.SetActive(false);
        }*/
        
    }
}
