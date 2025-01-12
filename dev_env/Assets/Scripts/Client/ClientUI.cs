using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

[DefaultExecutionOrder(-2)]
public class ClientUI : MonoBehaviour
{
    [SerializeField] private GameObject requestItemUI;
    [SerializeField] Transform parentTransform; // 表示先の親Transform

    private DeliveredItemsInfomationAdmin m_Admin = null;
    private List<TextMeshProUGUI> requestDeliveredItemsUI = new List<TextMeshProUGUI>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Admin = DeliveredItemsInfomationAdmin.Instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetupDeliveredItemQuantity(Dictionary<int,int> targetMaterials)
    {
        foreach (var material in targetMaterials)
        {
            GameObject newItem = Instantiate(requestItemUI, parentTransform);
            Image itemImage = newItem.GetComponentInChildren<Image>();
            TextMeshProUGUI requestQuantity = newItem.GetComponentInChildren<TextMeshProUGUI>();
            // アイテム情報を設定
            itemImage.sprite = m_Admin.deliveredItems[material.Key].icon; // アイテムの画像をセット
            requestQuantity.text = "x" + material.Value.ToString(); // アイテム名をセット
        }
    }

    void Old_SetupDeliveredItemQuantity()
    {
        foreach (DeliveredItems item in m_Admin.deliveredItems)
        {
            GameObject newItem = Instantiate(requestItemUI, parentTransform);
            Image itemImage = newItem.GetComponentInChildren<Image>();
            TextMeshProUGUI itemName = newItem.GetComponentInChildren<TextMeshProUGUI>();

            // アイテム情報を設定
            itemImage.sprite = item.icon; // アイテムの画像をセット
            itemName.text = "x" + item.quantity.ToString(); // アイテム名をセット

            requestDeliveredItemsUI.Add(itemName);
        }
    }



}
