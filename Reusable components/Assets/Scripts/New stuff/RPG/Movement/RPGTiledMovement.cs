using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using TreeEditor;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.Rendering;
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

public enum normalSequence
{
    Input,
    MovementType,
    WallDetection,
    Sequence,
    Movement,
    Nothing
}

[RequireComponent(typeof(Rigidbody2D))]
public class RPGTiledMovement : MonoBehaviour
{
    //Required components
    [SerializeField] private Tilemap _tileMap;

    [SerializeField] private LayerMask wallColliderLayer; 
    private Rigidbody2D _rigidbody;


    private Vector3 _cellTranfromPosition;

    //Object Moving Values
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _sprintSpeed;

    [SerializeField] private bool _diagonalMovement = false;

    Vector2 nextCellPosition = Vector2.zero;

    private float _speed;

    private bool[] _hasRaycastHitWall = new bool[8];

    float x;
    float y;

    //Direction Vectors
    [SerializeField] private TiledMoveDirection _moveDirection;
    private Vector2 _directionVector;

    [SerializeField] private Animator playerAnimator;

    //raycast
    private Vector2[] _rayAngles = new Vector2[8];

    private bool checkedAngles = false;

    private bool horizontalPriority = false;
    private bool verticalPriority = false;


    private bool isMoving;


    //Enums
    private normalSequence sequence = normalSequence.Input;

    public Vector2 Direction
    {
        get { return _directionVector; }
    }

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

        #region Priority Input 
        //Has to be in the update and not in any of the functions or else the ButtonUp will never be detected.
        //This is because as soon as 1 of them goes true it will activate the next sequence.

        if (Input.GetButtonDown("Horizontal"))
        {
            horizontalPriority = true;
            verticalPriority = false;
        }
        if (Input.GetButtonDown("Vertical"))
        {
            verticalPriority = true;
            horizontalPriority = false;
        }

        if (Input.GetButton("Horizontal") && verticalPriority == false)
        {
            if (horizontalPriority == false)
            {
                horizontalPriority = true;
            }
        }
        else
        {
            horizontalPriority = false;
        }

        if (Input.GetButton("Vertical") && horizontalPriority == false)
        {
            if (verticalPriority == false)
            {
                verticalPriority = true;
            }
        }
        else
        {
            verticalPriority = false;
        }
        #endregion

        switch (sequence)
        {
            case normalSequence.Input:
                
                Vector3Int cellPosition = _tileMap.WorldToCell(transform.position);
                _cellTranfromPosition = _tileMap.GetCellCenterLocal(cellPosition);
                transform.position = _cellTranfromPosition;
                //Debug.Log("-----Inpute Fase-----");
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _speed = _sprintSpeed;
                }
                else
                {
                    _speed = _walkSpeed;
                }
                DirectionInput();
                break;

            case normalSequence.MovementType:
                MoveDirection();
                break;

