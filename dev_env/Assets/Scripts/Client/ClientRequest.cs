using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ClientRequest : MonoBehaviour
{
    private bool isInContact = false; //ターゲットと接触しているか確認
    [SerializeField] private int MaximumNumberRequired = 4;
    private ClientUI clientUI = null;
    private DeliveredItemsInfomationAdmin m_Admin = null;
    private PlayerAssets playerAssets = null;
    private PlayerInfomationUI playerInfoUI = null;
    private Dictionary<int, int> targetRequestItemInfo = new Dictionary<int, int>();

    [System.NonSerialized] public int generateIndex;


    void Start()
    {
        m_Admin = DeliveredItemsInfomationAdmin.Instance;
        playerAssets = PlayerAssets.Instance;
        playerInfoUI = PlayerInfomationUI.Instance;

        int[] elements = { 0, 1, 2 };
        int NumberMaterialsRequired = UnityEngine.Random.Range(1, 4);

        foreach (int itemID in GetRandomElements(elements, NumberMaterialsRequired))
        {
            targetRequestItemInfo.Add(itemID, UnityEngine.Random.Range(1, MaximumNumberRequired));
        }

        clientUI = this.GetComponent<ClientUI>();
        clientUI.SetupDeliveredItemQuantity(targetRequestItemInfo);


    }

    // Update is called once per frame
    void Update()
    {
        if (isInContact && Input.GetKeyDown(KeyCode.E))
        {
            ConfirmatioOfDeliverables();
        }

        if (isInContact && Input.GetKeyDown(KeyCode.Q))
        {
            BehaviorWhenRequestIsRefused();
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInContact = true;
            // プレイヤーがインベントリを持っていると仮定
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInContact = false;
        }
    }

    private int[] GetRandomElements(int[] sourceArray, int numberOfElements)
    {
        // 使用されるインデックスを追跡するための配列を作成
        int[] selectedIndices = new int[numberOfElements];
        bool[] indexUsed = new bool[sourceArray.Length];
        int count = 0;

        while (count < numberOfElements)
        {
            int randomIndex = UnityEngine.Random.Range(0, sourceArray.Length);

            // まだ使用されていなければ、選択インデックスに追加
            if (!indexUsed[randomIndex])
            {
                selectedIndices[count] = randomIndex;
                indexUsed[randomIndex] = true;
                count++;
            }
        }

        // 選ばれたインデックスから要素を取得して配列にする
        int[] result = new int[numberOfElements];
        for (int i = 0; i < numberOfElements; i++)
        {
            result[i] = sourceArray[selectedIndices[i]];
        }

        return result;
    }

    public event Action<int> OnObjectDestroyed;

    private void ConfirmatioOfDeliverables()
    {
        //アイテムの数が足りているか確認
        foreach (var item in targetRequestItemInfo)
        {
            if (!(m_Admin.deliveredItems[item.Key].quantity >= item.Value))
            {
                return;
            }

        }
        //対象となっているアイテムをプレイヤーから消費する
        foreach (var item in targetRequestItemInfo)
        {
            m_Admin.deliveredItems[item.Key].quantity -= item.Value;
            playerInfoUI.UpdateDeliveredItemQuantity(item.Key, m_Admin.deliveredItems[item.Key].quantity);
            playerAssets.UpdateNegativePoint(-item.Value);
        }

        playerAssets.UpdateMoney(100);
        playerAssets.UpdatePositivePoint(5);
        playerAssets.motivity += 5;
        OnObjectDestroyed?.Invoke(generateIndex); // 引数として生成インデックスを渡す
        Destroy(gameObject);
    }

    private void BehaviorWhenRequestIsRefused()//依頼を断った時の振る舞い
    {
        playerAssets.UpdateNegativePoint(5);
        playerAssets.UpdatePositivePoint(-3);
        OnObjectDestroyed?.Invoke(generateIndex); // 引数として生成インデックスを渡す
        Destroy(gameObject);
    }

}
