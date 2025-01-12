using System.Collections.Generic;
using UnityEngine;

public class ClientGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> clientList = new List<GameObject>();
    private bool isDelaying = false;

    [SerializeField]
    private float timeToGenerate = 3f;

    private float elapsedTime = 0f;
    //クライアントが生成される場所
    [SerializeField] private List<GameObject> generatePosition = new List<GameObject>();

    [SerializeField]
    private int maxObjects = 3;
    private int currentObjectCount = 0;

    private Dictionary<int, bool> generatePosStatusInfo = new Dictionary<int, bool>();

    void Start()
    {
        //リスポーン場所の状態を初期化
        for (int i = 0; i < generatePosition.Count; ++i) 
        {
            generatePosStatusInfo.Add(i, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (maxObjects <= currentObjectCount) { return; }
        AddTime();
        if (isDelaying) { return; }
        GenerateClient();
        isDelaying = true;
    }

    private void AddTime()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= timeToGenerate)
        {
            isDelaying = false;
            elapsedTime = 0f;
        }
    }

    private void GenerateClient()
    {
        bool successfullyGenerated = false;

        while (!successfullyGenerated)
        {
            int generatePositionIndex = Random.Range(0, generatePosition.Count);
            if (generatePosStatusInfo[generatePositionIndex]) { continue; }
            generatePosStatusInfo[generatePositionIndex] = true;
            successfullyGenerated = true;

            GameObject newObject = Instantiate(clientList[Random.Range(0, clientList.Count)],
                     new Vector2(generatePosition[generatePositionIndex].transform.position.x,
                        generatePosition[generatePositionIndex].transform.position.y),
            Quaternion.identity);
            currentObjectCount++;

            newObject.GetComponent<ClientRequest>().OnObjectDestroyed += HandleObjectDestroyed;
            newObject.GetComponent<ClientRequest>().generateIndex = generatePositionIndex;
        }
    }

    // オブジェクトが破棄されたときに呼び出される関数
    void HandleObjectDestroyed(int index)
    {
        generatePosStatusInfo[index] = false;
        currentObjectCount--;
    }
}
