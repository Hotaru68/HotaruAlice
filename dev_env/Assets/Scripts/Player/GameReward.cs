using UnityEngine;
using System.Text;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;


// POST�f�[�^�p�̃N���X���`
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
            rewardUI.SetActive(false); // UI�̏�����Ԃ�ݒ�
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
        // ���͂������[�U�[���̎擾
        UserLoginData.userName = UserNameTextBox.text;
        string score = this.totalMoney.ToString();

        // ���M����f�[�^���쐬
        PostData data = new PostData(score, UserLoginData.userName);

        // API Gateway�̃G���h�|�C���gURL
        string apiUrl = "???";

        // POST���N�G�X�g�𑗐M
        StartCoroutine(SendPostRequest(apiUrl, data));
        // �v���C��ʂ֑J��
        //SceneManager.LoadScene("RankingScene");
        //Time.timeScale = 1;
        scenesActuator.Load_Scene();
    }

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
            Debug.Log("Response: " + request.downloadHandler.text);
            // �f�V���A���C�Y
            RankingResponse rankingData = JsonUtility.FromJson<RankingResponse>(request.downloadHandler.text);


            // ���ʂ�\��
            //DisplayRanking(sortedRanking);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }


}
