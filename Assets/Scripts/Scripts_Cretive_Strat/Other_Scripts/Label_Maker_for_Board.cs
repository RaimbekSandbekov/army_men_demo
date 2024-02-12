using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Label_Maker_for_Board : MonoBehaviour
{
    public GameObject label_0;
    public GameObject label_1;
    public GameObject label_2;
    public GameObject label_3;
    public GameObject label_4;
    public GameObject label_5;
    public GameObject label_6;
    public GameObject label_7;
    public GameObject label_8;
    public GameObject label_9;

    int board_x_width = 10;
    int board_z_width = 15;

    void Start()
    {
        for (int i = 0; i <= board_x_width; i++)
        {
            for (int j = 0; j <= board_z_width; j++)
            {
                var posit = new Vector3(i, 0, j);
                if (i == 0)
                {
                    Instantiate(label_0, posit, transform.rotation);
                }
                if (i == 1)
                {
                    Instantiate(label_1, posit, transform.rotation);
                }
                if (i == 2)
                {
                    Instantiate(label_2, posit, transform.rotation);
                }
            }
        }
    }

}
