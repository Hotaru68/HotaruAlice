using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target; // �Ǐ]����^�[�Q�b�g�i�v���C���[�L�����N�^�[�Ȃǁj
    public float smoothSpeed = 0.125f; // �X���[�X�ȓ����̑��x
    public Vector3 offset; // �J�����̃I�t�Z�b�g
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            // Z���W�͏�ɌŒ肷��i2D�Ȃ̂�Z��ς��Ȃ��j
            desiredPosition.z = transform.position.z;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
