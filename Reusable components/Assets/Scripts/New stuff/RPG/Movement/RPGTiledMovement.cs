using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TiledMoveDirection
{
    North,
    NorthEast,
    East,
    SouthEast,
    South,
    SouthWest,
    West,
    NorthWest,
    NoDirection
}
[RequireComponent(typeof(Rigidbody2D))]
public class RPGTiledMovement : MonoBehaviour
{
    //Required components
    [SerializeField] private Tilemap _tileMap;
    private Rigidbody2D _rigidbody;


    private Vector3 _cellTranfromPosition;

    //Object Moving Values
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _sprintSpeed;

    [SerializeField] private bool _diagonalMovement = false;

    Vector2 nextCellPosition = Vector2.zero;

    private float _speed;

    private bool _pressedInput = false;
    private bool _wallCheck = false;
    private bool _moveObject = false;

    private bool[] _hasRaycastHitWall = new bool[8];

    private bool _hasReachedDestination1;
    private bool _hasReachedDestination2;

    float x;
    float y;

    //Direction Vectors
    public TiledMoveDirection _moveDirection;
    private Vector2 _directionVector;

    //raycast
    private Vector2[] _rayAngles = new Vector2[8];


    /*TO DO
     * 
     * Maak diagonal movement naast tiles beter.
     *      Doe dit door een nieuwe void te maken dat checkt of er een muur naast je zit of niet. En dan laat het je 2 sequences doen van movement om naar je positie te komen.
     *      Of zorg er voor dat het movement blocked maar dit is minder om aan te raden.
     *      Mischien handig om research te doen van wat andere spellen doen met dezelfde movement.
     *      
     *Maad de input anders. Geef de input een priority van wat het laatst is ingedrukt. Zorgt er voor als diagonal uit staat dat de movement wat meer smooth voelt
     *Doe dit door de laatste gegeven input in een variable te doen dat de input veranderd.
     **/

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
         Vector3Int cellPosition = _tileMap.WorldToCell(transform.position);
        _cellTranfromPosition = _tileMap.GetCellCenterLocal(cellPosition);
        transform.position = _cellTranfromPosition;

        for (int i = 0; i < _rayAngles.Length; i++)
        {
            _rayAngles[i] = DegreeToVector2(45 * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = _sprintSpeed;
        }
        else
        {
            _speed = _walkSpeed;
        }

        DirectionInput();


        if (_pressedInput)
        {
            MoveDirection();
            if (_wallCheck)
            {
                nextCellPosition = new Vector2(_cellTranfromPosition.x + x, _cellTranfromPosition.y + y);
                WallDetection();
            }
        }
    }

    private void FixedUpdate()
    {
        if (_moveObject)
        {
            MoveObjectToCell(nextCellPosition);
        }
    }

    private void DirectionInput()
    {
        float horizontalInp = Input.GetAxisRaw("Horizontal");
        float verticalInp = Input.GetAxisRaw("Vertical");

        if (horizontalInp != 0 && verticalInp != 0 && _diagonalMovement && !_moveObject && !_pressedInput)
        {
            x = 0;
            y = 0;
            x = horizontalInp;
            y = verticalInp;
            _pressedInput = true;

            return;
        }

        if (horizontalInp != 0 && !_moveObject && !_pressedInput)
        {
            y = 0;
            x = horizontalInp;
            _pressedInput = true;

            return;
        }

        if (verticalInp != 0 && !_moveObject && !_pressedInput)
        {
            x = 0;
            y = verticalInp;
            _pressedInput = true;

            return;
        }
        return;
    }

