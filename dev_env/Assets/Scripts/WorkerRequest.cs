using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorkerRequest : MonoBehaviour
{

    private bool isInContact = false;
    private KeyManager keyManager = null;
    private ScoreManager scoreManager = null;
    private UIManager uiManager = null;

    private PlayerInventory playerInventory = null;
    private WorkerUI workerUI = null;
    public List<ScrollItem> scrollItem = new List<ScrollItem>();

    private void Awake()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreManager = ScoreManager.Instance;
        uiManager = UIManager.Instance;

        this.workerUI = gameObject.GetComponent<WorkerUI>();
        if (this.workerUI == null)
        {
            Debug.Log("WorkerUI component is missing from this GameObject.");
        }
        else
        {
            this.DisplayUIRequestScroll();
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (isInContact && Input.GetKeyDown(KeyCode.E))
        {
            CheckScrollQuantity();
        }
    }

    private void DisplayUIRequestScroll()
    {
        foreach (ScrollItem item in scrollItem)
        {
            workerUI.AddItem(item.scrollName, item.quantity);
        }
        
    }
        

    private void CheckScrollQuantity()
    {
        //�X�N���[���̐�������Ă��邩�m�F
        foreach (ScrollItem scroll in scrollItem)
        {
            if (!(playerInventory.scrollItem[scroll.ID].quantity >= scroll.quantity))
            {
                return;
            }

        }
        //�ΏۂƂȂ��Ă���X�N���[�����v���C���[��������
        foreach (ScrollItem scroll in scrollItem)
        {
            playerInventory.scrollItem[scroll.ID].quantity -= scroll.quantity;
            uiManager.UpdateScrollUI(
                scroll.ID, 
                playerInventory.scrollItem[scroll.ID].scrollName,
                playerInventory.scrollItem[scroll.ID].quantity
                );
        }


        scoreManager.AddScore(100);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        //this.scoreManager.AddScore(100);
        // �g���K�[�ɐN�������I�u�W�F�N�g�̏����擾
        /*
        GameObject otherObject = collision.gameObject;

        // ���̃X�N���v�g����l���擾����i��FScoreManager�X�N���v�g����X�R�A���擾�j
        KeyManager keyManager = otherObject.GetComponent<KeyManager>();
        if (keyManager != null && keyManager.interact)
        {
            this.scoreManager.AddScore(100);
        }
        */
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
            // �v���C���[���C���x���g���������Ă���Ɖ���
            playerInventory = null;
        }
    }

}
