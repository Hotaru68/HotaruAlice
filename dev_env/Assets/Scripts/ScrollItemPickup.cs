using System.Collections.Generic;
using UnityEngine;
using System;

public class ScrollItemPickup : MonoBehaviour
{
    public ScrollItem scrollItem; // 拾うアイテムを指定
    private bool isInContact = false;
    private PlayerInventory playerInventory = null;

    
    public event Action OnObjectDestroyed;

    private void Update()
    {

        if (isInContact && Input.GetKeyDown(KeyCode.E))
        {
            try
            {
                playerInventory.CountScrollItem(scrollItem.ID, scrollItem.quantity);
            }
            catch
            {
                Debug.Log("スクロールを拾うときにエラーが発生しました。");
            }

            Destroy(gameObject); // アイテムをシーンから削除

        }
    }

    private void OnDestroy()
    {
        OnObjectDestroyed?.Invoke();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInContact = true;
            // プレイヤーがインベントリを持っていると仮定
            playerInventory = other.GetComponent<PlayerInventory>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInContact = false;
        }

        // プレイヤーがインベントリを持っていると仮定
        playerInventory = null;
    }




}
