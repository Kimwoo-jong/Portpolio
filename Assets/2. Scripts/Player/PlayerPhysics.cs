using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    private Rigidbody2D rigid;

    public LayerMask groundLayer;
    public float horizontalInput;
    private float maxSpeed;
    private Transform groundCheck;

    [Header("경사면")]
    public float slopePower;                    //슬로프를 오르기 위해 밀어주는 힘
    public float slopeRayLength;                //슬로프 체크를 위한 Raycast의 길이
    [SerializeField]
    private bool isOnSlope;                     //슬로프를 오르고 있는지 확인해줄 변수
    [SerializeField]
    private Vector2 slopeDirection;             //슬로프의 방향
    [SerializeField]
    private float slopeAngle;                   //슬로프의 각도 확인용 변수

    [Header("플레이어 물리")]
    [SerializeField]
    private float playerGravityScale;           //플레이어가 받을 중력

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        //변수 초기화
        maxSpeed = 5;
        slopePower = 7;
        slopeRayLength = 1;
        slopeDirection = Vector2.zero;

        groundCheck = transform.Find("GroundCheck");

        //물리력 변수 초기화
        playerGravityScale = 4;

        rigid.gravityScale = playerGravityScale;
    }
    private void Update()
    {
        StopSlope();
        CheckSlope();
        ClimbSlope();
    }
    private void FixedUpdate()
    {
        //GetAxis()는 부드럽게 값을 받아옴, GetAxisRaw()는 즉시 값을 받아옴.
        float h = Input.GetAxisRaw("Horizontal");

        //좌우 키보드 입력값을 저장한다.
        horizontalInput = h;
    }
    //슬로프의 경사를 체크하여 힘을 가해주는 함수
    private void CheckSlope()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, slopeRayLength, groundLayer);

        if(hitInfo)
        {
            slopeAngle = Vector2.Angle(hitInfo.normal, Vector2.up);
            slopeDirection = Vector2.Perpendicular(hitInfo.normal).normalized;

            if (slopeAngle != 0)
                isOnSlope = true;
            else
                isOnSlope = false;

            Debug.DrawLine(hitInfo.point, hitInfo.point + hitInfo.normal, Color.blue);
            Debug.DrawLine(hitInfo.point, hitInfo.point + slopeDirection, Color.red);
        }
    }
    //슬로프를 오를 수 있도록 도와주는 함수
    private void ClimbSlope()
    {
        //Perpendicular함수는 반시계 방향의 벡터를 구하기 때문에
        //x의 값이 음수로 구해지고, 이것을 방지하기 위해 -1을 곱해줌.
        //rigid.velocity의 y값이 고정이 되는지, 점프하고 내려오는데 시간이 오래걸려서
        //x축으로만 힘을 주도록 변경
        if (isOnSlope)
        {
            rigid.velocity = new Vector2(slopePower * horizontalInput * -1, rigid.velocity.y);
        }
        else if (!isOnSlope)
        {
            rigid.velocity = new Vector2(horizontalInput * maxSpeed, rigid.velocity.y);
        }

        rigid.velocity = new Vector2(horizontalInput * maxSpeed, rigid.velocity.y);
    }
    //슬로프에서 멈춰있을 때 미끄러지지 않도록 해주는 함수
    private void StopSlope()
    {
        //방향키의 입력이 있을 시
        if (horizontalInput != 0)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX
                              | RigidbodyConstraints2D.FreezeRotation;
        }
    }
}