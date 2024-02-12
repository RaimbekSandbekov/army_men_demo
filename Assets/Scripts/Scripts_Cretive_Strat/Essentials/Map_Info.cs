using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Info : MonoBehaviour
{
    /*
    int[,] MapCellsArray = {   {0,1,2,3,4,5,6,7,8,9},               // 0 - empty cell
                               {10,11,12,13,14,15,16,17,18,19},     // 1 - populated cell
                               {20,0,0,0,0,0,0,0,0,29},
                               {30,0,0,0,0,0,0,0,0,39},          //  00 01 02 03   couting of indices
                               {40,0,0,0,0,0,0,0,0,49},          //  10 11 12 13
                               {50,0,0,0,0,0,0,0,0,59},          //  20 21 22 23
                               {60,0,0,0,0,0,0,0,0,69},
                               {70,0,0,0,0,0,0,0,0,79},
                               {80,0,0,0,0,0,0,0,0,89},
                               {90,0,0,0,0,0,0,0,0,99},       };
    */
    int[,] MapCellsArray = {   {0,0,0,0,0,0,0,0,0,0},   // 0 - empty cell
                               {0,1,0,0,0,0,0,0,0,0},   // 1 - populated cell
                               {0,2,0,0,0,0,0,0,0,0},
                               {0,0,0,0,0,0,0,0,0,0},   //  00 01 02 03   couting of indices
                               {0,0,0,0,0,0,0,0,0,0},   //  10 11 12 13
                               {0,0,0,0,0,0,0,0,0,0},   //  20 21 22 23
                               {0,0,0,0,0,0,0,0,0,0},
                               {0,0,0,0,0,0,0,0,0,0},
                               {0,0,0,0,0,0,0,0,0,0},
                               {0,0,0,0,0,0,0,0,0,0},       };

    public GameObject cellStateMarker;

    void Start()
    {

    }

    void Update()
    {
        
    }

    void VisualizeCellStateOnBoard()
    {
        /*      What this does is, makes visible populated arrays by
         *  placing marks on cells with "1" value.      */
        for (int i = 0; i <= 9; i++)
        {
            for (int j = 0; j <= 9; j++)
            {
                if (MapCellsArray[i,j] == 1)  // if cell is populated
                {
                    
                }
            }
        }
    }

    public void PrintCellState(int i_ind, int j_ind)
    {
        print(MapCellsArray[i_ind, j_ind]);
    }
}
