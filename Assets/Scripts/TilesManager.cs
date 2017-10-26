
//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class TilesManager : Editor
{
    public static List<Tile> allTiles = new List<Tile>();
    public static bool imDrawing;
    public static bool imCleaning;

    void Update()
    {
        if (imDrawing)
            DrawRoads();
    }

    public static void DrawRoads()
    {
        foreach (var item in allTiles)
        {
            if(item.gameObject == Selection.activeGameObject)
            {
                Debug.Log("Dibujando este Road");
                item.imRoad = true;
            }
            //Debug.Log(Selection.activeGameObject);
        }
    }

    public static void CleanSelectedRoads()
    {
        foreach (var item in allTiles)
        {
            if (item.gameObject == Selection.activeGameObject)
            {
                item.imRoad = false;
            }
        }
    }

    public static void SearchingAllTiles()
    {
        allTiles.Clear();
        foreach (var tile in GameObject.FindGameObjectsWithTag("Tile"))
        {
            allTiles.Add(tile.GetComponent<Tile>());
            //if (allTiles.Count != 0)
            //{
            //    for (int i = 0; i < allTiles.Count; i++)
            //    {
            //        if (allTiles[i] != tile)
            //            allTiles.Add(tile.GetComponent<Tile>());
            //    }
            //}
            //else
            //    allTiles.Add(tile.GetComponent<Tile>());
        }
    }
}