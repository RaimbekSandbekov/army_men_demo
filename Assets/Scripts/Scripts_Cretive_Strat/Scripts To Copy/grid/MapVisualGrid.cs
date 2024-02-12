using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVisualGrid : MonoBehaviour {

	int MapNumberOfSectorsY = 10;	// y number of sectors present in map
	int MapNumberOfSectorsX = 10;	// x number of sectors present in map
	int SectorNumberOfCellsY = 10;	// y number of cells present in sector
	int SectorNumberOfCellsX = 10;	// x number of cells present in sector

	public GameObject SectorOfPrefabOdd;	// prefab of odd idexed sector
	public GameObject SectorOfPrefabEven;	// prefab of even idexed sector
	public GameObject gridRod;	// prefab of grid rod

	void Awake () {
		//creates board with sector tiles. Evens are darker, odds are light
		for (int i = 0; i < MapNumberOfSectorsY; i++) {	// rows of sectors in map
			for (int j = 0; j < MapNumberOfSectorsX; j++) {	// columns of sectors in map
				if ((i + j) % 2 == 1) {
					var sector = Instantiate (SectorOfPrefabOdd, new Vector3 (j * MapNumberOfSectorsY+(SectorNumberOfCellsX/2), 0, i * MapNumberOfSectorsX+(SectorNumberOfCellsX/2)), Quaternion.Euler (new Vector3 (90, 0, 0)));
					sector.transform.localScale += new Vector3(SectorNumberOfCellsX-1, SectorNumberOfCellsY-1, 0);
					//print ("odd, i: " + i + ", j: " + j);
				}
				if ((i + j) % 2 == 0) {
					var sector = Instantiate (SectorOfPrefabEven, new Vector3 (j * MapNumberOfSectorsY+(SectorNumberOfCellsX/2), 0, i * MapNumberOfSectorsX+(SectorNumberOfCellsX/2)), Quaternion.Euler (new Vector3 (90, 0, 0)));
					sector.transform.localScale += new Vector3(SectorNumberOfCellsX-1, SectorNumberOfCellsY-1, 0);
					//print("even, i: " + i + ", j: " + j);
				}
			}
		}
		//end of board creation
		//start of grid creation
		// !IMPORTANT! check its variables! some must be even! 
		for (int i = 0; i <= MapNumberOfSectorsY * SectorNumberOfCellsY; i++) { // create grid around X
			var gri = Instantiate (gridRod, new Vector3 ((MapNumberOfSectorsY * SectorNumberOfCellsY)/2 - 5+(SectorNumberOfCellsX/2), 0, i - 5+(SectorNumberOfCellsX/2)), Quaternion.identity);
			gri.transform.localScale += new Vector3(MapNumberOfSectorsX * SectorNumberOfCellsX, 0, 0);
		}
		for (int i = 0; i <= MapNumberOfSectorsY * SectorNumberOfCellsY; i++) { // create grid around X
			var gri = Instantiate (gridRod, new Vector3 (i-5+(SectorNumberOfCellsX/2), 0, (MapNumberOfSectorsY * SectorNumberOfCellsY)/2 - 5+(SectorNumberOfCellsX/2)), Quaternion.identity);
			gri.transform.localScale += new Vector3(0, 0, MapNumberOfSectorsX * SectorNumberOfCellsX);
		}
		//end of grid creation
	}
}
