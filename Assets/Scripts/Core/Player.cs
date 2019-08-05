using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool parado = false;
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float maxMoveSpeed = 12;
    [SerializeField]
    private float cameraSpeed = 5f;
    [SerializeField]
    private int life = 100;
    [SerializeField]
    private float cansaco = 0;
    private Rigidbody rb;
    private GameObject playerCamera;
    [SerializeField]
    private float rayCastDistance = 10;
    private Pickable hand = null;
    private Container actualContainer = null;
    private GameObject handPos;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        playerCamera = this.transform.Find("Player Camera").gameObject;
        handPos = playerCamera.transform.Find("Hand").gameObject;
    }
    private void Update()
    {
        if (!GameConfig.inPause && !this.parado)
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                cameraMouse();
            }
            checkInput();
        }
    }
    private void FixedUpdate()
    {
        if (!GameConfig.inPause && !this.parado)
        {
            movement();
            checkHand();
        }
    }
    private void checkInput()
    {
        if (Input.GetButtonDown("Use"))
        {
            if (hand != null)
            {
                this.hand.enableGravity();
                this.hand = null;
            }else
            {
                GameObject objectCollider = checkRaycast();
                if (objectCollider != null)
                {
                    if (objectCollider.GetComponent<CustomTag>().findTag("Pickable"))
                    {
                        //Debug.Log("Pegou alguma bosta...");
                        setHand(objectCollider.GetComponent<Pickable>());
                        this.hand.disableGravity();
                        //Debug.Log(this.hand.ToString());
                    }
                    else if (objectCollider.GetComponent<CustomTag>().findTag("Porta"))
                    {
                        objectCollider.GetComponent<SingleDoor>().openDoor();
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                if (hand != null)
                {
                    if (hand.GetComponent<CustomTag>().findTag("Product"))
                    {
                        print("Is a Product");
                        GameObject objectCollider = checkRaycast();
                        if (objectCollider != null)
                        {
                            if (objectCollider.GetComponent<CustomTag>().findTag("Container"))
                            {
                                objectCollider.GetComponent<Container>().guardarItem((Product)hand);
                                hand = null;
                            }
                        }
                    }
                    if (hand != null)
                    {
                        throwItem();
                    }
                }
            }else if (Cursor.lockState == CursorLockMode.None)
            {
                //print(actualContainer.getActualInteractMenu().getIndexValue().ToString());
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GameObject objectCollider = checkRaycast();
            if (objectCollider != null)
            {
                if (actualContainer == null && hand == null)
                {
                    if (objectCollider.GetComponent<CustomTag>().findTag("Container"))
                    {
                        if (!objectCollider.GetComponent<Container>().hasActualInteractMenu())
                        {
                            GameConfig.actualController.changeCursorState();
                            this.actualContainer = objectCollider.GetComponent<Container>();
                            this.actualContainer.openContainer();
                        }
                    }
                }
                else
                {
                    if (actualContainer != null)
                    {
                        closeActualContainer();
                        GameConfig.actualController.changeCursorState();
                    }
                }
            }
        }
        if (Input.GetButtonDown("Cancel"))
        {
            closeActualContainer();
        }
    }

    void HandleUnityAction(int arg0)
    {
    }


    private void throwItem()
    {
        if (hand != null)
        {
            print("Throw Item!");
            this.hand.GetComponent<Rigidbody>().isKinematic = false;
            this.hand.GetComponent<Rigidbody>().freezeRotation = false;
            this.hand.GetComponent<Rigidbody>().useGravity = true;
            this.hand.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 500);
            this.hand = null;
        }
    }
    private void checkHand()
    {
        if (this.hand != null)
        {
            Vector3 dir = (this.handPos.transform.position - this.hand.transform.position);
            dir.y = dir.y / 4;
            hand.GetComponent<Rigidbody>().velocity = dir * 20;
            hand.transform.rotation = playerCamera.transform.rotation;
        }
    }
    public void setHand(Pickable item)
    {
        this.hand = item;
        this.hand.transform.position = handPos.transform.position;
    }
    private void movement()
    {
        if (!GameConfig.inPause && !this.parado)
        {
            float tmpAxisHinfo = Input.GetAxis("Horizontal");
            float tmpAxisVinfo = Input.GetAxis("Vertical");
            if (tmpAxisHinfo != 0 || tmpAxisVinfo != 0)
            {
                Vector3 newPos = new Vector3(tmpAxisHinfo, 0.0f, tmpAxisVinfo);
                if (rb.velocity.magnitude < maxMoveSpeed)
                {
                    rb.AddRelativeForce(newPos.normalized * moveSpeed);
                }
                else
                {
                    rb.AddRelativeForce(newPos.normalized * -moveSpeed);
                }
            }else
            {
                if (rb.velocity.magnitude > 0)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                }
            }
        }
    }
    private void cameraMouse()
    {
        float tmpAxisXMouse = Input.GetAxis("Mouse X");
        float tmpAxisYMouse = Input.GetAxis("Mouse Y");
        if (tmpAxisXMouse != 0)
        {
            if (tmpAxisXMouse > 0)
            {
                this.transform.Rotate(Vector3.up * (cameraSpeed * tmpAxisXMouse));
            }
            else {
                this.transform.Rotate(-Vector3.up * (-cameraSpeed * tmpAxisXMouse));
            }
        }
        if (tmpAxisYMouse != 0)
        {
            if (tmpAxisYMouse > 0)
            {
                this.playerCamera.GetComponent<Camera>().transform.Rotate(Vector3.left * ((cameraSpeed/2) * tmpAxisYMouse));
            }else
            {
                this.playerCamera.GetComponent<Camera>().transform.Rotate(-Vector3.left * (-(cameraSpeed/2) * tmpAxisYMouse));
            }
        }
    }
    private GameObject checkRaycast()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = this.transform.position;
        rayCastOrigin.y = playerCamera.transform.position.y;
        if (Physics.Raycast(rayCastOrigin, playerCamera.transform.TransformDirection(Vector3.forward), out hit, rayCastDistance))
        {
            Debug.DrawRay(rayCastOrigin, playerCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            //Debug.Log("HIT!");
            GameObject objectCollider = hit.collider.gameObject;
            Debug.Log(objectCollider.ToString());
            Debug.Log(objectCollider.tag);
            return objectCollider;
        }
        return null;
    }
    public void setParado(bool parado)
    {
        this.parado = parado;
    }
    public void closeActualContainer()
    {
        if (actualContainer != null)
        {
            actualContainer.closeContainer();
            actualContainer = null;
            GameConfig.actualController.changeCursorState();
        }
    }
}
