using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float jumpForce = 5f; // �W�����v�̗�
    private Rigidbody2D rb;
    private bool isGrounded; // �n�ʂɂ��邩�ǂ����𔻒f����t���O
    public Transform groundCheck; // �n�ʂ��`�F�b�N���邽�߂�Transform
    public float groundCheckRadius = 0.2f; // �`�F�b�N���锼�a
    public LayerMask groundLayer; // �n�ʂ̃��C���[


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���̈ړ�����
        if (Input.GetKey(KeyCode.RightArrow))
        {// �E�����̈ړ�����
            Vector2 pos = transform.position;
            pos.x += 0.05f;
            transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {// �������̈ړ�����
            Vector2 pos = transform.position;
            pos.x -= 0.05f;
            transform.position = pos;
        }

        // �W�����v���͂̏���
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
            // �n�ʂɂ��邩�ǂ����̃`�F�b�N
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

}
