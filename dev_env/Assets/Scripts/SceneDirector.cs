using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    private Scene currentScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 指定したシーンを読み込むメソッド
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScenePreset()
    {
        if (currentScene.name == "Title") {
            SceneManager.LoadScene("Main");
        }
        else if (currentScene.name == "GameOrver" || currentScene.name == "GameClear")
        {
            SceneManager.LoadScene("Title");
        }
        
    }

}
