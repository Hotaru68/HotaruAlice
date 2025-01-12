using UnityEngine;
using System.Text;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;


[Serializable]
public class RankingEntry
{
    public string id;
    public int score;  // スコアを整数型に変換
    public string username;
}


[Serializable]
public class RankingResponse
{
    public List<RankingEntry> ranking;
}


public class DisplayRanking : MonoBehaviour
{
    //[SerializeField] private Transform rankingUI = null;
    //[SerializeField] private GameObject userText = null;

    [SerializeField] private GameObject rankEntryPrefab;  // RankEntryプレハブ
    [SerializeField] private Transform contentTransform;   // ContentオブジェクトのTransform

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 送信するデータを作成
        PostData data = new PostData("0", "aaa");

        // API GatewayのエンドポイントURL
        string apiUrl = "???";

        // POSTリクエストを送信
        //StartCoroutine(SendPostRequest(apiUrl, data));

        StartCoroutine(SendGetRequest(apiUrl));
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //IEnumerator SendPostRequest(string url, PostData postData)
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
            //Debug.Log("Response: " + request.downloadHandler.text);
            // デシリアライズ
            RankingResponse rankingData = JsonUtility.FromJson<RankingResponse>(request.downloadHandler.text);

            // ランキングデータをソート
            List<RankingEntry> sortedRanking = SortRanking(rankingData.ranking);

            // 結果を表示
            DisplayRanking(sortedRanking);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }

        // ランキングをscoreで降順にソート
        List<RankingEntry> SortRanking(List<RankingEntry> ranking)
        {
            ranking.Sort((a, b) => b.score.CompareTo(a.score)); // 降順ソート
            return ranking;
        }


        //ランキングを表示
        void DisplayRanking(List<RankingEntry> sortedRanking)
        {
            /* 既存のランキングをクリア
            foreach (Transform child in rankingUI)
            {
                Destroy(child.gameObject);
            }
            */

            /*/ 新しいランキングを生成
            for (int i = 0; i < sortedRanking.Count; i++)
            {
                RankingEntry entry = sortedRanking[i];

                // ランキングテキストを生成
                GameObject textObj = Instantiate(userText, rankingUI);
                TextMeshProUGUI text = textObj.GetComponent<TextMeshProUGUI>();
                text.text = $"Rank {i + 1}: {entry.username} - Score: {entry.score}";
            }
            */
            // 新しいランキングを生成


            foreach (Transform child in contentTransform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < sortedRanking.Count; i++)
            {
                RankingEntry entry = sortedRanking[i];

                // ランキングテキストを生成
                GameObject textObj = Instantiate(rankEntryPrefab, contentTransform);
                TextMeshProUGUI text = textObj.GetComponent<TextMeshProUGUI>();
                text.text = $"Rank {i + 1}: {entry.username} - Score: {entry.score}";
            }



        }
    }

    IEnumerator SendGetRequest(string url)
    {

        // UnityWebRequestを作成
        UnityWebRequest request = new UnityWebRequest(url, "GET");
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // リクエスト送信
        yield return request.SendWebRequest();

        // 結果を処理
        if (request.result == UnityWebRequest.Result.Success)
        {
            //Debug.Log("Response: " + request.downloadHandler.text);
            // デシリアライズ
            RankingResponse rankingData = JsonUtility.FromJson<RankingResponse>(request.downloadHandler.text);

            // ランキングデータをソート
            List<RankingEntry> sortedRanking = SortRanking(rankingData.ranking);

            // 結果を表示
            DisplayRanking(sortedRanking);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }

        // ランキングをscoreで降順にソート
        List<RankingEntry> SortRanking(List<RankingEntry> ranking)
        {
            ranking.Sort((a, b) => b.score.CompareTo(a.score)); // 降順ソート
            return ranking;
        }

        // ランキングを表示
        void DisplayRanking(List<RankingEntry> sortedRanking)
        {
            // 既存のランキングをクリア
            foreach (Transform child in contentTransform)
            {
                Destroy(child.gameObject);
            }

            int rank = 1;
            // 新しいランキングを生成
            for (int i = 0; i < sortedRanking.Count; i++)
            {
                RankingEntry entry = sortedRanking[i];

                // 前のエントリが存在し、スコアが同じ場合は順位を変えない
                if (i > 0 && sortedRanking[i].score != sortedRanking[i - 1].score)
                {
                    rank = i + 1; // スコアが異なる場合のみ順位を更新
                }

                // ランキングテキストを生成
                GameObject textObj = Instantiate(rankEntryPrefab, contentTransform);
                TextMeshProUGUI text = textObj.GetComponent<TextMeshProUGUI>();
                text.text = $"Rank {rank}: {entry.username} - Score: {entry.score}";
            }
        }
    }
}
