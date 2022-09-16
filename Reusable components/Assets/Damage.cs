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

    [SerializeField] private float _damageAmount;
    [SerializeField] private LayerMask _damagableLayers;

    [SerializeField] private bool _hasKnockBack;
    [SerializeField] private float _knockBackForce;
    [SerializeField] [Range(0, 360)] private float _knockBackAngle;

    [SerializeField] private bool _hasHitStun;
    [SerializeField] private float _hitStunDuration;

    private SpriteRenderer _spR;


    private void Start()
    {
        _spR = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool doneOnce = false;

        if (!doneOnce)
        {
            if (_spR.flipX)
            {
                if (_knockBackAngle < 90 && _knockBackAngle > 0)
                {
                    _knockBackAngle = 180 - _knockBackAngle;
                    doneOnce = true;
                }
                else if (_knockBackAngle > 180 && _knockBackAngle < 270)
                {
                    _knockBackAngle = 360 - (_knockBackAngle - 180);
                    doneOnce = true;
                }
                else if (_knockBackAngle < 180 && _knockBackAngle > 90)
                {
                    _knockBackAngle = 180 - _knockBackAngle;
                    doneOnce = true;
                }
                else if (_knockBackAngle > 270 && _knockBackAngle < 360)
                {
                    _knockBackAngle = 360 - (_knockBackAngle - 180);
                    doneOnce = true;
                }
            }
        }
        if (collision.gameObject.layer == _damagableLayers.value - 1)
        {
            if (collision.GetComponent<Health>())
            {
                var colHealth = collision.GetComponent<Health>();

                if(_hasKnockBack)
                    colHealth.TakeDamage2(_damageAmount, _knockBackForce, _knockBackAngle);
                else
                    colHealth.TakeDamage2(_damageAmount);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(new Vector2(transform.position.x - 2.5f, transform.position.y), new Vector2(Mathf.Cos(_knockBackAngle * Mathf.Deg2Rad), Mathf.Sin(_knockBackAngle * Mathf.Deg2Rad)));
    }

}
