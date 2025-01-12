using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ClientRequest : MonoBehaviour
{
    private bool isInContact = false; //�^�[�Q�b�g�ƐڐG���Ă��邩�m�F
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
            // �v���C���[���C���x���g���������Ă���Ɖ���
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
        // �g�p�����C���f�b�N�X��ǐՂ��邽�߂̔z����쐬
        int[] selectedIndices = new int[numberOfElements];
        bool[] indexUsed = new bool[sourceArray.Length];
        int count = 0;

        while (count < numberOfElements)
        {
            int randomIndex = UnityEngine.Random.Range(0, sourceArray.Length);

            // �܂��g�p����Ă��Ȃ���΁A�I���C���f�b�N�X�ɒǉ�
            if (!indexUsed[randomIndex])
            {
                selectedIndices[count] = randomIndex;
                indexUsed[randomIndex] = true;
                count++;
            }
        }

        // �I�΂ꂽ�C���f�b�N�X����v�f���擾���Ĕz��ɂ���
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
        //�A�C�e���̐�������Ă��邩�m�F
        foreach (var item in targetRequestItemInfo)
        {
            if (!(m_Admin.deliveredItems[item.Key].quantity >= item.Value))
            {
                return;
            }

        }
        //�ΏۂƂȂ��Ă���A�C�e�����v���C���[��������
        foreach (var item in targetRequestItemInfo)
        {
            m_Admin.deliveredItems[item.Key].quantity -= item.Value;
            playerInfoUI.UpdateDeliveredItemQuantity(item.Key, m_Admin.deliveredItems[item.Key].quantity);
            playerAssets.UpdateNegativePoint(-item.Value);
        }

        playerAssets.UpdateMoney(100);
        playerAssets.UpdatePositivePoint(5);
        playerAssets.motivity += 5;
        OnObjectDestroyed?.Invoke(generateIndex); // �����Ƃ��Đ����C���f�b�N�X��n��
        Destroy(gameObject);
    }

    private void BehaviorWhenRequestIsRefused()//�˗���f�������̐U�镑��
    {
        playerAssets.UpdateNegativePoint(5);
        playerAssets.UpdatePositivePoint(-3);
        OnObjectDestroyed?.Invoke(generateIndex); // �����Ƃ��Đ����C���f�b�N�X��n��
        Destroy(gameObject);
    }

}
