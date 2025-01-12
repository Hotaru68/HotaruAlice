using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float jumpForce = 5f; // ジャンプの力
    private Rigidbody2D rb;
    private bool isGrounded; // 地面にいるかどうかを判断するフラグ
    public Transform groundCheck; // 地面をチェックするためのTransform
    public float groundCheckRadius = 0.2f; // チェックする半径
    public LayerMask groundLayer; // 地面のレイヤー


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 仮の移動処理
        if (Input.GetKey(KeyCode.RightArrow))
        {// 右方向の移動入力
            Vector2 pos = transform.position;
            pos.x += 0.05f;
            transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {// 左方向の移動入力
            Vector2 pos = transform.position;
            pos.x -= 0.05f;
            transform.position = pos;
        }

        // ジャンプ入力の処理
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }


    }


    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }


    void FixedUpdate()
    {
            // 地面にいるかどうかのチェック
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

}
