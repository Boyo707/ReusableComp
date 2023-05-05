using System.Collections;
using System.Collections.Generic;
using TreeEditor;
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

    [SerializeField] private float _blockedMovementTimer = 1f;
    private int _blockedAmount = 0;

    Vector3 _previosPosition = Vector3.zero;

    private float _speed;

    private bool _pressedInput = false;
    private bool _wallCheck = false;
    private bool _moveObject = false;

    float x;
    float y;

    //Direction Vectors
    public TiledMoveDirection _moveDirection;
    private Vector2 _directionVector;

    //raycast
    private Vector2[] _rayAngles = new Vector2[8];


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
                WallDetection();
            }
        }
    }

    private void FixedUpdate()
    {
        
        if (_moveObject)
        {
            Vector3 NextCellPosition = new Vector3(_cellTranfromPosition.x + x, _cellTranfromPosition.y + y, _cellTranfromPosition.z);
            
            MoveObjectToCell(NextCellPosition);
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
        if (x == 1)
        {
            if (y == 1)
            {
                _moveDirection = TiledMoveDirection.NorthEast;

            }
            else if (y == 0)
            {
                _moveDirection = TiledMoveDirection.East;

            }
            else if (y == -1)
            {
                _moveDirection = TiledMoveDirection.SouthEast;
            }

        }
        else if (x == 0)
        {
            if (y == 1)
            {
                _moveDirection = TiledMoveDirection.North;


            }
            else if (y == 0)
            {
                _moveDirection = TiledMoveDirection.NoDirection;

            }
            else if (y == -1)
            {
                _moveDirection = TiledMoveDirection.South;

            }

        }
        else if (x == -1)
        {
            if (y == 1)
            {
                _moveDirection = TiledMoveDirection.NorthWest;

            }
            else if (y == 0)
            {
                _moveDirection = TiledMoveDirection.West;
            }
            else if (y == -1)
            {
                _moveDirection = TiledMoveDirection.SouthWest;
            }
        }

        _directionVector = new Vector2(x, y).normalized;
        _wallCheck = true;
        
    }


    //Wall check most likely can detect false walls or accidental wall collision checks.
    //DIRECTION DEGREE CALCULATES WRONG ANGLE SOMEHOW. MIGHT NOT TO CHANGE 
    private void WallDetection()
    {

        
        for (int i = 0; i < _rayAngles.Length; i++)
        {

            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, _rayAngles[i], 1f);
            for (int j = 0; j < hit.Length; j++)
            {

                if (hit[j].transform != transform && hit[j].collider != null)
                {
                    
                    float rayDegree = Mathf.Round(Vector2.Angle(transform.position, _rayAngles[i]));
                    float directionDegree = Mathf.Round(Vector2.Angle(transform.position, _directionVector));
                    if (rayDegree == directionDegree)
                    {
                        Debug.Log("SAME ANGLE");
                        Debug.Log(i);
                        Debug.Log(directionDegree + " " + _directionVector + " " + _moveDirection.ToString());
                        Debug.Log(Mathf.Round(Vector2.Angle(transform.position, _directionVector)));
                        _wallCheck = false;
                        _pressedInput = false;
                        return;
                    }

                }

            }

        }

        _wallCheck = false;
        _pressedInput = false;
        _moveObject = true;
    }

    private void MoveObjectToCell(Vector3 NextCellPosition)
    {

        

        Vector3 position = Vector3.MoveTowards(transform.position, NextCellPosition, _speed * Time.deltaTime);

        _rigidbody.MovePosition(position);

        //OTHER SOLUTION IS TO KEEP UP HOW MANY TIMES A FRAME IT IS STUCK ON THE SAME P

        Debug.Log("Moving");

        
        Vector3 roundedPosition = new Vector3(Mathf.Round(transform.position.x * 100f) / 100, Mathf.Round(transform.position.y * 100f) / 100f, 0);
        roundedPosition = new Vector3(Mathf.Round(transform.position.x * 10f) / 10f, Mathf.Round(transform.position.y * 10f) / 10f, 0);
        
        if (roundedPosition == NextCellPosition)
        {
            Debug.Log("reached");
            _cellTranfromPosition = NextCellPosition;
            _moveObject = false;
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
    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
}
