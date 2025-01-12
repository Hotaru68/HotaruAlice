using UnityEngine;
using System.Text;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;


// POSTデータ用のクラスを定義
[System.Serializable]
public class PostData
{
    public string score;
    public string username;

    public PostData(string score, string username)
    {
        this.score = score;
        this.username = username;
    }
}

[DefaultExecutionOrder(10)]
public class GameReward : MonoBehaviour
{
    [SerializeField] private GameObject rewardUI = null;
    [SerializeField] private TMP_InputField UserNameTextBox = null;
    private int totalMoney = 0;

    private ScenesActuator scenesActuator = null;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scenesActuator = GetComponent<ScenesActuator>();

        if (rewardUI != null)
        {
            rewardUI.SetActive(false); // UIの初期状態を設定
        }
        else
        {
            Debug.LogError("rewardUI has not been assigned in the inspector.");
        }
    }

    public void GetReward(int _totalMoney)
    {
        this.totalMoney = _totalMoney;
        rewardUI.SetActive(true);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
          
    }

    public void OnClickLoginButton()
    {
        // 入力したユーザー名の取得
        UserLoginData.userName = UserNameTextBox.text;
        string score = this.totalMoney.ToString();

        // 送信するデータを作成
        PostData data = new PostData(score, UserLoginData.userName);

        // API GatewayのエンドポイントURL
        string apiUrl = "???";

        // POSTリクエストを送信
        StartCoroutine(SendPostRequest(apiUrl, data));
        // プレイ画面へ遷移
        //SceneManager.LoadScene("RankingScene");
        //Time.timeScale = 1;
        scenesActuator.Load_Scene();
    }

    IEnumerator SendPostRequest(string url, PostData postData)
    {
        // JSONデータを作成
        string jsonData = JsonUtility.ToJson(postData);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        // UnityWebRequestを作成
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // 必要であればAPIキーを設定
        // if (!string.IsNullOrEmpty(apiKey))
        // {
        //     request.SetRequestHeader("x-api-key", apiKey);
        // }

        // リクエスト送信
        yield return request.SendWebRequest();

        // 結果を処理
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Response: " + request.downloadHandler.text);
            // デシリアライズ
            RankingResponse rankingData = JsonUtility.FromJson<RankingResponse>(request.downloadHandler.text);


            // 結果を表示
            //DisplayRanking(sortedRanking);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }


}
