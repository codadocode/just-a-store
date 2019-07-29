using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDoor : MonoBehaviour
{
    private bool isOpened = false;
    private Animator animator;
    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void openDoor()
    {
        if (!isOpened)
        {
            isOpened = true;
            Debug.Log("Abriu!");
            animator.SetBool("isOpened", isOpened);
        }
        else
        {
            isOpened = false;
            Debug.Log("Fechou!");
            animator.SetBool("isOpened", isOpened);
        }
    }
}
