using System.Xml.Serialization;
using UnityEngine;

public class EnemyScoreHandler : MonoBehaviour
{
    private MagicEffectManager magicEffectManager = null;
    private PlayerInfomationUI playerInfomationUI = null;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInfomationUI = PlayerInfomationUI.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            // プレイヤーがインベントリを持っていると仮定
            magicEffectManager = other.GetComponent<MagicEffectManager>();
            playerInfomationUI.UpdateMoney(100);

        }
    }

}
