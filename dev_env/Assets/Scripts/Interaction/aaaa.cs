using UnityEngine;

public class aaaa : MonoBehaviour
{
    private PlayerInfomationUI playerInfomationUI = null;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInfomationUI = PlayerInfomationUI.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        playerInfomationUI.UpdateMoney(100);
    }
}
