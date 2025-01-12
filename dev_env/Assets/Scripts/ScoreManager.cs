using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    private UIManager uIManager = null;


    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("ScoreManager");
                _instance = go.AddComponent<ScoreManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.uIManager = this.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //AddScore(100);
    }


    // スコアを増やすメソッド
    public void AddScore(int points)
    {
        this.score += points;
        //Debug.Log(score);
        this.uIManager.UpdateScoreUI(score);
    }


}

