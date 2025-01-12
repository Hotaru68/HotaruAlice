using UnityEngine;

public class KeyManager : MonoBehaviour
{
    private ScoreManager scoreManager = null;
    [System.NonSerialized]
    public bool interact = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            this.interact = true;
        }
    }
}
