using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Damage : MonoBehaviour
{

    //Issue is that the values get erased after pressing the pay button.

    /*[CustomEditor(typeof(Damage))]
    public class MyScriptEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            var myScript = target as Damage;

            EditorGUILayout.LabelField("Damage Settings", EditorStyles.boldLabel);
            myScript._damagableLayers = EditorGUILayout.LayerField("Damagable Layers" ,myScript._damagableLayers);
            myScript._damageAmount = EditorGUILayout.FloatField("Damage Amount", myScript._damageAmount);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Knock Back Settings", EditorStyles.boldLabel);

            myScript._hasKnockBack = GUILayout.Toggle(myScript._hasKnockBack, "Includes Knock Back");

            if (myScript._hasKnockBack)
            {
                myScript._knockBackForce = EditorGUILayout.FloatField("Knock Back Force", myScript._knockBackForce);
                myScript._knockBackAngle = EditorGUILayout.Slider("Knock Back Angle" ,myScript._knockBackAngle, 0, 360);
            }

        }
    }*/

    [Header("Damage values")]
    [SerializeField] private LayerMask _damagableLayers;
    [SerializeField] private float _damageAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == _damagableLayers.value - 1)
        {
            if (collision.GetComponent<Health>())
            {
                collision.GetComponent<Health>().TakeDamage(_damageAmount);
                
            }
        }
    }

    

}
