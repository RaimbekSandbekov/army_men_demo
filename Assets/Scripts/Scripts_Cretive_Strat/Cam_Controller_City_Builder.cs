using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Controller_City_Builder : MonoBehaviour
{
    public Transform cameraTransform;
    float movementSpeed = 0.03f;
    float movementTime = 500f;    // the smaller time the slower all camera moves (pan,zoom,rotation)
    float normalSpeed = 0.03f;
    float fastSpeed = 0.09f;
    public Vector3 newPosition;
    float rotationAmount = 1.4f;
    Quaternion newRotation;
    Vector3 zoomAmount;
    Vector3 newZoom;
    Vector3 dragStartPosition;
    Vector3 dragCurrentPosition;
    Vector3 rotateStartPosition;
    Vector3 rotateCurrentPosition;

    bool MOVECAMERA = true;
    bool computer_control = false;

    //public GameObject carManual;

    bool camera_rotate_mode = true;

    float panXSlimit = -500f;
    float panXMlimit = 700f;
    float panZSlimit = -700f;
    float panZMlimit = 500f;
    float zoomUpLimit = 1000f;
    float zoomDownLimit = -150f;


    void Start()
    {
        newZoom = cameraTransform.localPosition;
        zoomAmount = new Vector3(0, -0.2f, 0.2f);
        newPosition = transform.position;
        newRotation = transform.rotation;
        //Debug.Log(SystemInfo.operatingSystem);
    }

    private void LateUpdate()
    {
        if (computer_control == true)
        {
            Function_Keyboard_Controls();
            Function_Mouse_Controls();
        }
        else
        {
            Function_Sensor_Controls();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            computer_control = true;
        }
        //Function_Camera_Movement_Limits();
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
        if (camera_rotate_mode == true)
        {
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
    void Function_Sensor_Controls (){
        if (MOVECAMERA == true)
        {
            if (Input.touchCount == 4)
            {
                Touch touchZero = Input.GetTouch(0);

                if (touchZero.phase == TouchPhase.Moved)
                {
                    transform.Rotate(-Vector3.right * Time.deltaTime * 1.5f * touchZero.deltaPosition.y, Space.Self); // up
                }
            }
            else if (Input.touchCount == 3)
            {
                Touch touchZero = Input.GetTouch(0);

                if (touchZero.phase == TouchPhase.Moved)
                {
                    transform.Rotate(-Vector3.up * Time.deltaTime * 1.5f * touchZero.deltaPosition.x, Space.World); // rotate
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
                    transform.Translate(0, (.02f * deltaMagnitudeDiff), 0, Space.World);
                }
            }
            else if (Input.touchCount == 1)
            {
                Touch touchPan = Input.GetTouch(0);

                if (touchPan.fingerId == 0 && touchPan.fingerId != 1 && touchPan.fingerId != 2 && touchPan.fingerId != 3)
                {
                    if (touchPan.phase == TouchPhase.Moved)
                    {
                        transform.Translate(touchPan.deltaPosition.x * .005f, 0, touchPan.deltaPosition.y * .005f, Space.Self);
                    }
                }
            }
        }
    }
    void Function_Sensor_Controls_new_works_with_lags()
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
                    //Camera.main.transform.Rotate(-Vector3.up * Time.deltaTime * rotationAmount * touchZero.deltaPosition.x, Space.World); // rotate // OLD CODE
                    newRotation *= Quaternion.Euler(-Vector3.up * rotationAmount * touchZero.deltaPosition.x / 8);
                    transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
                }
                /*  THIS IS OLDEST CODE EVER
                 * else if (Input.touchCount == 3) {
				Touch touchZero = Input.GetTouch (0);

				if (touchZero.phase == TouchPhase.Moved) {
					Controller.transform.Rotate (-Vector3.up * Time.deltaTime * rotSpeed * touchZero.deltaPosition.x, Space.World); // rotate
				}
                 */
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
                    //newZoom -= deltaMagnitudeDiff * zoomAmount / 30;
                    //cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
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
                        newPosition += (-transform.forward * touchPan.deltaPosition.y * movementSpeed);
                        newPosition += (-transform.right * touchPan.deltaPosition.x * movementSpeed);
                        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
                    }
                }
            }
        }
    }

    public void changeRotationMode()
    {
        camera_rotate_mode = !camera_rotate_mode;
        //print(camera_rotate_mode);
    }

    void Function_Camera_Movement_Limits()                      // !!! Sets Limits on Movement and Rotation of CAMERA !!!
    {
        if (transform.position.z >= panZMlimit)                                                                     // Pan Z-Position Max Limit
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, panZMlimit);
        }
        if (transform.position.z <= panZSlimit)                                                                     // Pan Z-Position Min Limit
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, panZSlimit);
        }
        if (transform.position.x >= panXMlimit)                                                                     // Pan X-Position Max Limit
        {
            transform.position = new Vector3(panXMlimit, transform.position.y, transform.position.z);
        }
        if (transform.position.x <= panXSlimit)                                                                     // Pan X-Position Min Limit
        {
            transform.position = new Vector3(panXSlimit, transform.position.y, transform.position.z);
        }
        if (cameraTransform.localPosition.y >= zoomUpLimit)                                                                            // Zoom-Up Limit
        {
            cameraTransform.localPosition = new Vector3(transform.position.x, zoomUpLimit, transform.position.z);
        }
        if (cameraTransform.localPosition.y <= zoomDownLimit)                                                                          // Zoom-Down Limit
        {
            cameraTransform.localPosition = new Vector3(transform.position.x, zoomDownLimit, transform.position.z);
        }
    }
}
