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
    public int score;  // �X�R�A�𐮐��^�ɕϊ�
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

    [SerializeField] private GameObject rankEntryPrefab;  // RankEntry�v���n�u
    [SerializeField] private Transform contentTransform;   // Content�I�u�W�F�N�g��Transform

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ���M����f�[�^���쐬
        PostData data = new PostData("0", "aaa");

        // API Gateway�̃G���h�|�C���gURL
        string apiUrl = "???";

        // POST���N�G�X�g�𑗐M
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
        // JSON�f�[�^���쐬
        string jsonData = JsonUtility.ToJson(postData);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        // UnityWebRequest���쐬
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // �K�v�ł����API�L�[��ݒ�
        // if (!string.IsNullOrEmpty(apiKey))
        // {
        //     request.SetRequestHeader("x-api-key", apiKey);
        // }

        // ���N�G�X�g���M
        yield return request.SendWebRequest();

        // ���ʂ�����
        if (request.result == UnityWebRequest.Result.Success)
        {
            //Debug.Log("Response: " + request.downloadHandler.text);
            // �f�V���A���C�Y
            RankingResponse rankingData = JsonUtility.FromJson<RankingResponse>(request.downloadHandler.text);

            // �����L���O�f�[�^���\�[�g
            List<RankingEntry> sortedRanking = SortRanking(rankingData.ranking);

            // ���ʂ�\��
            DisplayRanking(sortedRanking);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }

        // �����L���O��score�ō~���Ƀ\�[�g
        List<RankingEntry> SortRanking(List<RankingEntry> ranking)
        {
            ranking.Sort((a, b) => b.score.CompareTo(a.score)); // �~���\�[�g
            return ranking;
        }


        //�����L���O��\��
        void DisplayRanking(List<RankingEntry> sortedRanking)
        {
            /* �����̃����L���O���N���A
            foreach (Transform child in rankingUI)
            {
                Destroy(child.gameObject);
            }
            */

            /*/ �V���������L���O�𐶐�
            for (int i = 0; i < sortedRanking.Count; i++)
            {
                RankingEntry entry = sortedRanking[i];

                // �����L���O�e�L�X�g�𐶐�
                GameObject textObj = Instantiate(userText, rankingUI);
                TextMeshProUGUI text = textObj.GetComponent<TextMeshProUGUI>();
                text.text = $"Rank {i + 1}: {entry.username} - Score: {entry.score}";
            }
            */
            // �V���������L���O�𐶐�


            foreach (Transform child in contentTransform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < sortedRanking.Count; i++)
            {
                RankingEntry entry = sortedRanking[i];

                // �����L���O�e�L�X�g�𐶐�
                GameObject textObj = Instantiate(rankEntryPrefab, contentTransform);
                TextMeshProUGUI text = textObj.GetComponent<TextMeshProUGUI>();
                text.text = $"Rank {i + 1}: {entry.username} - Score: {entry.score}";
            }



        }
    }

    IEnumerator SendGetRequest(string url)
    {

        // UnityWebRequest���쐬
        UnityWebRequest request = new UnityWebRequest(url, "GET");
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // ���N�G�X�g���M
        yield return request.SendWebRequest();

        // ���ʂ�����
        if (request.result == UnityWebRequest.Result.Success)
        {
            //Debug.Log("Response: " + request.downloadHandler.text);
            // �f�V���A���C�Y
            RankingResponse rankingData = JsonUtility.FromJson<RankingResponse>(request.downloadHandler.text);

            // �����L���O�f�[�^���\�[�g
            List<RankingEntry> sortedRanking = SortRanking(rankingData.ranking);

            // ���ʂ�\��
            DisplayRanking(sortedRanking);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }

        // �����L���O��score�ō~���Ƀ\�[�g
        List<RankingEntry> SortRanking(List<RankingEntry> ranking)
        {
            ranking.Sort((a, b) => b.score.CompareTo(a.score)); // �~���\�[�g
            return ranking;
        }

        // �����L���O��\��
        void DisplayRanking(List<RankingEntry> sortedRanking)
        {
            // �����̃����L���O���N���A
            foreach (Transform child in contentTransform)
            {
                Destroy(child.gameObject);
            }

            int rank = 1;
            // �V���������L���O�𐶐�
            for (int i = 0; i < sortedRanking.Count; i++)
            {
                RankingEntry entry = sortedRanking[i];

                // �O�̃G���g�������݂��A�X�R�A�������ꍇ�͏��ʂ�ς��Ȃ�
                if (i > 0 && sortedRanking[i].score != sortedRanking[i - 1].score)
                {
                    rank = i + 1; // �X�R�A���قȂ�ꍇ�̂ݏ��ʂ��X�V
                }

                // �����L���O�e�L�X�g�𐶐�
                GameObject textObj = Instantiate(rankEntryPrefab, contentTransform);
                TextMeshProUGUI text = textObj.GetComponent<TextMeshProUGUI>();
                text.text = $"Rank {rank}: {entry.username} - Score: {entry.score}";
            }
        }
    }
}
