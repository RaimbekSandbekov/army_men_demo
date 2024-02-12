using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    /*int[,] mapArray = { 
    { 0,0,0,0 },    // [rows, columns]
    { 0,0,0,0 },
    { 0,0,0,0 } 
    };*/
    public GameObject cellOnEven;
    public GameObject cellOnOdd;

    public GameObject Ruins_Neutral;
    public GameObject Factory_1_Neutral;
    public GameObject Factory_2_Neutral;

    public GameObject HQ_Player;
    public GameObject HQ_Enemy;
    public GameObject Tank_Player;
    public GameObject Tank_Enemy;
    public GameObject Troopers_Player;
    public GameObject Troopers_Enemy;

    public GameObject Troopers_SandbagPLAYA;
    public GameObject Troopers_SandbagEMEMI;

    void Start()
    {
        //printAll2DArrayElements(mapArray, "comfy formatting");
        //mapArray[1, 1] = 9;
        //printAll2DArrayElements(mapArray, "");

        VisualCellBoard_initialise(cellOnEven, cellOnOdd, 15, 15);
        int[,] cellBoardArray = Create2DArray(15, 15);
        printAll2DArrayElements(cellBoardArray, "");
        

        //plantRandomUnitsOnBoard(cellBoardArray, 1);plantRandomUnitsOnBoard(cellBoardArray, 1);plantRandomUnitsOnBoard(cellBoardArray, 1);plantRandomUnitsOnBoard(cellBoardArray, 1);plantRandomUnitsOnBoard(cellBoardArray, 1);

        plantUnitOnBoardOnXZ(cellBoardArray, 100, 2, 4);
        plantUnitOnBoardOnXZ(cellBoardArray, 200, 9, 9);

        plantRandomUnitsOnBoard(cellBoardArray, 0);
        plantRandomUnitsOnBoard(cellBoardArray, 1);
        plantRandomUnitsOnBoard(cellBoardArray, 2);
        plantRandomUnitsOnBoard(cellBoardArray, 0);
        plantRandomUnitsOnBoard(cellBoardArray, 1);
        plantRandomUnitsOnBoard(cellBoardArray, 2);
        plantRandomUnitsOnBoard(cellBoardArray, 0);
        plantRandomUnitsOnBoard(cellBoardArray, 1);
        plantRandomUnitsOnBoard(cellBoardArray, 2);
        plantRandomUnitsOnBoard(cellBoardArray, 0);
        plantRandomUnitsOnBoard(cellBoardArray, 1);
        plantRandomUnitsOnBoard(cellBoardArray, 2);
        plantRandomUnitsOnBoard(cellBoardArray, 0);
        plantRandomUnitsOnBoard(cellBoardArray, 1);
        plantRandomUnitsOnBoard(cellBoardArray, 2);
        plantRandomUnitsOnBoard(cellBoardArray, 0);
        plantRandomUnitsOnBoard(cellBoardArray, 1);
        plantRandomUnitsOnBoard(cellBoardArray, 2);

        plantRandomUnitsOnBoard(cellBoardArray, 102);
        plantRandomUnitsOnBoard(cellBoardArray, 202);
        plantRandomUnitsOnBoard(cellBoardArray, 102);
        plantRandomUnitsOnBoard(cellBoardArray, 202);
        plantRandomUnitsOnBoard(cellBoardArray, 102);
        plantRandomUnitsOnBoard(cellBoardArray, 202);
        plantRandomUnitsOnBoard(cellBoardArray, 102);
        plantRandomUnitsOnBoard(cellBoardArray, 202);
        plantRandomUnitsOnBoard(cellBoardArray, 202);
        plantRandomUnitsOnBoard(cellBoardArray, 102);
        plantRandomUnitsOnBoard(cellBoardArray, 202);
        plantRandomUnitsOnBoard(cellBoardArray, 202);
        plantRandomUnitsOnBoard(cellBoardArray, 102);
        plantRandomUnitsOnBoard(cellBoardArray, 202);
        plantRandomUnitsOnBoard(cellBoardArray, 202);
        plantRandomUnitsOnBoard(cellBoardArray, 102);
        plantRandomUnitsOnBoard(cellBoardArray, 202);

        plantRandomUnitsOnBoard(cellBoardArray, 101);
        plantRandomUnitsOnBoard(cellBoardArray, 201);
        plantRandomUnitsOnBoard(cellBoardArray, 101);
        plantRandomUnitsOnBoard(cellBoardArray, 201);
        plantRandomUnitsOnBoard(cellBoardArray, 101);
        plantRandomUnitsOnBoard(cellBoardArray, 201);
        plantRandomUnitsOnBoard(cellBoardArray, 101);
        plantRandomUnitsOnBoard(cellBoardArray, 201);
        plantRandomUnitsOnBoard(cellBoardArray, 101);
        plantRandomUnitsOnBoard(cellBoardArray, 201);
        plantRandomUnitsOnBoard(cellBoardArray, 101);
        plantRandomUnitsOnBoard(cellBoardArray, 201);
        plantRandomUnitsOnBoard(cellBoardArray, 101);
        plantRandomUnitsOnBoard(cellBoardArray, 201);
        plantRandomUnitsOnBoard(cellBoardArray, 101);
        plantRandomUnitsOnBoard(cellBoardArray, 201);


        plantRandomUnitsOnBoard(cellBoardArray, -999);
        plantRandomUnitsOnBoard(cellBoardArray, -999);
        plantRandomUnitsOnBoard(cellBoardArray, -999);
        plantRandomUnitsOnBoard(cellBoardArray, -999);
        plantRandomUnitsOnBoard(cellBoardArray, -999);
        plantRandomUnitsOnBoard(cellBoardArray, -999);
        plantRandomUnitsOnBoard(cellBoardArray, -999);
        plantRandomUnitsOnBoard(cellBoardArray, -999);
        
        plantRandomUnitsOnBoard(cellBoardArray, -888);
        plantRandomUnitsOnBoard(cellBoardArray, -888);
        plantRandomUnitsOnBoard(cellBoardArray, -888);
        plantRandomUnitsOnBoard(cellBoardArray, -888);
        plantRandomUnitsOnBoard(cellBoardArray, -888);
        plantRandomUnitsOnBoard(cellBoardArray, -888);
        plantRandomUnitsOnBoard(cellBoardArray, -888);
        plantRandomUnitsOnBoard(cellBoardArray, -888);
        plantRandomUnitsOnBoard(cellBoardArray, -888);
        plantRandomUnitsOnBoard(cellBoardArray, -888);
        plantRandomUnitsOnBoard(cellBoardArray, -888);


        printAll2DArrayElements(cellBoardArray, "");
    }

    void Update()
    {
        
    }

    void printAll2DArrayElements(int[,] arr, string optionals)
    {
        if (optionals == "shit formatting")
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    print("[" + i + "," + j + "] = " + arr[i, j]);
                }
            }
        }
        else if ((optionals == "comfy formatting") || (optionals == ""))
        {
            print("======================================[BEGIN]");
            for (int i = arr.GetLength(0) - 1; i >= 0; i--)
            {
                string res = "";
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    res += (arr[i, j].ToString()+",");
                }
                print("array["+i+","+ arr.GetLength(1) +"]: "+ res);
            }
            print("========================================[END]");
        }
    }

    void VisualCellBoard_initialise(GameObject cellEven, GameObject cellOdd, int gridSize_X, int gridSize_Z)
    {
        for(int i = 0; i < gridSize_X; i++)
        {
            for(int j = 0; j < gridSize_Z; j++)
            {
                if ((i + j) % 2 == 0){
                    Instantiate(cellEven, new Vector3(j, 0, i), Quaternion.Euler(0, 0, 0));
                }
                else
                {
                    Instantiate(cellOdd, new Vector3(j, 0, i), Quaternion.Euler(0, 0, 0));
                }
            }
        }
    }

    static int[,] Create2DArray(int sizeX, int sizeY)
    {
        int[,] newArray = new int[sizeX, sizeY];
        // You can initialize the array elements with your desired values here
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                newArray[i, j] = 0; // cause empty cell has value of 0
            }
        }
        return newArray;
    }

    void plantRandomUnitsOnBoard(int[,] mapArr, int unitID)
    // Randomly plant Units on Board
    // cellValue is ID of Unit
    {
        int randoX = Random.Range(0, mapArr.GetLength(0));
        int randoZ = Random.Range(0, mapArr.GetLength(1));
        while (mapArr[randoX, randoZ] != 0)
        {
            randoX = Random.Range(0, mapArr.GetLength(0));
            randoZ = Random.Range(0, mapArr.GetLength(1));
        }
        mapArr[randoX, randoZ] = 1; // the array cell is occupied: 0 is free, 1 is occupied
        unitIDs(unitID, randoZ, randoX);
    }
    void plantUnitOnBoardOnXZ(int[,] mapArr, int unitID, int X, int Z)
    // plant Unit on Board on XZ coordinates
    // cellValue is ID of Unit
    // while loop lets randomise position if it is occupied
    {
        /*while (mapArr[X, Z] != 0)
        {
            X = Random.Range(0, mapArr.GetLength(0));
            Z = Random.Range(0, mapArr.GetLength(1));
        }*/
        mapArr[X, Z] = 1; // the array cell is occupied: 0 is free, 1 is occupied
        unitIDs(unitID, X, Z);
    }

    void unitIDs(int unitID, int Z, int X)
    {
        if (unitID == 100)
        {
            Instantiate(HQ_Player, new Vector3(Z, 0, X), Quaternion.identity);
        }
        else if (unitID == 101)
        {
            Instantiate(Troopers_Player, new Vector3(Z, 0, X), Quaternion.identity);
        }
        else if (unitID == 102)
        {
            Instantiate(Tank_Player, new Vector3(Z, 0, X), Quaternion.identity);
        }
        else if (unitID == 200)
        {
            Instantiate(HQ_Enemy, new Vector3(Z, 0, X), Quaternion.identity);
        }
        else if (unitID == 201)
        {
            Instantiate(Troopers_Enemy, new Vector3(Z, 0, X), Quaternion.identity);
        }
        else if (unitID == 202)
        {
            Instantiate(Tank_Enemy, new Vector3(Z, 0, X), Quaternion.identity);
        }
        else if (unitID == 0)
        {
            Instantiate(Ruins_Neutral, new Vector3(Z, 0, X), Quaternion.identity);
        }
        else if (unitID == 1)
        {
            Instantiate(Factory_1_Neutral, new Vector3(Z, 0, X), Quaternion.identity);
        }
        else if (unitID == 2)
        {
            Instantiate(Factory_2_Neutral, new Vector3(Z, 0, X), Quaternion.identity);
        }

        else if (unitID == -999)
        {
            Instantiate(Troopers_SandbagPLAYA, new Vector3(Z, 0, X), Quaternion.identity);
        }
        else if (unitID == -888)
        {
            Instantiate(Troopers_SandbagEMEMI, new Vector3(Z, 0, X), Quaternion.identity);
        }

        else
        {
            print("Wrong ID mothefucker");
        }
    }
}
