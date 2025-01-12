using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target; // 追従するターゲット（プレイヤーキャラクターなど）
    public float smoothSpeed = 0.125f; // スムースな動きの速度
    public Vector3 offset; // カメラのオフセット
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
            // Z座標は常に固定する（2DなのでZを変えない）
            desiredPosition.z = transform.position.z;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
