using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    private PlayerAssets playerAssets;

    private void Start()
    {
        playerAssets = PlayerAssets.Instance;
        // 最初はCamera1をアクティブにする
        camera1.enabled = true;
        camera2.enabled = false;
    }

    private void Update()
    {
        // スペースキーを押したときにカメラを切り替える
        if (Input.GetKeyDown(KeyCode.M) && playerAssets.activePlayingGame)
        {
            camera1.enabled = !camera1.enabled;
            camera2.enabled = !camera2.enabled;
        }
    }
}
