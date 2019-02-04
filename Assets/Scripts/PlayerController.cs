using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Transform playerTrans;
    Rigidbody2D playerRB;

    [SerializeField] float playerMoveSpeed;
    [SerializeField] float jumpPower;

    [SerializeField] float gravityForce;

    [SerializeField] LayerMask groundCheckLM;

    bool isGround;
    bool checkGround;

    // アニメーション用
    private Animator animator;


    void Awake()
    {
        playerTrans = transform;
        playerRB = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        isGround = true;
        checkGround = false;

        animator = GetComponent<Animator>(); // アニメータの取得
    }


    void Update()
    {
        if (!isGround && checkGround)
        {
            RaycastHit2D hit = Physics2D.Raycast(playerTrans.position, Vector2.down, 2.0f, groundCheckLM);
            if (hit) isGround = true;
        }
        animator.SetBool("is_running", false);

        float dir = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(dir) > 0.01f)
            PlayerMove(dir);
        if (Input.GetKeyDown(KeyCode.UpArrow))
            PlayerJump();
    }

    void FixedUpdate()
    {
        playerRB.AddForce(Vector2.down * gravityForce);
    }

    void PlayerMove(float dir)
    {
        animator.SetBool("is_running", true);
        playerTrans.position += Vector3.right * dir * playerMoveSpeed * Time.deltaTime;

        // プレーヤーの描画向き変更
        if (dir > 0) playerTrans.localScale = new Vector3(2.0f, 2.0f, 2.0f);
        else playerTrans.localScale = new Vector3(-2.0f, 2.0f, 2.0f);
    }

    void PlayerJump()
    {
        if (isGround)
        {
            animator.SetTrigger("is_jumping");
            playerRB.velocity = new Vector3(playerRB.velocity.x, 0, 0);
            playerRB.AddForce(Vector2.up * jumpPower);
            isGround = false;
            checkGround = false;
            Invoke("EnableCheckGround", 0.2f);
        }
    }

    void EnableCheckGround()
    {
        checkGround = true;
    }
}
