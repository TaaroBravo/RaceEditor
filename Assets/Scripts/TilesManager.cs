using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class TilesManager : Editor
{
    public static List<Tile> allTiles = new List<Tile>();

    public static void SearchingAllTiles()
    {
        allTiles.Clear();
        foreach (var tile in GameObject.FindGameObjectsWithTag("Tile"))
            allTiles.Add(tile.GetComponent<Tile>());
    }
}