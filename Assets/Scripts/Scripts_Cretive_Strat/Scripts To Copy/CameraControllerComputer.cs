using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerComputer: MonoBehaviour {
	
	bool isROT = false;
	float horizontalSpeed = 2;
	float verticalSpeed = 2;
	bool zoomIN = false;
	bool zoomOUT = false;
	float zoomSpeed = 2;
	bool rotLEFT = false;
	bool rotRIGHT = false;
	float rotSPEED = 25;
	bool angleUP = false;
	bool angleDOWN = false;
	float angleSPEED = 25;

	void Update () 
	{
		//if LMB is pressed, there comes PAN
		if(Input.GetMouseButton(0))
		{
			isROT = true;
		}
		if (!Input.GetMouseButton(0)) {
			isROT = false;
		}
		if (isROT == true) {
			float h = horizontalSpeed * Input.GetAxis("Mouse X");
			float v = verticalSpeed * Input.GetAxis("Mouse Y");
			transform.Translate(-h, 0, -v, Space.World);

		}

		// ZOOM!
		// IN!
		if(Input.GetKeyDown(KeyCode.W))
		{
			zoomIN = true;
		}
		if (Input.GetKeyUp(KeyCode.W)) {
			zoomIN = false;
		}
		if (zoomIN == true) {
			transform.Translate(0, zoomSpeed, 0, Space.World);
		}
		// OUT
		if(Input.GetKeyDown(KeyCode.S))
		{
			zoomOUT = true;
		}
		if (Input.GetKeyUp(KeyCode.S)) {
			zoomOUT = false;
		}
		if (zoomOUT == true) {
			transform.Translate(0, -zoomSpeed * 1.2f, 0, Space.World);
		}

		// ROTATE
		// LEFT
		if(Input.GetKeyDown(KeyCode.A))
		{
			rotLEFT = true;
		}
		if (Input.GetKeyUp(KeyCode.A)) {
			rotLEFT = false;
		}
		if (rotLEFT == true) {
			transform.Rotate (-Vector3.up * Time.deltaTime * rotSPEED, Space.World);
		}
		// RIGHT
		if(Input.GetKeyDown(KeyCode.D))
		{
			rotRIGHT = true;
		}
		if (Input.GetKeyUp(KeyCode.D)) {
			rotRIGHT = false;
		}
		if (rotRIGHT == true) {
			transform.Rotate(Vector3.up * Time.deltaTime * rotSPEED, Space.World);
		}

		// ANGLE
		// UP
		if(Input.GetKeyDown(KeyCode.Q))
		{
			angleUP = true;
		}
		if (Input.GetKeyUp(KeyCode.Q)) {
			angleUP = false;
		}
		if (angleUP == true) {
			transform.Rotate (-Vector3.right * Time.deltaTime * angleSPEED, Space.Self);
		}
		// DOWN
		if(Input.GetKeyDown(KeyCode.E))
		{
			angleDOWN = true;
		}
		if (Input.GetKeyUp(KeyCode.E)) {
			angleDOWN = false;
		}
		if (angleDOWN == true) {
			transform.Rotate(Vector3.right * Time.deltaTime * angleSPEED, Space.Self);
		}



	}








}
