using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public void enableGravity()
    {
        GetComponent<Rigidbody>().freezeRotation = false;
        GetComponent<Rigidbody>().useGravity = true;
    }
    public void disableGravity()
    {
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().useGravity = false;
    }
}
