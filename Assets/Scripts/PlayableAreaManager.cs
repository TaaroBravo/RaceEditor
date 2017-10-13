using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class PlayableAreaManager : MonoBehaviour {

    // PlaceHolders de tiles;
    //public GameObject tile1;
    //public GameObject tile2;
    //public GameObject tile3;

    // Listas para armar la matriz (publicas para que las muestre el editor)
    public List<List<Vector3>> positions;
    public List<List<int>> tiles;
    public List<int> test;
    public List<Vector3> test2;

    // variables publicas usadas en el editor
    public int matrixsizey;
    public int matrixsizex;
    public bool createlevel; // scrap del proyecto anterior, no va a quedar
    public bool creatematrixandmap;
    public float tilesize;

    // variables internas
    private int _maplength;
    private bool _drawgizmomatrix;
    private Vector3 _gizmocorrector;

    void Awake()
    {
        if (tilesize <= 0) tilesize = 6.4f;
        tiles = new List<List<int>>();
        test = new List<int>();
        test2 = new List<Vector3>();
        positions = new List<List<Vector3>>();
        _gizmocorrector = new Vector3(-tilesize/2, tilesize/2, 0);
    }
    void Update()
    {
        if (createlevel)
        {
            //ClearCurrentMap();
            CreateMap(positions, tiles);
            createlevel = false;
        }
        if (creatematrixandmap)
        {
            //ClearCurrentMap();
            UpdateMatrixSize();
            CreateMap(positions, tiles);
            _drawgizmomatrix = true;
            creatematrixandmap = false;
        }
    }
    void CreateMap(List<List<Vector3>> positions, List<List<int>> tiles)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            AssignPositions(positions[i], (-i* tilesize));
            //CreateTiles(positions[i], tiles[i]);
        }
    }
    void AssignPositions(List<Vector3> positions, float yposition)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            positions[i] = new Vector2((i * tilesize), yposition);
        }
    }
    //void CreateTiles(List<Vector3> positions, List<int> tiles)
    //{
    //    for (int i = 0; i < positions.Count; i++)
    //    {
    //        if (tiles[i] == 1) Instantiate(tile1).transform.position = positions[i];
    //        if (tiles[i] == 2) Instantiate(tile2).transform.position = positions[i];
    //        if (tiles[i] == 3) Instantiate(tile3).transform.position = positions[i];
    //    }
    //}
    void UpdateMatrixSize()
    {
        test.Clear();
        tiles.Clear();
        test2.Clear();
        positions.Clear();
        for (int i = 0; i < matrixsizey; i++)
        {
            test.Add(0);
            test2.Add(new Vector2());
        }
        for (int i = 0; i < matrixsizex; i++)
        {
            tiles.Add(new List<int>(test));
            positions.Add(new List<Vector3>(test2));
        }
    }
    //void ClearCurrentMap()
    //{
    //GameObject[] _playeabletiles = GameObject.FindGameObjectsWithTag("PlayeableTiles");
    //GameObject[] _roadtiles = GameObject.FindGameObjectsWithTag("RoadTiles");
    //GameObject[] _watertiles = GameObject.FindGameObjectsWithTag("WaterTiles");
    //    if (_playeabletiles.Length != 0)
    //    {
    //        for (int i = 0; i < _playeabletiles.Length; i++)
    //        {
    //            Destroy(_playeabletiles[i]);
    //        }
    //    }
    //    if (_roadtiles.Length != 0)
    //    {
    //        for (int i = 0; i < _roadtiles.Length; i++)
    //        {
    //            Destroy(_roadtiles[i]);
    //        }
    //    }
    //    if (_watertiles.Length != 0)
    //    {
    //        for (int i = 0; i < _watertiles.Length; i++)
    //        {
    //            Destroy(_watertiles[i]);
    //        }
    //    }
    //}
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (_drawgizmomatrix)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                Gizmos.DrawLine(positions[i][0] + _gizmocorrector, (positions[i][positions[i].Count - 1] + _gizmocorrector) + new Vector3(tilesize, 0, 0));
                if (i == positions.Count-1) Gizmos.DrawLine((positions[i][0] + _gizmocorrector) + new Vector3(0, -tilesize, 0), (positions[i][positions[i].Count - 1] + _gizmocorrector) + new Vector3(tilesize, -tilesize, 0));
                for (int e = 0; e < positions[i].Count; e++)
                {
                    Gizmos.DrawLine(positions[0][e] + _gizmocorrector, (positions[positions.Count - 1][e] + _gizmocorrector) + new Vector3(0, -tilesize, 0));
                    if (e == positions[i].Count - 1) Gizmos.DrawLine((positions[0][e] + _gizmocorrector) + new Vector3(tilesize, 0, 0), (positions[positions.Count - 1][e] + _gizmocorrector) + new Vector3(tilesize, -tilesize, 0));
                }
            }

        }
    }
}
