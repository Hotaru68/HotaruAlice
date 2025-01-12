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

    public GameObject itemPrefab; // �v���n�u���C���X�y�N�^�[����Z�b�g
    public Transform parentTransform; // �\����̐eTransform

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

            // �A�C�e������ݒ�
            itemImage.sprite = item.icon; // �A�C�e���̉摜���Z�b�g
            itemName.text = "x" + item.quantity.ToString(); // �A�C�e�������Z�b�g

            deliveredItemsUI.Add(itemName);
        }
    }

    public void UpdateDeliveredItemQuantity(int itemID,int quantity)
    {
        deliveredItemsUI[itemID].text = "x" + quantity.ToString(); // �A�C�e���̐����Z�b�g
    }
}
