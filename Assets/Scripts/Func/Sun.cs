using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    private bool isWorking = true;
    [SerializeField]
    private float rotationSpeed = 0.005f;
    void Start()   {

    }
    void Update()   {
        if(!GameConfig.inPause)   {
            if (isWorking)   {
                this.transform.Rotate(Vector3.left, rotationSpeed);
            }
        }
    }
    public bool working()   {
        return this.isWorking;
    }
    public float getRotationSpeed()   {
        return this.rotationSpeed;
    }
}
