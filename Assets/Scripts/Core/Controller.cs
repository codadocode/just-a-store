using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        loadGlobalInstances();
        changeCursorState();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            changeCursorState();
        }
    }

    public void changeCursorState()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            //Debug.Log("Mostrou o mouse...");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Cursor.lockState == CursorLockMode.None)
        {
            //Debug.Log("Escondeu o mouse...");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private void loadGlobalInstances()
    {
        GameConfig.actualCanvas = GameObject.FindObjectOfType<Canvas>();
        GameConfig.actualPlayer = GameObject.FindObjectOfType<Player>();
        GameConfig.actualSun = GameObject.FindObjectOfType<Sun>();
        GameConfig.actualController = this;
    }
}
