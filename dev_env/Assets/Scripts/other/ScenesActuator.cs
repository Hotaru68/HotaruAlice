using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;


public enum ActiveGameScenes
{
    Title=0,
    SelectMode = 1,
    Ranking = 2,
    Main = 3,
    GameOrver = 4,
    Tutorial = 5
}

public class ScenesActuator : MonoBehaviour
{
    [SerializeField]private ActiveGameScenes targetGameScenes;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Load_Scene()
    {
        SceneManager.LoadScene(targetGameScenes.ToString());
    }

}
