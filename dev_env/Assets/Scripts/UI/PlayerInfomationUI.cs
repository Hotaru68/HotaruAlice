using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using NUnit.Framework.Interfaces;
using System.Collections.Generic;
//using static UnityEditor.Progress;

public class PlayerInfomationUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private TextMeshProUGUI motivityText;
    [SerializeField] private TextMeshProUGUI positivePointText;
    [SerializeField] private TextMeshProUGUI negativePointText;

    private DeliveredItemsInfomationAdmin m_Admin = null;
    private static PlayerInfomationUI _instance;

    public GameObject itemPrefab; // プレハブをインスペクターからセット
    public Transform parentTransform; // 表示先の親Transform

    private List<TextMeshProUGUI> deliveredItemsUI = new List<TextMeshProUGUI>();

    public static PlayerInfomationUI Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("PlayerInfomationUI");
                _instance = go.AddComponent<PlayerInfomationUI>();
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
        m_Admin = DeliveredItemsInfomationAdmin.Instance;
        SetupDeliveredItemQuantity();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void UpdateMoney(int _money)
    {
        money.text = "SpendingMoney:" + _money.ToString();
    }

    public void UpdateMotivity(float currentMotivity)
    {
        motivityText.text = "Motivity:" + currentMotivity.ToString("F0");
    }

    public void UpdatePositivePoint(int positivePoint)
    {
        positivePointText.text = "Positive:" + positivePoint.ToString() + "/100";
    }

    public void UpdateNegativePoint(int negativePoint)
    {
        negativePointText.text = "Negative:" + negativePoint.ToString() + "/100";
    }


    void SetupDeliveredItemQuantity()
    {
        foreach (DeliveredItems item in m_Admin.deliveredItems)
        {
            GameObject newItem = Instantiate(itemPrefab, parentTransform);
            Image itemImage = newItem.GetComponentInChildren<Image>();
            TextMeshProUGUI itemName = newItem.GetComponentInChildren<TextMeshProUGUI>();

            // アイテム情報を設定
            itemImage.sprite = item.icon; // アイテムの画像をセット
            itemName.text = "x" + item.quantity.ToString(); // アイテム名をセット

            deliveredItemsUI.Add(itemName);
        }
    }

    public void UpdateDeliveredItemQuantity(int itemID,int quantity)
    {
        deliveredItemsUI[itemID].text = "x" + quantity.ToString(); // アイテムの数をセット
    }
}
