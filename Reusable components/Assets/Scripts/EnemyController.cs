using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private IMovements movements;
    // Start is called before the first frame update
    void Start()
    {
        movements = GetComponent<IMovements>();
    }

    // Update is called once per frame
    void Update()
    {
        movements.MoveInput(Vector2.left);
    }
}
