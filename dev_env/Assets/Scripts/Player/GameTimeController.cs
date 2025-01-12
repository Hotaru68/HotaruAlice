using UnityEngine;

public class GameTimeController : MonoBehaviour
{
    [SerializeField]
    private float timeLimit = 60f;
    private float timeRemaining;
    private SceneDirector sceneDirector;
    private PlayerInfomationUI playerInfomationUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInfomationUI = PlayerInfomationUI.Instance;
        timeRemaining = timeLimit; // c‚èŠÔ‚ğ‰Šú‰»
        //playerInfomationUI.UpdateTimeLimit(timeRemaining);

        sceneDirector = GetComponent<SceneDirector>();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeRemaining();
    }


    private void UpdateTimeRemaining() { 
        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime; // c‚èŠÔ‚ğŒ¸­
            //playerInfomationUI.UpdateTimeLimit(timeRemaining);
        }
        else
        {
            sceneDirector.LoadScene("GameOrver");
        }
    
    }

}
