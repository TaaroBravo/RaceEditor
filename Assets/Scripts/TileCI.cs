using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Tile))]
public class TileCI : Editor
{
    private void OnSceneGUI()
    {
        var tgt = (Tile)target;
        Handles.BeginGUI();
        foreach (var tile in TilesManager.allTiles)
        {
            if (tile.gameObject == Selection.activeGameObject)
            {
                if (GUI.Button(new Rect(100, 100, 75, 50), (Texture)AssetDatabase.LoadAssetAtPath("Assets/Resources/road button.jpg", typeof(Texture))))
                {
                    tile.imRoad = true;
                    tile.CalculateNeighborhoods();
                    foreach (var link in tile.neighborhoods)
                        if (link != null)
                        {
                            if (link.imRoad)
                                link.CalculateNeighborhoods();
                            else
                                link.NotRoad();
                        }
                }
                if (GUI.Button(new Rect(100, 200, 75, 50), "Clean"))
                {
                    tile.imRoad = false;
                    tile.CalculateNeighborhoods();
                    foreach (var link in tile.neighborhoods)
                        if (link != null)
                        {
                            if (link.imRoad)
                                link.CalculateNeighborhoods();
                            else
                                link.NotRoad();
                        }
                    tile.NotRoad();
                }
            }
        }
        Handles.EndGUI();
    }
}
