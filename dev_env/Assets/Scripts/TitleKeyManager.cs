using UnityEngine;

public class TitleKeyManager : MonoBehaviour
{
    private SceneDirector sceneDirector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneDirector = GetComponent<SceneDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            sceneDirector.LoadScene("SC Demo Scene - Village Props");
                   
        }
    }
}
