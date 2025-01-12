using UnityEngine;

public class OtherKeyController : MonoBehaviour
{
    [SerializeField]
    private KeyCode changeSceneKey = KeyCode.Space;
    private ScenesActuator scenesActuator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scenesActuator = GetComponent<ScenesActuator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(changeSceneKey)) { scenesActuator.Load_Scene(); }
    }

}
