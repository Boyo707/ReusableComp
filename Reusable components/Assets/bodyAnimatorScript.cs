using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyAnimatorScript : MonoBehaviour
{
    [SerializeField]private SpriteRenderer _parentRenderer;
    [SerializeField] private AttackMellee attackMellee;
    private SpriteRenderer _thisRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _thisRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        _thisRenderer.flipX = _parentRenderer.flipX;
    }

}
