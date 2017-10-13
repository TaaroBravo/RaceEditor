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

    public Vector3 myPos;
    public List<GameObject> neighborhoods = new List<GameObject>();

    void Update()
    {
        if (Application.isEditor)
        {
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

    void CalculteNeighborhoods()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward * (_xBound + 0.5f), out hit))
            neighborhoods.Add(hit.collider.gameObject);
        if (Physics.Raycast(transform.position, -transform.forward * (_xBound + 0.5f), out hit))
            neighborhoods.Add(hit.collider.gameObject);
        if (Physics.Raycast(transform.position, transform.right * (_zBound + 0.5f), out hit))
            neighborhoods.Add(hit.collider.gameObject);
        if (Physics.Raycast(transform.position, -transform.right * (_zBound + 0.5f), out hit))
            neighborhoods.Add(hit.collider.gameObject);
    }

    void CalculateOpenSpace()
    {
        //Como hacer un enroque entre tiles?

        //List<bool> boolList = new List<bool>();
        //boolList.Add(Physics.Raycast(transform.position, transform.forward, _xBound + 0.5f));
        //boolList.Add(Physics.Raycast(transform.position, -transform.forward, _xBound + 0.5f));
        //boolList.Add(Physics.Raycast(transform.position, transform.right, _zBound + 0.5f));
        //boolList.Add(Physics.Raycast(transform.position, -transform.right, _zBound + 0.5f));
        //foreach (var _bool in boolList)
        //{
        //    if(!_bool)
        //    {
        //        transform.position = new Vector3(6, 6, 6);
        //    }
        //}
    }

    void Collisions()
    {
        foreach (var obj in Physics.OverlapBox(transform.position, GetComponent<Collider>().bounds.extents))
        {
            if (obj.transform.gameObject.tag == "Tile")
                Debug.Log("Hola");
        }
        
            
    }

    void OnCollisionEnter(Collision c)
    {
        print("Hola");
        if(c.transform.gameObject.tag == "Tile")
        {
            CalculateOpenSpace();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * (_xBound + 0.5f));
        Gizmos.DrawLine(transform.position, transform.position - transform.forward * (_xBound + 0.5f));
        Gizmos.DrawLine(transform.position, transform.position + transform.right * (_zBound + 0.5f));
        Gizmos.DrawLine(transform.position, transform.position -transform.right * (_zBound + 0.5f));
        //Gizmos.DrawCube(transform.position, GetComponent<Collider>().bounds.extents * 2);
    }
}
