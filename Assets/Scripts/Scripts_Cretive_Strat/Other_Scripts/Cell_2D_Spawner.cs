using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell_2D_Spawner : MonoBehaviour
{
    public GameObject cell_One;
    public GameObject cell_Two;
    int board_x_width = 10;
    int board_z_width = 15;

    void Start()
    {
        for (int i = 0; i <= board_x_width; i++)
        {
            for (int j = 0; j <= board_z_width; j++)
            {
                var q = new Vector3(i, 0, j);
                if ((i + j) % 2 == 0)
                {
                    Instantiate(cell_One, q, transform.rotation);
                }
                else
                {
                    Instantiate(cell_Two, q, transform.rotation);
                }
            }
        }
    }
}
