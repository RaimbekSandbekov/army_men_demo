using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArraysAndPathfinding : MonoBehaviour {

	byte [,] SectorsArray = new byte[10,10];	//byte has 0 to 256 range
	byte [,] CellsArray = new byte[100,100];
	List<int []> SectorPath;
	List<int []> CellularPath;
	public GameObject cellPathCube;
	int startCellY = 0;
	int startCellX = 60;
	int endCellY = 60;
	int endCellX = 0;

	void Start () {
//		name spaeks for itself
//		SectorPath = FindSectorPath (startCellY,startCellX,endCellY,endCellX);

		/*
		// Print all Sectors in path
		for (int i = 0; i < SectorPath.Count; i++) {
			print (SectorPath[i][0] + ", " + SectorPath[i][1]);
		}
		*/

		// It is PATHFINDING !!! CellularPath = FindCellularPath(SectorPath,startCellY,startCellX,endCellY,endCellX, 0);

		/*
		// This makes path from now point to the very end point. Used to fill the rest of path
		while(startCellY != endCellY || startCellX != endCellX){
			if (startCellY != endCellY) {
				startCellY = startCellY + 1;
			}
			if (startCellX != endCellX) {
				startCellX = startCellX + 1;
			}
			CellularPath.Add (new int[2] {startCellY, startCellX});
			print ("Y: " + startCellY + ", X: " + startCellX);
		}
		*/

		/*
		It is Cubes of Path
		for (int i = 0; i < CellularPath.Count; i++) {
			Instantiate (cellPathCube, new Vector3 (0.5f + CellularPath[i][0],0.35f,0.5f + CellularPath[i][1]), Quaternion.identity);
		}
		*/
	}

	static List<int []> FindCellularPath(List<int []> inputSectorPath, int startCellY, int startCellX, int endCellY, int endCellX, int cellAdjustment){
		List<int []> cellularPath = new List<int[]>();
		for (int i = 1; i < inputSectorPath.Count - 2; i++) { // between 2nd Sector from start AND 3rd from end Sector in path; below must return their ends
//			print ("Y: " + inputSectorPath[i][0] + "X: " + inputSectorPath[i][1]);
			int Ysec;
			int Xsec;
			if (inputSectorPath [i] [0] < inputSectorPath [i + 1] [0]) {	// UP
				Ysec = inputSectorPath[i][0] * 10 + 9;
				Xsec = inputSectorPath[i][1] * 10 + 5;
				while(startCellY != Ysec || startCellX != Xsec){
					if (startCellY < Ysec) {
						startCellY = startCellY + 1;
					} else if (startCellY > Ysec) {
						startCellY = startCellY - 1;
					}
					if (startCellX < Xsec) {
						startCellX = startCellX + 1;
					} else if (startCellX > Xsec) {
						startCellX = startCellX - 1;
					}
					cellularPath.Add (new int[2] { startCellY, startCellX});
//					print ("Y: " + startCellY + ", X: " + startCellX);
				}
			} else if (inputSectorPath [i] [0] > inputSectorPath [i + 1] [0]) {	// DOWN
				Ysec = inputSectorPath[i][0] * 10;
				Xsec = inputSectorPath[i][1] * 10 + 5;
				while(startCellY != Ysec || startCellX != Xsec){
					if (startCellY < Ysec) {
						startCellY = startCellY + 1;
					} else if (startCellY > Ysec) {
						startCellY = startCellY - 1;
					}
					if (startCellX < Xsec) {
						startCellX = startCellX + 1;
					} else if (startCellX > Xsec) {
						startCellX = startCellX - 1;
					}
					cellularPath.Add (new int[2] { startCellY, startCellX});
//					print ("Y: " + startCellY + ", X: " + startCellX);
				}
			} else if (inputSectorPath [i] [1] > inputSectorPath [i + 1] [1]) {	// LEFT
				Ysec = inputSectorPath[i][0] * 10 + 5;
				Xsec = inputSectorPath[i][1] * 10;
				while(startCellY != Ysec || startCellX != Xsec){
					if (startCellY < Ysec) {
						startCellY = startCellY + 1;
					} else if (startCellY > Ysec) {
						startCellY = startCellY - 1;
					}
					if (startCellX < Xsec) {
						startCellX = startCellX + 1;
					} else if (startCellX > Xsec) {
						startCellX = startCellX - 1;
					}
					cellularPath.Add (new int[2] { startCellY, startCellX});
//					print ("Y: " + startCellY + ", X: " + startCellX);
				}
			} else if (inputSectorPath [i] [1] < inputSectorPath [i + 1] [1]) {	// RIGHT
				print("right");
				Ysec = inputSectorPath[i][0] * 10 + 5;
				Xsec = inputSectorPath[i][1] * 10 + 9;
				while(startCellY != Ysec || startCellX != Xsec){
					if (startCellY < Ysec) {
						startCellY = startCellY + 1;
					} else if (startCellY > Ysec) {
						startCellY = startCellY - 1;
					}
					if (startCellX < Xsec) {
						startCellX = startCellX + 1;
					} else if (startCellX > Xsec) {
						startCellX = startCellX - 1;
					}
					cellularPath.Add (new int[2] { startCellY, startCellX});
//					print ("Y: " + startCellY + ", X: " + startCellX);
				}
			}
		}
		return cellularPath;
	}

	static List<int []> FindSectorPath(int startCellY, int startCellX, int endCellY, int endCellX){
		List<int []> sectorPath = new List<int[]>();
		int sy = startCellY / 10;
		int sx = startCellX / 10;
		int ey = endCellY / 10;
		int ex = endCellX / 10;
		bool Yaxis = false;
		print ("start: " + sy + "," + sx + " end: " + ey + "," + ex);
		sectorPath.Add(new int[2] {sy, sx} );
		for (int i = 0; i < 9999; i++) {
			if (sx == ex) {  //STRAIGHT X
				if (ey == sy) {	// Stop if at place
					break;
				} else if (ey > sy) { // go UP
					sy = sy + 1;
					sectorPath.Add(new int[2] {sy, sx} );
				} else { // go DOWN
					sy = sy - 1;
					sectorPath.Add(new int[2] {sy, sx} );
				}
			} else if (sy == ey) {  //STRAIGHT Y
				if (ex > sx) {  // go RIGHT
					sx = sx + 1;
					sectorPath.Add(new int[2] {sy, sx} );
				} else if (ex < sx) {  // go LEFT
					sx = sx - 1;
					sectorPath.Add(new int[2] {sy, sx} );
				}
			} else {  // else is: sx != ex && sy != ey  NON-STRAIGHT
				if (!Yaxis) {
					if (ey < sy) {
						sy = sy - 1;
						sectorPath.Add (new int[2] { sy, sx } );
					} else {
						sy = sy + 1;
						sectorPath.Add (new int[2] { sy, sx } );
					}
					Yaxis = true;
				} else {
					if (ex < sx) {
						sx = sx - 1;
						sectorPath.Add (new int[2] { sy, sx } );
					} else {
						sx = sx + 1;
						sectorPath.Add (new int[2] { sy, sx } );
					}
					Yaxis = false;
				}
			}
		}
		return sectorPath;
	}

	public Vector3 DemoGiveNextCell(int demoUnitYpos, int demoUnitXpos, int demoUnitTargYpos, int demoUnitTargXpos){
		Vector3 nextCell = new Vector3 (0, 0, 0);
		if (demoUnitYpos > demoUnitTargYpos) {
			nextCell.z = demoUnitYpos - 1;
		} else if (demoUnitYpos < demoUnitTargYpos) {
			nextCell.z = demoUnitYpos + 1;
		} else {
			nextCell.z = demoUnitYpos;
		}
		if (demoUnitXpos > demoUnitTargXpos) {
			nextCell.x = demoUnitXpos - 1;
		} else if (demoUnitXpos < demoUnitTargXpos) {
			nextCell.x = demoUnitXpos + 1;
		} else {
			nextCell.x = demoUnitXpos;
		}
		if (demoUnitYpos == demoUnitTargYpos) {
			if (demoUnitXpos == demoUnitTargXpos) {
				nextCell.y = -10;	// if target is done, y = -10 to indicate it
			}
		}
		return nextCell;
	}
}
