using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

[DefaultExecutionOrder(-2)]
public class ClientUI : MonoBehaviour
{
    [SerializeField] private GameObject requestItemUI;
    [SerializeField] Transform parentTransform; // �\����̐eTransform

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
            // �A�C�e������ݒ�
            itemImage.sprite = m_Admin.deliveredItems[material.Key].icon; // �A�C�e���̉摜���Z�b�g
            requestQuantity.text = "x" + material.Value.ToString(); // �A�C�e�������Z�b�g
        }
    }

    void Old_SetupDeliveredItemQuantity()
    {
        foreach (DeliveredItems item in m_Admin.deliveredItems)
        {
            GameObject newItem = Instantiate(requestItemUI, parentTransform);
            Image itemImage = newItem.GetComponentInChildren<Image>();
            TextMeshProUGUI itemName = newItem.GetComponentInChildren<TextMeshProUGUI>();

            // �A�C�e������ݒ�
            itemImage.sprite = item.icon; // �A�C�e���̉摜���Z�b�g
            itemName.text = "x" + item.quantity.ToString(); // �A�C�e�������Z�b�g

            requestDeliveredItemsUI.Add(itemName);
        }
    }



}
