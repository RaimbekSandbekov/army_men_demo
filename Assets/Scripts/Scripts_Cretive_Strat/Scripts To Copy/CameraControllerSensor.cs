using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerSensor : MonoBehaviour {

	float panSpeed = 0.05f;
	float zoomSpeed = 0.1f;
	float rotSpeed = 2;
	float angleSpeed = 1;

	float panXSlimit = -43f;
	float panXMlimit = 47f;
	float panZSlimit = -65f;
	float panZMlimit = 18f;

	float zoomUpLimit = 40f;
	float zoomDownLimit = 4f;

	float angleUpLimit = 25f;
	float angleDownLimit = 80f;

	public GameObject Controller;

	bool MOVECAMERA = true;

	public GameObject turret;
	public Camera Cam;
	bool buildMode = false;

	void Update()
	{
		if (MOVECAMERA == true) {
			if (Input.touchCount == 4) {
				Touch touchZero = Input.GetTouch (0);

				if (touchZero.phase == TouchPhase.Moved) {
					transform.Rotate (-Vector3.right * Time.deltaTime * angleSpeed * touchZero.deltaPosition.y, Space.Self); // up
				}
			} else if (Input.touchCount == 3) {
				Touch touchZero = Input.GetTouch (0);

				if (touchZero.phase == TouchPhase.Moved) {
					Controller.transform.Rotate (-Vector3.up * Time.deltaTime * rotSpeed * touchZero.deltaPosition.x, Space.World); // rotate
				}
			} else if (Input.touchCount == 2) {
				Touch touchZoomOne = Input.GetTouch (0);
				Touch touchZoomTwo = Input.GetTouch (1);

				if (touchZoomTwo.phase == TouchPhase.Moved && touchZoomOne.phase == TouchPhase.Moved) {
					// Find the position in the previous frame of each touch.
					Vector2 touchZeroPrevPos = touchZoomOne.position - touchZoomOne.deltaPosition;
					Vector2 touchOnePrevPos = touchZoomTwo.position - touchZoomTwo.deltaPosition;
					// Find the magnitude of the vector (the distance) between the touches in each frame.
					float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
					float touchDeltaMag = (touchZoomOne.position - touchZoomTwo.position).magnitude;
					// Find the difference in the distances between each frame.
					float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
					Controller.transform.Translate (0, (zoomSpeed * deltaMagnitudeDiff), 0, Space.World);
				}
			} else if (Input.touchCount == 1) {
				Touch touchPan = Input.GetTouch (0);

				if (touchPan.fingerId == 0 && touchPan.fingerId != 1 && touchPan.fingerId != 2 && touchPan.fingerId != 3) {
					if (touchPan.phase == TouchPhase.Moved) {
						Controller.transform.Translate (-touchPan.deltaPosition.x * panSpeed, 0, -touchPan.deltaPosition.y * panSpeed, Space.Self);
					}
				}
			}
		}

		if (Controller.transform.position.y >= zoomUpLimit) {
			Controller.transform.position = new Vector3(transform.position.x, zoomUpLimit, transform.position.z);
		}
		if (Controller.transform.position.y <= zoomDownLimit) {
			Controller.transform.position = new Vector3(transform.position.x, zoomDownLimit, transform.position.z);
		}


		if (transform.position.z >= panZMlimit) {
			Controller.transform.position = new Vector3(transform.position.x, transform.position.y, panZMlimit);
		}
		if (transform.position.z <= panZSlimit) {
			Controller.transform.position = new Vector3(transform.position.x, transform.position.y, panZSlimit);
		}
		if (transform.position.x >= panXMlimit) {
			Controller.transform.position = new Vector3(panXMlimit, transform.position.y, transform.position.z);
		}
		if (transform.position.x <= panXSlimit) {
			Controller.transform.position = new Vector3(panXSlimit, transform.position.y, transform.position.z);
		}


		if (transform.eulerAngles.x <= angleUpLimit) { //vverh angleUpLimit
			transform.eulerAngles = new Vector3(angleUpLimit, transform.eulerAngles.y, transform.eulerAngles.z);
		}
		if (transform.eulerAngles.x >= angleDownLimit) { //vniz angleDownLimit
			transform.eulerAngles = new Vector3(angleDownLimit, transform.eulerAngles.y, transform.eulerAngles.z);
		}

		if (transform.position.y >= zoomUpLimit) {
			transform.position = new Vector3(transform.position.x, zoomUpLimit, transform.position.z);
		}
		if (transform.position.y <= zoomDownLimit) {
			transform.position = new Vector3(transform.position.x, zoomDownLimit, transform.position.z);
		}


	} // Update

	void FixedUpdate(){
		Ray ray = Cam.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 300.0f)) {
			if (hit.collider.tag == "Ground") {
				if (buildMode) {
					Instantiate (turret, hit.point, Quaternion.identity);
					buildMode = false;
				}
			}
		}
	}

	public void toBuild(){
		buildMode = true;
	}

} //End of script
