using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Sebastian Nilsson
/// </summary>

public class PipeGenerationSebastian : MonoBehaviour
{
    [SerializeField] private GameObject[] pipePrefabs;
    [SerializeField] private GameObject startPrefabPiece;
    private List<PipeObjectSebastian> pipes = new List<PipeObjectSebastian>();
    int prefabIndex = 0;
    void Start()
    {
        Vector3 startPos = new Vector3(0, 0, 0);
        for (int i = 0; i < 1; i++)
        {
            pipes.Add(Instantiate<GameObject>(pipePrefabs[0], startPos, pipePrefabs[0].transform.rotation).GetComponent<PipeObjectSebastian>());
            startPos = pipes[pipes.Count - 1].endTransPos.position;
            pipes[pipes.Count - 1].GetComponentInChildren<PipeTrigger>().generation = GetComponent<PipeGenerationSebastian>();
        }
    }
    internal void GeneratePipe(Transform startTransform)
    {
        prefabIndex = (int)Random.Range(0, pipePrefabs.Length);
        pipes.Add(Instantiate<GameObject>(pipePrefabs[prefabIndex], startTransform.position, 
            startTransform.rotation).GetComponent<PipeObjectSebastian>());
        pipes[pipes.Count - 1].GetComponentInChildren<PipeTrigger>().generation = GetComponent<PipeGenerationSebastian>();
        if (pipes[pipes.Count - 1].typeOfPipe != PipeObjectSebastian.PipeType.straight)
        {
            PipeRotation();
        }
        if (pipes.Count > 10)
        {
            DestroyPipe();
        }
    }
    void PipeRotation()
    {
        Vector3 rotationAxis = pipes[pipes.Count - 2].endTransPos.position - pipes[pipes.Count - 2].midPoint.position;
        pipes[pipes.Count - 1].transform.Rotate(rotationAxis, Random.Range(0, 360), Space.World);
    }
    void DestroyPipe()
    {
        GameObject.Destroy(pipes[0].gameObject);
        pipes.RemoveAt(0);
    }
}