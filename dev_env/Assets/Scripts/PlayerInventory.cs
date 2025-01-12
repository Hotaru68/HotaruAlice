using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public List<ScrollItem> scrollItem = new List<ScrollItem>();
    private UIManager uiManager = null;

    private void Start()
    {
        //CountScrollItem("aaa", 1);
        uiManager = UIManager.Instance;
        foreach (ScrollItem scroll in scrollItem)
        {
            uiManager.UpdateScrollUI(scroll.ID, scrollItem[scroll.ID].scrollName, scroll.quantity);
        }
        
    }
    /*
    thinking
    muscle
    behavior
    */

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void CountScrollItem(int id,int quantity)
    {
        foreach (ScrollItem scroll in scrollItem)
        {
            if (id == scroll.ID)
            {
                scroll.quantity += quantity;
                uiManager.UpdateScrollUI(scroll.ID, scrollItem[scroll.ID].scrollName, scroll.quantity);
            }
            
        }
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public void ShowInventory()
    {
        // インベントリの表示ロジックを実装
        foreach (Item item in items)
        {
            Debug.Log(item.itemName);
        }
    }

}
