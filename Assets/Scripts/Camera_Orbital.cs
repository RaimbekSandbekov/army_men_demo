using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Orbital : MonoBehaviour
{
    bool computerControlsAllowed = false;
    bool smartphoneControlsAllowed = true;
    /* INITIAL CAMERA VALUES */
    public Transform cameraTransform;
    float movementSpeed = 0.15f;
    float movementTime = 1000f;    // the smaller time the slower all camera moves (pan,zoom,rotation)
    float normalSpeed = 0.05f;
    float fastSpeed = 0.09f;
    public Vector3 newPosition;
    float rotationAmount = 1.2f;
    Quaternion newRotation;
    Vector3 zoomAmount;
    Vector3 newZoom;
    Vector3 dragStartPosition;
    Vector3 dragCurrentPosition;
    Vector3 rotateStartPosition;
    Vector3 rotateCurrentPosition;

    /* SENSOR CONTROL VARIABLES */
    bool MOVECAMERA = true;

    /* UNIT REFERENSE VALUES */
    int xPos;   // Raycast hit value, used to find cell location
    int zPos;   // Raycast hit value

    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
        zoomAmount = new Vector3(0, -1.4f, 1.4f);
    }

    void Update()
    {
        Function_Camera_Raycast();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            computerControlsAllowed = true;
            smartphoneControlsAllowed = false;
        }
        if (computerControlsAllowed)
        {
            Function_Mouse_Controls();
            Function_Keyboard_Controls();
        }
        else
        {
            Function_Sensor_Controls();
        }
    }

    private void LateUpdate()
    {
        //Function_Keyboard_Controls();
        //Function_Mouse_Controls();
        //Function_Sensor_Controls();

        /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
            computerControlsAllowed = true;
            smartphoneControlsAllowed = false;
        }
        if (computerControlsAllowed)
        {
            Function_Mouse_Controls();
            Function_Keyboard_Controls();
        }
        else
        {
            Function_Sensor_Controls();
        }
        */
    }

    void Function_Mouse_Controls()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }
        if (Input.GetMouseButtonDown(2))
        {
            Plane plane1 = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entry;
            if (plane1.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(2))
        {
            Plane plane1 = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entry;
            if (plane1.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            rotateCurrentPosition = Input.mousePosition;
            Vector3 difference = rotateStartPosition - rotateCurrentPosition;
            rotateStartPosition = rotateCurrentPosition;
            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5));
            //newRotation *= Quaternion.Euler(difference.y / 5, difference.x / 5, 0);
        }
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }

    void Function_Keyboard_Controls()
    {
        if (Input.GetKey(KeyCode.LeftShift)) { movementSpeed = fastSpeed; }
        else { movementSpeed = normalSpeed; }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (-transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (-transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(-Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.X))
        {
            newZoom += zoomAmount / 8;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            newZoom -= zoomAmount / 8;
        }
    }

    void Function_Camera_Raycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 300.0f))
            {
                xPos = Mathf.FloorToInt(hit.point.x);
                zPos = Mathf.FloorToInt(hit.point.z);
                if (hit.collider.tag == "PlayGround")
                {
                    //unitScript3.SetAndInitializeUnitDestination(hit.point);

                }
            }
        }
    }

    void Function_Sensor_Controls()
    {
        if (MOVECAMERA == true)
        {
            /*
            if (Input.touchCount == 4)                            // X-Rotation
            {
                Touch touchZero = Input.GetTouch(0);
                if (touchZero.phase == TouchPhase.Moved)
                {
                    //Camera.main.transform.Rotate(-Vector3.right * Time.deltaTime * angleSpeed * touchZero.deltaPosition.y, Space.Self);
                    newRotation *= Quaternion.Euler(Vector3.right * rotationAmount * touchZero.deltaPosition.y / 8);
                    transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
                }
            }
            else*/
            if (Input.touchCount == 3)                       // Y-Rotation
            {
                Touch touchZero = Input.GetTouch(0);
                if (touchZero.phase == TouchPhase.Moved)
                {
                    //Camera.main.transform.Rotate(-Vector3.up * Time.deltaTime * rotSpeed * touchZero.deltaPosition.x, Space.World); // rotate // OLD CODE
                    newRotation *= Quaternion.Euler(-Vector3.up * rotationAmount * touchZero.deltaPosition.x / 8);
                    transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
                }
            }
            else if (Input.touchCount == 2)
            {
                Touch touchZoomOne = Input.GetTouch(0);
                Touch touchZoomTwo = Input.GetTouch(1);
                if (touchZoomTwo.phase == TouchPhase.Moved && touchZoomOne.phase == TouchPhase.Moved)
                {
                    // Find the position in the previous frame of each touch.
                    Vector2 touchZeroPrevPos = touchZoomOne.position - touchZoomOne.deltaPosition;
                    Vector2 touchOnePrevPos = touchZoomTwo.position - touchZoomTwo.deltaPosition;
                    // Find the magnitude of the vector (the distance) between the touches in each frame.
                    float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                    float touchDeltaMag = (touchZoomOne.position - touchZoomTwo.position).magnitude;
                    // Find the difference in the distances between each frame.
                    float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
                    //Controller.transform.Translate(0, (zoomSpeed * deltaMagnitudeDiff), 0, Space.World);  // OLD SCRIPT
                    newZoom -= deltaMagnitudeDiff * zoomAmount / 30;
                    cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
                }
            }
            else if (Input.touchCount == 1)
            {
                Touch touchPan = Input.GetTouch(0);
                if (touchPan.fingerId == 0 && touchPan.fingerId != 1 && touchPan.fingerId != 2 && touchPan.fingerId != 3)
                {
                    if (touchPan.phase == TouchPhase.Moved)
                    {
                        //Controller.transform.Translate(-touchPan.deltaPosition.x * panSpeed, 0, -touchPan.deltaPosition.y * panSpeed, Space.Self); // IS OLD CODE
                        newPosition += (-transform.forward * touchPan.deltaPosition.y * movementSpeed / 5);
                        newPosition += (-transform.right * touchPan.deltaPosition.x * movementSpeed / 5);
                        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
                    }
                }
            }
        }
    }

    void PrintTileInfo(Vector3 hitPointTemp)
    {   // print Sector and Cell by Vector3 TODO add state of cell TODO
        int Xcross2 = 0;
        int Zcross2 = 0;
        if (hitPointTemp.z - Mathf.FloorToInt(hitPointTemp.z) >= 0.5f) { Zcross2 = (int)zPos + 1; } else { Zcross2 = (int)zPos; }
        if (hitPointTemp.x - Mathf.FloorToInt(hitPointTemp.x) >= 0.5f) { Xcross2 = (int)xPos + 1; } else { Xcross2 = (int)xPos; }
        //print(hitPointTemp + ", [" + zPos + "," + xPos + "]" + ", sector: [" + (zPos / 10) + "," + (xPos / 10) + "]" + " Cross: " + Zcross2 + ", " + Xcross2);
        int Xapprox = Mathf.FloorToInt(hitPointTemp.x);
        int Zapprox = Mathf.FloorToInt(hitPointTemp.z);
        print(Zapprox + ", " + Xapprox);
    }
}
