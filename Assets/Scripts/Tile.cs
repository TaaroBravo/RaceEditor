using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    //Agregar elevaciones?
    //Como hacer un enroque entre tiles?
    public float gridScale = 2;
    public float yPos = 0;
    public float _xBound;
    public float _zBound;

    public bool imRoad;

    public Vector3 myPos;
    public List<GameObject> neighborhoods = new List<GameObject>();
    public List<int> connections = new List<int> { 0, 0, 0, 0 };

    private int nTile;
    private int oTile;
    private int eTile;
    private int sTile;
    public int index = 0;
    public List<Texture2D> allTexture = new List<Texture2D>();

    void Update()
    {
        if (Application.isEditor)
        {
            if (neighborhoods == null)
            {
                if (imRoad)
                    CalculateNeighborhoods();
                else
                    NotRoad();
            }
            if (myPos != transform.position)
            {
                CalculateCollision();
                if (imRoad)
                {
                    neighborhoods.Clear();
                    CalculateNeighborhoods();
                }
                else
                    NotRoad();
            }
            myPos = transform.position;
            if (_xBound == 0)
                CalculateBounds();
            Vector3 currentPosition = transform.position;
            float xDifference = currentPosition.x % gridScale;
            float zDifference = currentPosition.z % gridScale;
            transform.position = new Vector3(currentPosition.x - xDifference, yPos, currentPosition.z - zDifference);
        }
    }

    void CalculateBounds()
    {
        _xBound = GetComponent<Collider>().bounds.extents.x;
        _zBound = GetComponent<Collider>().bounds.extents.z;
    }

    void NotRoad()
    {
        GetComponent<Renderer>().sharedMaterial = new Material (Shader.Find("Diffuse"));
    }

    void CalculateNeighborhoods()
    {
        index = 0;
        oTile = 0;
        nTile = 0;
        eTile = 0;
        sTile = 0;
        RaycastHit hit;
        //neighborhoods.Add(Physics.Raycast(transform.position, -transform.right * (_zBound + 0.5f), out hit) ? hit.collider.gameObject : null);
        //neighborhoods.Add(Physics.Raycast(transform.position, transform.forward * (_xBound + 0.5f), out hit) ? hit.collider.gameObject : null);
        //neighborhoods.Add(Physics.Raycast(transform.position, transform.right * (_zBound + 0.5f), out hit) ? hit.collider.gameObject : null);
        //neighborhoods.Add(Physics.Raycast(transform.position, -transform.forward * (_xBound + 0.5f), out hit) ? hit.collider.gameObject : null);
        if (Physics.Raycast(transform.position, -transform.right, out hit, _zBound + 0.5f))
        {
            neighborhoods.Add(hit.collider.gameObject);
            eTile = 1;
        }
        if (Physics.Raycast(transform.position, transform.forward, out hit, _xBound + 0.5f))
        {
            neighborhoods.Add(hit.collider.gameObject);
            sTile = 1;
        }
        if (Physics.Raycast(transform.position, transform.right, out hit, _zBound + 0.5f))
        {
            neighborhoods.Add(hit.collider.gameObject);
            oTile = 1;
        }
        if (Physics.Raycast(transform.position, -transform.forward, out hit, _xBound + 0.5f))
        {
            neighborhoods.Add(hit.collider.gameObject);
            nTile = 1;
        }
        index = nTile * 1 + eTile * 2 + sTile * 4 + oTile * 8;
        GetComponent<Renderer>().sharedMaterial.mainTexture = allTexture[index];

    }

    void CalculateCollision()
    {
        //Como hacer un enroque entre tiles?

        foreach (var obj in Physics.OverlapBox(transform.position, GetComponent<Collider>().bounds.extents / 2))
        {
            if (obj.transform.gameObject.tag == "Tile" && obj.transform.gameObject != transform.gameObject)
            {
                obj.transform.position = myPos;
                //Vector3 currentPosition = myPos;
                //float xDifference = currentPosition.x % gridScale;
                //float zDifference = currentPosition.z % gridScale;
                //obj.transform.position = new Vector3(currentPosition.x + xDifference, yPos, currentPosition.z + zDifference);
                //myPos = transform.position;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * (_xBound + 0.5f));
        Gizmos.DrawLine(transform.position, transform.position - transform.forward * (_xBound + 0.5f));
        Gizmos.DrawLine(transform.position, transform.position + transform.right * (_zBound + 0.5f));
        Gizmos.DrawLine(transform.position, transform.position - transform.right * (_zBound + 0.5f));
        //Gizmos.DrawCube(transform.position, GetComponent<Collider>().bounds.extents * 2);
    }
}