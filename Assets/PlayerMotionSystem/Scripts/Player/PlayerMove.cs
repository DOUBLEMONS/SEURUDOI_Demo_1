using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //리스트
    //왼쪽 벽에 붙었을 때 라인렌더러는 오직 5도 보다 크거나 175도 보다 작을 때만 활성화된다.
    //오른쪽 벽에 붙었을 때 라인렌더러는 오직 185도 보다 크거나 355도 보다 작을 때만 활성화된다.
    //천장에 붙었을 때 라인렌더러는 오직 95도 보다 크거나 265도 보다 작을 때만 활성화된다.
    //바닥에 붙었을 때 라인렌더러는 오직 85도 보다 작거나 275도 보다 클 때만 활성화된다. 
    //천장 부분 오류남 옆쪽으로 콜라이더 적용이 안됨.

    //Main
    Rigidbody2D Rigidbody2D;

    //Collision
    public float SlidingSpeed;

    //Move
    private float MovePower;
    public float MoveSpeed = 10;

    //Jump
    public float JumpPower = 10;
    public bool IsJumping;
    public float WallJumpPower = 10;
    bool IsWallJumping;

    //Flash
    public float FlashSpeed = 10;
    public int IsFlashingCount = 0;
    public Camera Camera;
    Vector3 MousePos;
    public LineRenderer LineRenderer;
    Vector3 EndPoint;
    Vector3 StartPoint;

    //Raycast
    [SerializeField]
    GameObject face;

    [SerializeField]
    Transform castPoint;

    [SerializeField]
    float agroRange;

    bool isFacingLeft;

    void Awake()
    {

    }

    void Start()
    {
        //Main
        Rigidbody2D = GetComponent<Rigidbody2D>();

        //Move

        //Jump

        //Flash
        Camera = Camera.main;
        LineRenderer = GetComponent<LineRenderer>();
        LineRenderer.positionCount = 2;
    }

    //Main
    void Update()
    {
        Jump();
        Flash();
        CollisionDetection(agroRange);
    }

    void FixedUpdate()
    {
        Move();
    }

    //Raycast
    bool CollisionDetection(float distance)
    {
        bool val = false;
        float canDist = distance;
        float canDistWithFloor = distance;

        if (isFacingLeft)
        {
            canDist = -distance;
            canDistWithFloor = -distance;
        }

        Vector2 endPosdown = castPoint.position + Vector3.down * distance;

        RaycastHit2D hitdown = Physics2D.Linecast(transform.position, endPosdown, 1 << LayerMask.NameToLayer("Action"));

        Vector2 endPosleft = castPoint.position + Vector3.left * distance;

        RaycastHit2D hitleft = Physics2D.Linecast(transform.position, endPosleft, 1 << LayerMask.NameToLayer("Action"));

        Vector2 endPosright = castPoint.position + Vector3.right * distance;

        RaycastHit2D hitright = Physics2D.Linecast(transform.position, endPosright, 1 << LayerMask.NameToLayer("Action"));

        Vector2 endPosup = castPoint.position + Vector3.up * distance;

        RaycastHit2D hitup = Physics2D.Linecast(transform.position, endPosup, 1 << LayerMask.NameToLayer("Action"));


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //CheckLeft

        if (hitleft.collider != null)
        {
            //Ray
            Debug.DrawLine(castPoint.position, hitleft.point, Color.yellow);

            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, Rigidbody2D.velocity.y * SlidingSpeed);
            Rigidbody2D.drag = 8;

            //True or False
            IsJumping = true;
            IsWallJumping = false;

            //Reset
            IsFlashingCount = 0;

            //Escape
            if (Input.GetButtonDown("Jump"))
            {
                Rigidbody2D.velocity = Vector3.right * WallJumpPower * 10f * Time.fixedDeltaTime;
                IsWallJumping = true;
                IsJumping = true;
            }
        }
        else if(hitleft.collider == null && hitright.collider == null)
        {
            //Ray
            Debug.DrawLine(castPoint.position, endPosleft, Color.blue);
            Rigidbody2D.drag = 0;
            IsJumping = true;
            IsWallJumping = true;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //CheckRight

        if (hitright.collider != null)
        {
            //Ray
            Debug.DrawLine(castPoint.position, hitright.point, Color.yellow);

            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, Rigidbody2D.velocity.y * SlidingSpeed);
            Rigidbody2D.drag = 8;

            //True or False
            IsJumping = true;
            IsWallJumping = false;

            //Rest
            IsFlashingCount = 0;

            //Escape
            if (Input.GetButtonDown("Jump"))
            {
                Rigidbody2D.velocity = Vector3.left * WallJumpPower * 10f * Time.fixedDeltaTime;
                IsWallJumping = true;
                IsJumping = true;
            }
        }
        else if(hitright.collider == null && hitleft.collider == null)
        {
            //Ray
            Debug.DrawLine(castPoint.position, endPosright, Color.blue);
            Rigidbody2D.drag = 0;
            IsJumping = true;
            IsWallJumping = true;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //CheckUp

        if (hitup.collider != null)
        {
            //Ray
            Debug.DrawLine(castPoint.position, hitup.point, Color.yellow);

            //True or False
            IsJumping = true;
            IsFlashingCount = 0;

            //Fixed
            Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            MoveSpeed = 0;

            //Escape
            if (Input.GetButton("Jump"))
            {
                Rigidbody2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                Rigidbody2D.velocity = Vector3.down * WallJumpPower * 0.25f;
                IsWallJumping = true;
                IsJumping = true;
                MoveSpeed = 25;
            }
            else if (Input.GetMouseButton(0))
            {
                Rigidbody2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                MoveSpeed = 25;
            }

        }
        else
        {
            Debug.DrawLine(castPoint.position, endPosup, Color.blue);

            IsJumping = true;
            IsWallJumping = true;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //CheckDown

        if (hitdown.collider != null)
        {
            Debug.DrawLine(castPoint.position, hitdown.point, Color.yellow);

            IsFlashingCount = 0;
            IsJumping = false;
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPosdown, Color.blue);

            IsJumping = true;
            IsWallJumping = true;
        }

        return val;
    }

    //Move
    void Move()
    {
        float MovePower = Input.GetAxis("Horizontal");

        transform.Translate((new Vector2(MovePower, 0) * MoveSpeed) * Time.deltaTime);
    }

    //Jump
    void Jump()
    {
        if (IsJumping == false)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Rigidbody2D.AddForce(Vector3.up * JumpPower, ForceMode2D.Impulse);
                IsJumping = true;
            }
        }
    }

    //Flash
    void Flash()
    {
        MousePos = Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 MousezDir = MousePos - gameObject.transform.position;
        MousezDir.z = 0;
        MousezDir = MousezDir.normalized;

        if (IsFlashingCount < 2)
        {
            if (Input.GetMouseButton(0))
            {
                IsJumping = true;
                LineRenderer.enabled = true;
                Time.timeScale = 0.0125f;
                Time.fixedDeltaTime = 0.01f * Time.timeScale;

                StartPoint = gameObject.transform.position;
                StartPoint.z = 0;
                LineRenderer.SetPosition(0, StartPoint);
                EndPoint = MousePos;
                EndPoint.z = 0;
                LineRenderer.SetPosition(1, EndPoint);
            }

            if (Input.GetMouseButtonUp(0))
            {
                LineRenderer.enabled = false;
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                Rigidbody2D.velocity = Vector3.zero;

                Rigidbody2D.AddForce(MousezDir * FlashSpeed * 2f, ForceMode2D.Impulse);

                IsFlashingCount++;
            }
        }
    }
}