    private void MoveDirection()
    {

        switch (x)
        {
            case -1:
                switch (y)
                {
                    case -1:
                        _moveDirection = TiledMoveDirection.SouthWest;
                        break;

                    case 0:
                        _moveDirection = TiledMoveDirection.West;
                        break;

                    case 1:
                        _moveDirection = TiledMoveDirection.NorthWest;
                        break;
                }
                break;

            case 0:
                switch (y)
                {
                    case -1:
                        _moveDirection = TiledMoveDirection.South;
                        break;

                    case 0:
                        _moveDirection = TiledMoveDirection.NoDirection;
                        break;

                    case 1:
                        _moveDirection = TiledMoveDirection.North;
                        break;
                }
                break;

            case 1:
                switch (y)
                {
                    case -1:
                        _moveDirection = TiledMoveDirection.SouthEast;
                        break;

                    case 0:
                        _moveDirection = TiledMoveDirection.East;
                        break;

                    case 1:
                        _moveDirection = TiledMoveDirection.NorthEast;
                        break;
                }
                break;
        }

        _directionVector = new Vector2(x, y).normalized;
        _wallCheck = true;
        
    }


    private void WallDetection()
    {
        for (int i = 0; i < _rayAngles.Length; i++)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, _rayAngles[i], 1f);
            for (int j = 0; j < hit.Length; j++)
            {
                if (hit[j].transform != transform && hit[j].collider != null)
                {
                    _hasRaycastHitWall[j] = true;
                    Vector2 colliderTranform = Vector2.zero;
                    switch (i)
                    {
                        case 0:
                            
                            colliderTranform = new Vector2(_cellTranfromPosition.x + 1, _cellTranfromPosition.y);
                            break;

                        case 1:
                            colliderTranform = new Vector2(_cellTranfromPosition.x + 1, _cellTranfromPosition.y + 1);
                            break;

                        case 2:
                            colliderTranform = new Vector2(_cellTranfromPosition.x, _cellTranfromPosition.y + 1);
                            break;

                        case 3:
                            colliderTranform = new Vector2(_cellTranfromPosition.x - 1, _cellTranfromPosition.y + 1);
                            break;

                        case 4:
                            colliderTranform = new Vector2(_cellTranfromPosition.x - 1, _cellTranfromPosition.y);
                            break;

                        case 5:
                            colliderTranform = new Vector2(_cellTranfromPosition.x - 1, _cellTranfromPosition.y - 1);
                            break;

                        case 6:
                            colliderTranform = new Vector2(_cellTranfromPosition.x, _cellTranfromPosition.y - 1);
                            break;

                        case 7:
                            colliderTranform = new Vector2(_cellTranfromPosition.x + 1, _cellTranfromPosition.y - 1);
                            break;
                    }
                    

                    if (colliderTranform == nextCellPosition)
                    {
                        _wallCheck = false;
                        _pressedInput = false;
                        return;
                    }
                }
                else
                {
                    _hasRaycastHitWall[j] = false;
                }
            }
        }
        _wallCheck = false;
        _pressedInput = false;
        _moveObject = true;
    }

    private void MoveObjectToCell(Vector2 NextCellPosition)
    {
        Vector2 position = Vector2.MoveTowards(transform.position, NextCellPosition, _speed * Time.deltaTime);

        _rigidbody.MovePosition(position);

        if (position == NextCellPosition)
        {
            Debug.Log("reached");
            _cellTranfromPosition = NextCellPosition;
            _moveObject = false;
        }
    }


    //Manier om dit ook te doen is door een array van bools te maken of een dictonary ofzo.
    //heel verwarrend
    private void Something()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, _rayAngles[2], 1f);
        //if (hit[j].transform != transform && hit[j].collider != null)
        if (x == 1 && y == 1)
        {

            if (!_hasReachedDestination1)
            {
                if (_moveObject == true)
                {
                    Vector2 sequencePosition1 = new Vector2(_cellTranfromPosition.x + 1, _cellTranfromPosition.y);
                    MoveObjectToCell(sequencePosition1);
                }
                else
                {
                    _hasReachedDestination1 = true;
                }
            }
            else if (!_hasReachedDestination2)
            {
                if (_moveObject == true)
                {
                    Vector2 sequencePosition1 = new Vector2(_cellTranfromPosition.x, _cellTranfromPosition.y + 1);
                    MoveObjectToCell(sequencePosition1);
                }
                else
                {
                    _hasReachedDestination2 = true;
                }
            }
            
        }
    }

    private void MovementSequence()
    {

    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < _rayAngles.Length; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, _rayAngles[i]);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _directionVector);
    }
    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
}
