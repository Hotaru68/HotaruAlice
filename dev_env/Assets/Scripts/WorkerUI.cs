using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;

public class WorkerUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject itemPrefab; // �v���n�u���C���X�y�N�^����ݒ�
    public Transform content; // Content�I�u�W�F�N�g���C���X�y�N�^����ݒ�
    public ScrollRect scrollRect; // ScrollRect�R���|�[�l���g���C���X�y�N�^����ݒ�

    private void Start()
    {
       
    }

    public void AddItem(string itemName,int requestQuantity)
    {
        // �v���n�u����V����UI�A�C�e���𐶐�
        GameObject newItem = Instantiate(itemPrefab, content);

        // �V�������������A�C�e���Ƀe�L�X�g��ݒ�
        TextMeshProUGUI itemText = newItem.GetComponent<TextMeshProUGUI>(); 
        itemText.SetText(itemName+ ":" + requestQuantity.ToString());

        // �X�N���[���ʒu���X�V - �K�v�ɉ����Ē���
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0; // ��ԉ��ɃX�N���[��
    }
}
