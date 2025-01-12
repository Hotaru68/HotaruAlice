using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-5)]
public class DeliveredItemsInfomationAdmin : MonoBehaviour
{
    public List<DeliveredItems> deliveredItems = new List<DeliveredItems>();
    private PlayerInfomationUI mUI;

    private static DeliveredItemsInfomationAdmin _instance;

    public static DeliveredItemsInfomationAdmin Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("DeliveredItemsInfomationAdmin");
                _instance = go.AddComponent<DeliveredItemsInfomationAdmin>();
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

    private void Start()
    {
        mUI = PlayerInfomationUI.Instance;
    }

    public void AddDeliveredItemsQuntity(int itemID)
    {
        deliveredItems[itemID].quantity += 1;
        mUI.UpdateDeliveredItemQuantity(itemID, deliveredItems[itemID].quantity);
    }
}
