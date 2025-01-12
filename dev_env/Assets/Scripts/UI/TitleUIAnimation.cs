using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class TitleUIAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI discriptionChangeScene = null;
    [SerializeField] private int loopingTimeInterval = 3;
    [SerializeField] private float currentTimeInterval = 0f;
    public float fadeDuration = 3.0f; // フェードする時間

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (discriptionChangeScene != null)
        {
            StartCoroutine(FadeText());
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTimeInterval += Time.deltaTime;
        if (loopingTimeInterval <= currentTimeInterval)
        {
            StartCoroutine(FadeText());
            currentTimeInterval = 0;
        }
    }

    private IEnumerator FadeText()
    {
        // フェードアウト
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            Color color = discriptionChangeScene.color;
            color.a = Mathf.Clamp01(1 - (t / fadeDuration)); // アルファを減少
            discriptionChangeScene.color = color; // アルファを設定
            yield return null;
        }

        // フェードイン
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            Color color = discriptionChangeScene.color;
            color.a = Mathf.Clamp01(t / fadeDuration); // アルファを増加
            discriptionChangeScene.color = color; // アルファを設定
            yield return null;
        }
    }
}
