using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(+5)]
public class PlayerAssets : MonoBehaviour
{
    [System.NonSerialized] public int spendingMoney = 0;
    [System.NonSerialized] public int positivePoint = 0;
    [System.NonSerialized] public int negativePoint = 0;
    [System.NonSerialized] public float motivity = 0;

    [SerializeField] private int setupMotivity = 60;
    private static PlayerAssets _instance;
    private PlayerInfomationUI _ui;
    private SceneDirector sceneDirector;
    private GameReward gameReward;

    [SerializeField] private bool onTutorial = false;

    [System.NonSerialized] public bool activePlayingGame = true;

    public static PlayerAssets Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("PlayerAssets");
                _instance = go.AddComponent<PlayerAssets>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spendingMoney = 0;
        positivePoint = 0;
        negativePoint = 0;
        motivity = setupMotivity;
        _ui = PlayerInfomationUI.Instance;
        sceneDirector = GetComponent<SceneDirector>();
        gameReward = GetComponent<GameReward>();

        _ui.UpdateMotivity(motivity);
        UpdateMoney(0);
        UpdateMotivity(0);
        UpdatePositivePoint(positivePoint);
        UpdateNegativePoint(negativePoint);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateMotivity(0);
    }


    public void UpdateMoney(int money)
    {
        spendingMoney += (int)(money * (positivePoint * 0.05f + 1));
        _ui.UpdateMoney(spendingMoney);
    }


    private void UpdateMotivity(int _motivity)
    {
        if (onTutorial) { return; }
        motivity += _motivity;
        if (motivity > 0)
        {
            motivity -= Time.deltaTime * (1.0f + negativePoint * 0.01f);
            _ui.UpdateMotivity(motivity);
        }
        else
        {
            activePlayingGame = false;
            gameReward.GetReward(spendingMoney);
        }
    }

    public void UpdatePositivePoint(int point)
    {
        positivePoint += point;

        if (positivePoint > 100)
        {
            positivePoint = 100;
        }
        else if (positivePoint < 0)
        {
            positivePoint = 0;
        }
        _ui.UpdatePositivePoint(positivePoint);
    }

    public void UpdateNegativePoint(int point)
    {
        negativePoint += point;

        if (negativePoint > 100)
        {
            negativePoint = 100;
        }
        else if (negativePoint < 0)
        {
            negativePoint = 0;
        }
        _ui.UpdateNegativePoint(negativePoint);
    }
}
