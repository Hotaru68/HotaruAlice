using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    private PlayerAssets playerAssets;

    private void Start()
    {
        playerAssets = PlayerAssets.Instance;
        // �ŏ���Camera1���A�N�e�B�u�ɂ���
        camera1.enabled = true;
        camera2.enabled = false;
    }

    private void Update()
    {
        // �X�y�[�X�L�[���������Ƃ��ɃJ������؂�ւ���
        if (Input.GetKeyDown(KeyCode.M) && playerAssets.activePlayingGame)
        {
            camera1.enabled = !camera1.enabled;
            camera2.enabled = !camera2.enabled;
        }
    }
}
