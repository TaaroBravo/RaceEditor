using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class MatrixCreatorWindows : EditorWindow
{
    private int _xlength;
    private int _ylength;
    private bool _gizmohelpers;
    public GameObject tile1;
    public GameObject gizmohelper;
    private GameObject _gizmohelper;
    public List<List<Vector3>> positions;
    public List<List<int>> tiles;
    public List<int> test;
    public List<Vector3> test2;
    public int matrixsizey;
    public int matrixsizex;
    public bool creatematrixandmap;
    public float tilesize;
    private int _maplength;
    private bool _drawgizmomatrix;
    private Vector3 _gizmocorrector;
    public GizmoHelper helper;
    private List<Object> _assets = new List<Object>();
    private List<Texture2D> _base = new List<Texture2D>();
    private List<List<Texture2D>> _availablestilesets = new List<List<Texture2D>>();
    private List<string> _newtilesetsnames = new List<string>();
    private string _keyword;
    private Vector2 _scroll1Position;

    [MenuItem("Track Creator/STEP 1 - Size and Tiles")]
    static void ShowWindow()
    {
        ((MatrixCreatorWindows)GetWindow(typeof(MatrixCreatorWindows))).Show();
    }
    void OnGUI() // ES EL UPDATE
    {
        tiles = new List<List<int>>();
        test = new List<int>();
        test2 = new List<Vector3>();
        positions = new List<List<Vector3>>();
        GameObject[] _currentiles;
        _currentiles = GameObject.FindGameObjectsWithTag("Tile");

        if (_assets.Count > 0) _assets.Clear();
        if (_base.Count > 0) _base.Clear();
        string[] allPaths = AssetDatabase.FindAssets("Tiles");
        int i;
        for (i = 0; i < allPaths.Length; i++)
        {
            allPaths[i] = AssetDatabase.GUIDToAssetPath(allPaths[i]);
            _assets.Add(AssetDatabase.LoadAssetAtPath(allPaths[i], typeof(Object)));
        }
        for (i = 0; i < _assets.Count; i++)
        {
            if (_assets[i].GetType() == typeof(Texture2D))
            {
                _base.Add(_assets[i] as Texture2D);
            }
        }

        //Debug.Log(_assets.Count);
        //Debug.Log(_base.Count);

        EditorGUILayout.BeginVertical(GUILayout.Width(200));
        GUI.DrawTexture(GUILayoutUtility.GetRect(10, 10, 20, 50), (Texture2D)Resources.Load("TackCreation1Header"));

        GUI.DrawTexture(GUILayoutUtility.GetRect(10, 10, 20, 23), (Texture2D)Resources.Load("TackCreation2Header"));
        EditorGUILayout.BeginHorizontal(GUILayout.Width(200));

        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("X Length");
        matrixsizex = EditorGUILayout.IntField(matrixsizex);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Y Length");
        matrixsizey = EditorGUILayout.IntField(matrixsizey);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        if (GUILayout.Button("Create Matrix"))
        {
            var path = AssetDatabase.FindAssets("Tile2");
            var realpath = AssetDatabase.GUIDToAssetPath(path[0]);
            //Debug.Log(realpath);
            tile1 = (AssetDatabase.LoadAssetAtPath(realpath, typeof(Object))) as GameObject;
            var path2 = AssetDatabase.FindAssets("MapStart");
            var realpath2 = AssetDatabase.GUIDToAssetPath(path2[0]);
            //Debug.Log(realpath2);
            gizmohelper = (AssetDatabase.LoadAssetAtPath(realpath2, typeof(Object))) as GameObject;
            tilesize = 2f;
            _gizmocorrector = new Vector3(-tilesize / 2, 0, tilesize / 2);
            ClearCurrentMap();
            UpdateMatrixSize();
            CreateMap(positions);
            TilesManager.SearchingAllTiles();
            SetGizmo();
            //Debug.Log(test.Count);
            //Debug.Log(test2.Count);
            //Debug.Log(positions.Count);
        }
        if (GUILayout.Button("Reset Matrix"))
        {
            ClearCurrentMap();
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        TilesManager.imDrawing = EditorGUILayout.Toggle("Draw Roads", TilesManager.imDrawing);
        if(TilesManager.imDrawing)
        {
            TilesManager.DrawRoads();
        }
        TilesManager.imCleaning = EditorGUILayout.Toggle("Clean Selecting Roads", TilesManager.imCleaning);
        if (TilesManager.imCleaning)
        {
            TilesManager.CleanSelectedRoads();
        }

        GUI.DrawTexture(GUILayoutUtility.GetRect(10, 10, 20, 23), (Texture2D)Resources.Load("TackCreation3Header"));
        EditorGUILayout.BeginHorizontal(GUILayout.Width(200));
        EditorGUILayout.LabelField("Gizmo Helpers:", EditorStyles.boldLabel);
        if (GUILayout.Button("Allow"))
        {
            _gizmohelpers = true;
            if (helper != null) helper.gizmohelperenabled = _gizmohelpers;
            if (_gizmohelper != null) Framepass();
        }
        if (GUILayout.Button("Disable"))
        {
            _gizmohelpers = false;
            if (helper != null) helper.gizmohelperenabled = _gizmohelpers;
            if (_gizmohelper != null) Framepass();
        }
        if (_gizmohelpers) GUI.DrawTexture(GUILayoutUtility.GetRect(15, 15, 15, 15), (Texture2D)Resources.Load("enabled"));
        else GUI.DrawTexture(GUILayoutUtility.GetRect(15, 15, 15, 15), (Texture2D)Resources.Load("disabled"));
        EditorGUILayout.EndHorizontal();

        GUI.DrawTexture(GUILayoutUtility.GetRect(10, 10, 20, 23), (Texture2D)Resources.Load("TrackNewArea1"));
        EditorGUILayout.BeginHorizontal(GUILayout.Width(300));
        EditorGUILayout.BeginVertical(GUILayout.Width(50));
        EditorGUI.DrawPreviewTexture(GUILayoutUtility.GetRect(50, 50, 50, 50), _base[0]);
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical(GUILayout.Width(250));
        EditorGUILayout.LabelField("BASE TILESET (CANT BE REMOVED)", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal(GUILayout.Width(250));
        if (GUILayout.Button("Apply"))
        {
            if (_currentiles.Length > 0)
            {
                for (int e = 0; e < _currentiles.Length; e++)
                {
                    _currentiles[e].GetComponent<Tile>().allTexture = _base;
                }
                _currentiles[Random.Range(0, _currentiles.Length - 1)].transform.position += new Vector3(0, 0.05f, 0);
            }
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(GUILayout.Width(400),GUILayout.Height(200));
        if (_availablestilesets.Count > 0)
        {
            _scroll1Position = EditorGUILayout.BeginScrollView(_scroll1Position, false, false);
            for (i = 0; i < _availablestilesets.Count; i++)
            {
                EditorGUILayout.BeginHorizontal(GUILayout.Width(300));
                EditorGUILayout.BeginVertical(GUILayout.Width(50));
                //Debug.Log(_availablestilesets[i].Count);
                EditorGUI.DrawPreviewTexture(GUILayoutUtility.GetRect(50, 50, 50, 50), _availablestilesets[i][0]);
                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical(GUILayout.Width(250));
                EditorGUILayout.LabelField(_newtilesetsnames[i], EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal(GUILayout.Width(250));
                if (GUILayout.Button("Apply"))
                {
                    if (_currentiles.Length > 0)
                    {
                        for (int e = 0; e < _currentiles.Length; e++)
                        {
                            _currentiles[e].GetComponent<Tile>().allTexture = _availablestilesets[i];
                        }
                        _currentiles[Random.Range(0, _currentiles.Length - 1)].transform.position += new Vector3(0, 0.05f, 0);
                    }
                }
                if (GUILayout.Button("Remove"))
                {
                    _newtilesetsnames.RemoveAt(i);
                    _availablestilesets.RemoveAt(i);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
        }
        else EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();


        GUI.DrawTexture(GUILayoutUtility.GetRect(10, 10, 20, 23), (Texture2D)Resources.Load("TrackNewArea2"));
        EditorGUILayout.BeginHorizontal(GUILayout.Width(300));
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Keyword:", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        _keyword = EditorGUILayout.TextField(_keyword);
        if (GUILayout.Button("Add"))
        {
            if (_assets.Count > 0) _assets.Clear();
            string[] searchpath = AssetDatabase.FindAssets(_keyword);
            for (i = searchpath.Length - 1; i >= 0; i--)
            {
                searchpath[i] = AssetDatabase.GUIDToAssetPath(searchpath[i]);
                _assets.Add(AssetDatabase.LoadAssetAtPath(searchpath[i], typeof(Object)));
            }
            var _temptexturelist = new List<Texture2D>();
            for (i = _assets.Count -1; i >= 0 ; i--)
            {
                if (_assets[i].GetType() == typeof(Texture2D))
                {
                    _temptexturelist.Add(_assets[i] as Texture2D);
                }
            }
            if (_temptexturelist.Count > 0 )
            {
                _availablestilesets.Add(_temptexturelist);
                _newtilesetsnames.Add(_keyword);
            }
            //Debug.Log(_assets.Count);
            //Debug.Log(_temptexturelist.Count);
            //Debug.Log(_availablestilesets.Count);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("How to add new Tiles:", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("TO ADD NEW TILES 16 IMAGES OF THE SAME SIZE AS");
        EditorGUILayout.LabelField("BASE MUST BE PLACED ANYWHERE ON THE PROJECT");
        EditorGUILayout.LabelField("WITH A UNIQUE KEYWORD IN THE NAME BEFORE");
        EditorGUILayout.LabelField("USING THIS, USE THE BASE TILES AS REFERENCE");
        EditorGUILayout.LabelField("WHEN CREATING NEW TILESETS");
    }
    void CreateMap(List<List<Vector3>> positions/*, List<List<int>> tiles*/)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            AssignPositions(positions[i], (-i * tilesize));
        }
        CreateTiles(positions/*, tiles[i]*/);
        
    }
    void AssignPositions(List<Vector3> positions, float zposition)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            positions[i] = new Vector3((i * tilesize), 0, zposition);
        }
    }
    void CreateTiles(List<List<Vector3>> positions/*, List<int> tiles*/)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            for (int e = 0; e < positions[i].Count; e++)
            {
                Instantiate(tile1).transform.position = positions[i][e];
            }
        }
        
    }
    void Framepass()
    {
        _gizmohelper.transform.position += _gizmohelper.transform.up;
        _gizmohelper.transform.position -= _gizmohelper.transform.up;
    }
    void UpdateMatrixSize()
    {
        if (test != null) test.Clear();
        if (tiles != null) tiles.Clear();
        if (test2 != null) test2.Clear();
        if (positions != null) positions.Clear();
        for (int i = 0; i < matrixsizey; i++)
        {
            test.Add(0);
            test2.Add(new Vector3());
        }
        for (int i = 0; i < matrixsizex; i++)
        {
            tiles.Add(new List<int>(test));
            positions.Add(new List<Vector3>(test2));
        }
    }
    void ClearCurrentMap()
    {
        GameObject[] _tiles = GameObject.FindGameObjectsWithTag("Tile");
        if (_tiles.Length != 0)
        {
            for (int i = 0; i < _tiles.Length; i++)
            {
                DestroyImmediate(_tiles[i]);
            }
        }
        GameObject _gizmo = GameObject.FindGameObjectWithTag("Helper");
        DestroyImmediate(_gizmo);
    }
    void SetGizmo()
    {
        _gizmohelper = Instantiate(gizmohelper);
        helper = _gizmohelper.GetComponent<GizmoHelper>();
        helper.positions = positions;
        helper._gizmocorrector = _gizmocorrector;
        helper.tilesize = tilesize;
        helper.gizmohelperenabled = _gizmohelpers;
        helper.gizmoelevation = 0.3f;
    }
}

