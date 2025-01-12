using UnityEngine;
using System;

public class DeliveredItemsHandler : MonoBehaviour
{
    private DeliveredItemsInfomationAdmin m_Admin = null;
    private PlayerAssets m_PlayerAssets = null;
    private bool isInContact = false;
    [SerializeField]private int itemID = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Admin = DeliveredItemsInfomationAdmin.Instance;
        m_PlayerAssets = PlayerAssets.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInContact)
        {
            try
            {
                m_Admin.AddDeliveredItemsQuntity(itemID);
                m_PlayerAssets.UpdateNegativePoint(1);
                OnObjectDestroyed?.Invoke();
                Destroy(gameObject); // アイテムをシーンから削除
            }
            catch
            {
                Debug.Log("アイテムを拾うときにエラーが発生しました。");
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInContact = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInContact = false;
        }
    }

    public event Action OnObjectDestroyed;

}
