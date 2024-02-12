using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster_Camera : MonoBehaviour
{
    Map_Info map_info_reference;
    public GameObject script_Obj;

    void Start()
    {
        //Map_Info map_info_reference = GameObject.Find("SCRIPTS_1").GetComponent<Map_Info>();
        //Map_Info map_info_reference = GameObject.FindGameObjectWithTag("Script_Manager").GetComponent<Map_Info>();
        Map_Info map_info_reference = script_Obj.GetComponent<Map_Info>();
        //script_Obj.GetComponent<Map_Info>().PrintCellState(9, 9);
        
        /*
        print(Mathf.FloorToInt(Mathf.Abs(0.5f)));//
        print(Mathf.FloorToInt(Mathf.Abs(-0.5f)));//

        print(Mathf.FloorToInt(Mathf.Abs(0.51f)));//
        print(Mathf.FloorToInt(Mathf.Abs(0.6f)));//
        print(Mathf.FloorToInt(Mathf.Abs(1.5f)));//
        */
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            do_Raycast();
        }
    }

    void do_Raycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.gameObject.name == "GAME BOARD")
            {
                //					PrintCellInfoInEditor (hit.point);
                //for (int i = 0; i < GameObject.FindGameObjectsWithTag("Unit").Length; i++)
                //GameObject.FindGameObjectsWithTag("Unit")[i].GetComponent<DemoUnitMovement>().Move(hit.point);
                int xPos = Mathf.FloorToInt(hit.point.x);
                int yPos = Mathf.FloorToInt(hit.point.z);
                print(hit.point.x.ToString() + "_____________" + hit.point.z.ToString());
                print("...and " + xPos.ToString() + "_____________" + yPos.ToString());
                script_Obj.GetComponent<Map_Info>().PrintCellState(yPos, xPos);
            }
        }
    }

    void PrintRaycastToCellInfo(Vector3 hitPointTemp)
    {   // print Sector and Cell by Vector3 TODO add state of cell TODO
        //xPos = Mathf.FloorToInt(hitPointTemp.x);
        //yPos = Mathf.FloorToInt(hitPointTemp.z);
        //print(hitPointTemp + ", [" + yPos + "," + xPos + "]" + ", sector: [" + (yPos / 10) + "," + (xPos / 10) + "]");
    }
}
