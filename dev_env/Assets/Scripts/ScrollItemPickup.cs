using System.Collections.Generic;
using UnityEngine;
using System;

public class ScrollItemPickup : MonoBehaviour
{
    public ScrollItem scrollItem; // �E���A�C�e�����w��
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
                Debug.Log("�X�N���[�����E���Ƃ��ɃG���[���������܂����B");
            }

            Destroy(gameObject); // �A�C�e�����V�[������폜

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
            // �v���C���[���C���x���g���������Ă���Ɖ���
            playerInventory = other.GetComponent<PlayerInventory>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInContact = false;
        }

        // �v���C���[���C���x���g���������Ă���Ɖ���
        playerInventory = null;
    }




}
