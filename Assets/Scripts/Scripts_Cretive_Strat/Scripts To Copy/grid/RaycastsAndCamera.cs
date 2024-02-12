using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastsAndCamera : MonoBehaviour {

	int xPos;	//stores position of click
	int yPos;
	public Text FPSui;

	int m_frameCounter = 0;
	float m_timeCounter = 0.0f;
	float m_lastFramerate = 0.0f;
	public float m_refreshTime = 1f;

	void Update () {
		if( m_timeCounter < m_refreshTime )
		{
			m_timeCounter += Time.smoothDeltaTime;
			m_frameCounter++;
		}
		else
		{
			m_lastFramerate = (float)m_frameCounter/m_timeCounter;
			m_frameCounter = 0;
			m_timeCounter = 0.0f;
		}
		SetFPSui ();
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				if (hit.collider.gameObject.tag == "Board") {
//					PrintCellInfoInEditor (hit.point);
					for (int i = 0; i < GameObject.FindGameObjectsWithTag ("Unit").Length; i++) {
						GameObject.FindGameObjectsWithTag ("Unit") [i].GetComponent<DemoUnitMovement>().Move(hit.point);
					}
				}
			}
		}
	}
	
	void PrintCellInfoInEditor(Vector3 hitPointTemp){	// print Sector and Cell by Vector3 TODO add state of cell TODO
		xPos = Mathf.FloorToInt (hitPointTemp.x);
		yPos = Mathf.FloorToInt (hitPointTemp.z);
		print (hitPointTemp + ", [" + yPos + "," + xPos + "]" + ", sector: [" + (yPos/10) + "," + (xPos/10) + "]");
	}

	void SetFPSui ()
	{
		FPSui.text = "FPS: " + m_lastFramerate.ToString();
//		FPSui.text = "FPS: " + ((int)(1.0f / Time.smoothDeltaTime)).ToString ();
	}
}
