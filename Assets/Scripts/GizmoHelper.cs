using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoHelper : MonoBehaviour {
    public List<List<Vector3>> positions = new List<List<Vector3>>();
    public Vector3 _gizmocorrector;
    public float tilesize;
    public bool gizmohelperenabled;
    public float gizmoelevation;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (gizmohelperenabled && positions.Count > 0)
        {
            for (int i = 0; i < positions.Count; i++)
        {
            Gizmos.DrawLine(positions[i][0] + _gizmocorrector + new Vector3(0, gizmoelevation, 0), (positions[i][positions[i].Count - 1] + _gizmocorrector) + new Vector3(tilesize, gizmoelevation, 0));
            if (i == positions.Count - 1) Gizmos.DrawLine((positions[i][0] + _gizmocorrector) + new Vector3(0, gizmoelevation, -tilesize), (positions[i][positions[i].Count - 1] + _gizmocorrector) + new Vector3(tilesize, gizmoelevation, -tilesize));
            for (int e = 0; e < positions[i].Count; e++)
            {
                Gizmos.DrawLine(positions[0][e] + _gizmocorrector + new Vector3(0, gizmoelevation, 0), (positions[positions.Count - 1][e] + _gizmocorrector) + new Vector3(0, gizmoelevation, -tilesize));
                if (e == positions[i].Count - 1) Gizmos.DrawLine((positions[0][e] + _gizmocorrector) + new Vector3(tilesize, gizmoelevation, 0), (positions[positions.Count - 1][e] + _gizmocorrector) + new Vector3(tilesize, gizmoelevation, -tilesize));
            }
        }

        }
    }
}
