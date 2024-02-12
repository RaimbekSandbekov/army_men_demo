using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoUnitMovement : MonoBehaviour {

	MapArraysAndPathfinding arrayScript;
	int unitXpos;
	int unitYpos;
	int targetXpos;
	int targetYpos;
	bool doMove = false;
	Vector3 NextCell;
	float speed = 4f;
	Vector3 targetV3;

	void Start () {
		arrayScript = GameObject.Find ("MapVisualGrid").GetComponent<MapArraysAndPathfinding> ();
		unitXpos = Mathf.FloorToInt (transform.position.x);
		unitYpos = Mathf.FloorToInt (transform.position.z);
	}
	
	void Update () {
		if (doMove) {
			Vector3 dir = NextCell - transform.position;
			float distThisFrame = speed * Time.deltaTime;
			if(dir.magnitude <= distThisFrame) {
				Move(targetV3);
			} else {
				transform.Translate( dir.normalized * distThisFrame, Space.World );
//				Quaternion targetRotation = Quaternion.LookRotation( dir );
//				this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime*5);
			}
		}
	}

	public void Move(Vector3 targ){
		targetV3 = targ;
		targetXpos = Mathf.FloorToInt (targetV3.x);
		targetYpos = Mathf.FloorToInt (targetV3.z);
		unitXpos = Mathf.FloorToInt (transform.position.x);
		unitYpos = Mathf.FloorToInt (transform.position.z);
		NextCell = arrayScript.DemoGiveNextCell (unitYpos, unitXpos, targetYpos, targetXpos);
		doMove = true;
		if (NextCell.y == -10) {	// our condition where pos == target
			doMove = false;
		}
		NextCell += new Vector3 (0.5f, 0, 0.5f);
	}
}
