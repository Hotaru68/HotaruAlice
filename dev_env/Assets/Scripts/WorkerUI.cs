using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;

public class WorkerUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject itemPrefab; // プレハブをインスペクタから設定
    public Transform content; // Contentオブジェクトをインスペクタから設定
    public ScrollRect scrollRect; // ScrollRectコンポーネントをインスペクタから設定

    private void Start()
    {
       
    }

    public void AddItem(string itemName,int requestQuantity)
    {
        // プレハブから新しいUIアイテムを生成
        GameObject newItem = Instantiate(itemPrefab, content);

        // 新しく生成したアイテムにテキストを設定
        TextMeshProUGUI itemText = newItem.GetComponent<TextMeshProUGUI>(); 
        itemText.SetText(itemName+ ":" + requestQuantity.ToString());

        // スクロール位置を更新 - 必要に応じて調整
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0; // 一番下にスクロール
    }
}
