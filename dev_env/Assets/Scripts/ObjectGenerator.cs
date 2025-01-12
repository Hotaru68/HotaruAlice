using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objList = new List<GameObject>();

    private bool isDelaying = false;

    [SerializeField]
    private float timeToGenerate = 3f;

    private float elapsedTime = 0f;

    [SerializeField]
    private List<GameObject> generatePosition = new List<GameObject>();

    //生成したアイテムを格納
    private List<GameObject> generateObject = new List<GameObject>();

    [SerializeField]
    private int maxObjects = 3;
    private int currentObjectCount = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (maxObjects < currentObjectCount) { return; }
        AddTime();
        Generator();
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

    private void Generator() {
        if (!isDelaying)
        {
            isDelaying = true;
            /*
            Instantiate(objList[Random.Range(0, 3)],
            new Vector2(this.transform.position.x + Random.Range(-5, 6), this.transform.position.y),
            Quaternion.identity);
            */
            int generatePositionIndex = Random.Range(0, generatePosition.Count);
            GameObject newObject = Instantiate(objList[Random.Range(0, objList.Count)],
                                 new Vector2(generatePosition[generatePositionIndex].transform.position.x,
                                    generatePosition[generatePositionIndex].transform.position.y),
                        Quaternion.identity);
            currentObjectCount++;

            newObject.GetComponent<ScrollItemPickup>().OnObjectDestroyed += HandleObjectDestroyed;
        }
    }

    // オブジェクトが破棄されたときに呼び出される関数
    void HandleObjectDestroyed()
    {
        currentObjectCount--;
    }

    IEnumerator DelayedAction(float delay)
    {
        yield return new WaitForSeconds(delay);
        isDelaying = false;
    }

}
