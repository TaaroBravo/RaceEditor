
﻿//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[ExecuteInEditMode]
//public class TilesManager : MonoBehaviour {

//    public List<GameObject> allTiles = new List<GameObject>();
	
//	void Update () {

//        SearchingAllTiles();
        
         
//	}

//    void SearchingAllTiles()
//    {
//        foreach (var tile in GameObject.FindGameObjectsWithTag("Tile"))
//        {
//            if (allTiles.Count != 0)
//            {
//                for (int i = 0; i < allTiles.Count; i++)
//                {
//                    if (allTiles[i] != tile)
//                        allTiles.Add(tile);
//                }
//            }
//            else
//                allTiles.Add(tile);
//        }
//    }
//}