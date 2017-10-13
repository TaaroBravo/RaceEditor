using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(PlayableAreaManager))]
public class BackgroundAreaEditor : Editor {

    private PlayableAreaManager _target;
    void OnEnable()
    {
        _target = (PlayableAreaManager)target;
    }
    public override void OnInspectorGUI()
    {
        ShowValues();
        FixValues();
        Repaint();
    }
    private void ShowValues()
    {
        EditorGUILayout.LabelField("Parameters", EditorStyles.boldLabel);
        //_target.tile1 = (GameObject)EditorGUILayout.ObjectField("Surface Object Tile 1", _target.tile1, typeof(GameObject), true);
        //_target.tile2 = (GameObject)EditorGUILayout.ObjectField("Surface Object Tile 2", _target.tile2, typeof(GameObject), true);
        //_target.tile3 = (GameObject)EditorGUILayout.ObjectField("Surface Object Tile 3", _target.tile3, typeof(GameObject), true);
        _target.tilesize = EditorGUILayout.FloatField("Tamaño de los Tiles", _target.tilesize);
        _target.matrixsizey = EditorGUILayout.IntField("Cuadraditos de Largo (en X)", _target.matrixsizey);
        _target.matrixsizex = EditorGUILayout.IntField("Cuadraditos de Alto (en Y)", _target.matrixsizex);
        //EditorGUILayout.BeginHorizontal(); // ESTO LO NECESITO PARA ALGO, LA VDD NO ME ACUERDO QUE :l...
        //{
        //    if (GUILayout.Button("Generate Map"))
        //    {
        //        _target.createlevel = true;
        //    }
        //    if (GUILayout.Button("Disable (in editor)"))
        //    {
        //        _target.createlevel = false;
        //    }
        //    if (_target.createlevel) GUI.DrawTexture(GUILayoutUtility.GetRect(15, 15, 15, 15), (Texture2D)Resources.Load("enabled"));
        //    else GUI.DrawTexture(GUILayoutUtility.GetRect(15, 15, 15, 15), (Texture2D)Resources.Load("disabled"));
        //}
        //EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Crear Matriz"))
            {
                _target.creatematrixandmap = true;
            }
            if (GUILayout.Button("Disable (in editor)"))
            {
                _target.creatematrixandmap = false;
            }
            if (_target.creatematrixandmap) GUI.DrawTexture(GUILayoutUtility.GetRect(15, 15, 15, 15), (Texture2D)Resources.Load("enabled"));
            else GUI.DrawTexture(GUILayoutUtility.GetRect(15, 15, 15, 15), (Texture2D)Resources.Load("disabled"));
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("PARA HACER LA MATRIZ EN EDITOR MODE", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("MANDALE LOS PARAMETROS QUE QUERES Y", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("MOVE EL MANAGER UN TOKE PARA QUE PASE", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("DE FRAME, DPS LO HACEMOS BIEN", EditorStyles.boldLabel);
    }
    private void FixValues()
    {
        if (_target.matrixsizey <= 0) _target.matrixsizey = 1;
        if (_target.matrixsizex <= 0) _target.matrixsizex = 1;
        if (_target.matrixsizex <= 0) _target.tilesize= 1;
    }
}

