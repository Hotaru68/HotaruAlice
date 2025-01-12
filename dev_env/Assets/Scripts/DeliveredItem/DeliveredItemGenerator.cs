using System.Collections.Generic;
using UnityEngine;

public class DeliveredItemGenerator : MonoBehaviour
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


    void Start()
    {

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
        int generatePositionIndex = Random.Range(0, generatePosition.Count);

        GameObject newObject = Instantiate(clientList[Random.Range(0, clientList.Count)],
                 new Vector2(generatePosition[generatePositionIndex].transform.position.x,
                    generatePosition[generatePositionIndex].transform.position.y),
        Quaternion.identity);
        currentObjectCount++;

        newObject.GetComponent<DeliveredItemsHandler>().OnObjectDestroyed += HandleObjectDestroyed;

    }

    // オブジェクトが破棄されたときに呼び出される関数
    void HandleObjectDestroyed()
    {
        currentObjectCount--;
    }


}
