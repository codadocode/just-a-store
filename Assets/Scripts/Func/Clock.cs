using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private GameObject minPointer;
    private GameObject hourPointer;
    // Start is called before the first frame update
    void Start()
    {
        this.minPointer = GameObject.Find("Ponteiro Maior");
        this.hourPointer = GameObject.Find("Ponteiro Menor");
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameConfig.inPause)   {
            if (GameConfig.actualSun.working())   {
                hourPointer.transform.Rotate(Vector3.down, (GameConfig.actualSun.getRotationSpeed() * 2));
                minPointer.transform.Rotate(Vector3.down, (GameConfig.actualSun.getRotationSpeed() * 8));
            }
        }
    }
}
