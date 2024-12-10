using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{

    [SerializeField] private Transform interactSource;
    [SerializeField] private float interactRange;
    [SerializeField] private RPGTiledMovement movement;
    [SerializeField] private LayerMask layerMask;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(interactSource.position, movement.Direction, interactRange, layerMask);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.TryGetComponent(out IInteractable interactObject))
                {
                    interactObject.Interact();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(interactSource.position, movement.Direction);
    }
}