            case normalSequence.WallDetection:
                nextCellPosition = new Vector2(_cellTranfromPosition.x + x, _cellTranfromPosition.y + y);
                WallDetection();
                break;

        }

        playerAnimator.SetInteger("FacingInt" ,(int)_moveDirection);
        playerAnimator.SetBool("IsWalking", isMoving);
        
    }

    private void FixedUpdate()
    {
        if (sequence == normalSequence.Movement)
        {
            isMoving = true;
            MoveObjectToCell(nextCellPosition);
        }
    }

    private void DirectionInput()
    {
        float horizontalInp = Input.GetAxisRaw("Horizontal");
        float verticalInp = Input.GetAxisRaw("Vertical");

        

        if (_diagonalMovement == true)
        {
            if (horizontalInp != 0 && verticalInp != 0)
            {
                x = 0;
                y = 0;
                x = horizontalInp;
                y = verticalInp;
                sequence = normalSequence.MovementType;
                return;
            }

            if (horizontalInp != 0)
            {
                y = 0;
                x = horizontalInp;
                sequence = normalSequence.MovementType;
                return;
            }

            if (verticalInp != 0)
            {
                x = 0;
                y = verticalInp;
                sequence = normalSequence.MovementType;

                return;
            }
        }
        else
        {
            if (verticalPriority == true)
            { 
                x = 0;
                y = verticalInp;
                sequence = normalSequence.MovementType;
                return;
            }

            if (horizontalPriority == true)
            {
                y = 0;
                x = horizontalInp;
                sequence = normalSequence.MovementType;
                return;
            }
        }
        isMoving = false;
    }

    private void MoveDirection()
    {
        Vector2 direction = new Vector2(_speed, _speed);

        


        var normalizedSpeed = (_speed * Mathf.Sin(Mathf.PI * 2 * 45 / 360)) * 1.5f;


        switch (x)
        {
            case -1:
                switch (y)
                {
                    case -1:
                        //_speed /= 1.05f;
                        _speed = normalizedSpeed;
                        _moveDirection = TiledMoveDirection.SouthWest;
                        break;

                    case 0:
                        _moveDirection = TiledMoveDirection.West;
                        break;

                    case 1:
                        _speed = normalizedSpeed;
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
                        _speed = normalizedSpeed;
                        _moveDirection = TiledMoveDirection.SouthEast;
                        break;

                    case 0:
                        _moveDirection = TiledMoveDirection.East;
                        break;

                    case 1:
                        _speed = normalizedSpeed;
                        _moveDirection = TiledMoveDirection.NorthEast;
                        break;
                }
                break;
        }

        _directionVector = new Vector2(x, y).normalized;
        sequence = normalSequence.WallDetection;
    }
    private void WallDetection()
    {
        //for every angle shoot a ray and check if one of the collisions is a collider.
        Vector2[] transformArray = new Vector2[8];
        for (int i = 0; i < _rayAngles.Length; i++)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, _rayAngles[i], 1f, wallColliderLayer);
            for (int j = 0; j < hit.Length; j++)
            {
                //If the hit is not the player and if the collider is not null, save the collider position.
                if (hit[j].transform != transform && hit[j].collider != null)
                {
                    _hasRaycastHitWall[j] = true;
                    Vector2 colliderTranform = Vector2.zero;
                   
                    switch (i)
                    {
                        case 0:
                            
                            colliderTranform = new Vector2(_cellTranfromPosition.x + 1, _cellTranfromPosition.y);
                            transformArray[i] = colliderTranform;
                            break;

                        case 1:
                            colliderTranform = new Vector2(_cellTranfromPosition.x + 1, _cellTranfromPosition.y + 1);
                            transformArray[i] = colliderTranform;
                            break;

                        case 2:
                            colliderTranform = new Vector2(_cellTranfromPosition.x, _cellTranfromPosition.y + 1);
                            transformArray[i] = colliderTranform;
                            break;

                        case 3:
                            colliderTranform = new Vector2(_cellTranfromPosition.x - 1, _cellTranfromPosition.y + 1);
                            transformArray[i] = colliderTranform;
                            break;

                        case 4:
                            colliderTranform = new Vector2(_cellTranfromPosition.x - 1, _cellTranfromPosition.y);
                            transformArray[i] = colliderTranform;
                            break;

                        case 5:
                            colliderTranform = new Vector2(_cellTranfromPosition.x - 1, _cellTranfromPosition.y - 1);
                            transformArray[i] = colliderTranform;
                            break;

                        case 6:
                            colliderTranform = new Vector2(_cellTranfromPosition.x, _cellTranfromPosition.y - 1);
                            transformArray[i] = colliderTranform;
                            break;

                        case 7:
                            colliderTranform = new Vector2(_cellTranfromPosition.x + 1, _cellTranfromPosition.y - 1);
                            transformArray[i] = colliderTranform;
                            break;
                    }
                    
                    //If the next position contains a collider then return to the input fase.
                    if (colliderTranform == nextCellPosition)
                    {
                        sequence = normalSequence.Input;
                        return;
                    }
                }
                else
                {
                    _hasRaycastHitWall[j] = false;
                }
            }

            if(i == 7)
            {
                checkedAngles = true;
            }
        }

        if (checkedAngles == true)
        {
            if (_diagonalMovement)
            {
                switch (_moveDirection)
                {
                    case TiledMoveDirection.NorthEast:

                        ColliderCheck(transformArray, 0, 2);
                        checkedAngles = false;
                        break;

                    case TiledMoveDirection.NorthWest:

                        ColliderCheck(transformArray, 4, 2);
                        checkedAngles = false;
                        break;

                    case TiledMoveDirection.SouthEast:

                        ColliderCheck(transformArray, 0, 6);
                        checkedAngles = false;
                        break;

                    case TiledMoveDirection.SouthWest:

                        ColliderCheck(transformArray, 4, 6);
                        checkedAngles = false;
                        break;

                }

                if (sequence == normalSequence.WallDetection)
                {
                    sequence = normalSequence.Movement;
                    checkedAngles = false;
                }

            }
            else
            {
                sequence = normalSequence.Movement;
                checkedAngles = false;
                return;
            }
        }
    }
    private void ColliderCheck(Vector2[] array, int horizontalInt, int VerticalInt)
    {
        if (array[horizontalInt] != Vector2.zero && array[VerticalInt] != Vector2.zero)
        {
            sequence = normalSequence.Input;
            return;
        }
        else if (array[horizontalInt] != Vector2.zero)
        {
            nextCellPosition = new Vector2(_cellTranfromPosition.x, _cellTranfromPosition.y + y);
            sequence = normalSequence.Movement;
            return;
        }
        else if (array[VerticalInt] != Vector2.zero)
        {
            nextCellPosition = new Vector2(_cellTranfromPosition.x + x, _cellTranfromPosition.y);
            sequence = normalSequence.Movement;
            return;
        }
    }
    private void MoveObjectToCell(Vector2 NextCellPosition)
    {
        Vector2 position = Vector2.MoveTowards(transform.position, NextCellPosition, _speed * Time.deltaTime);

        _rigidbody.MovePosition(position);

        if (position == NextCellPosition)
        {
            _cellTranfromPosition = NextCellPosition;
            sequence = normalSequence.Input;
            return;
        }
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
    private Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    private Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
}
