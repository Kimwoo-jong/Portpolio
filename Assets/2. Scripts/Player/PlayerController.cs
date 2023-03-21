using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator anim;

    [Header("플레이어 이동")]
    public float maxSpeed;                      //플레이어 속도 제한을 위한 값
    public float horizontalInput = 0f;

    [Header("플레이어 점프")]
    public float jumpPower;                     //플레이어 점프 시 가해지는 힘
    private bool isGrounded;                    //플레이어가 땅에 닿아 있는지 확인

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;

    [Header("플레이어 효과")]
    [SerializeField] private ParticleSystem playerEffect;        //플레이어의 움직임에 따라 다른 파티클이 실행될 예정
    [SerializeField] private GameObject jumpEffect;              //점프 이펙트 프리팹을 연결

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheck");
        //플레이어의 초기 위치 설정
        gameObject.transform.position = new Vector2(-7.0f, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = 5f;
        jumpPower = 12f;

        playerEffect = GetComponentInChildren<ParticleSystem>();
    }
    private void Update()
    {
        //속력이 급격히 줄어듦
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        Jump();
    }
    private void FixedUpdate()
    {
        Movement();
    }
    #region 이동 관련
    //플레이어의 이동 담당
    private void Movement()
    {
        //GetAxis()는 부드럽게 값을 받아옴, GetAxisRaw()는 즉시 값을 받아옴.
        float h = Input.GetAxisRaw("Horizontal");

        //좌우 키보드 입력값을 저장한다.
        horizontalInput = h;

        //ForceMode2D >> Impulse는 순간적인 힘, Force는 연속적인 힘을 가해준다.
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //우측 이동시
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        //좌측 이동시
        else if (rigid.velocity.x < maxSpeed * -1)
        {
            rigid.velocity = new Vector2(maxSpeed * -1, rigid.velocity.y);
        }
        //이동값이 0.2보다 작을 경우
        //좌 우측 이동을 다 신경쓰기 위해 절대값 함수 사용
        if (Mathf.Abs(rigid.velocity.x) < 0.2f)
        {
            anim.SetBool("isMove", false);
        }
        else
        {
            anim.SetBool("isMove", true);
        }

        //플레이어가 땅에 있을 때만 이펙트 생성
        if (isGrounded && Input.GetButton("Horizontal"))
        {
            CreateMoveDust();
        }
    }
    #endregion
    #region 점프 관련
    private void CheckSurrounding()
    {
        //원을 그려서 땅에 닿아있는지 확인
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
    private void Jump()
    {
        CheckSurrounding();

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
            CreateJumpDust();
        }
    }
    #endregion
    #region 이동효과
    private void CreateMoveDust()
    {
        playerEffect.Play();
    }
    private void CreateJumpDust()
    {
        Instantiate(jumpEffect, new Vector2(transform.position.x, transform.position.y - 0.1f), Quaternion.identity);
    }
    #endregion
    //플레이어 오브젝트가 땅에 충돌할 때 애니메이션을 종료해줘야 하기 때문
    private void OnCollisionEnter2D(Collision2D col)
    {
        //땅에 닿았는지 확인
        if (isGrounded)
        {
            anim.SetBool("isJump", false);
        }
        else if(col.gameObject.CompareTag("Platform"))
        {
            anim.SetBool("isJump", false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}