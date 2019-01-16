using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeObjectSebastian : MonoBehaviour
{
    [SerializeField]private Vector3 startPosition;
    [SerializeField]internal Vector3[] endPositions;
    [SerializeField] private Transform[] endTransforms;
    private void Start()
    {
        GenerateEndPositions();
    }
    internal void GenerateEndPositions()
    {
        foreach (Transform endLocation in endTransforms)
        {
            endPositions[0] = transform.position - endLocation.position;
            //Debug.Log(transform.position - endLocation.position);
        }
    }
    internal Vector3 GetEndPosition()
    {
        GenerateEndPositions();
        return endPositions[0];
    }
}