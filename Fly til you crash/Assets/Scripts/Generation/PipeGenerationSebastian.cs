using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGenerationSebastian : MonoBehaviour
{
    [SerializeField] private GameObject[] pipePrefabs;
    private List<PipeObjectSebastian> pipes = new List<PipeObjectSebastian>();
    private GameObject bannedPipe;
    private Vector3 startPos = new Vector3(0, 0, 0);
    int index = 0, totalPipes = 0;
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GeneratePipe();
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            GeneratePipe();
            if (pipes.Count > 10)
            {
                DestroyPipe();
            }
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Pipe: " + totalPipes + "/n" + "Rotation: " + pipes[pipes.Count - 1].transform.rotation);
        }
    }
    void GeneratePipe()
    {
        totalPipes++;
        index = (int)Random.Range(0, pipePrefabs.Length);
        if(pipes.Count > 0)
            startPos -= pipes[pipes.Count - 1].GetEndPosition();
        pipes.Add(Instantiate<GameObject>(pipePrefabs[index], startPos, pipePrefabs[index].transform.rotation).GetComponent<PipeObjectSebastian>());
        if (pipes.Count >= 2)
        {
            pipes[pipes.Count - 1].transform.rotation = pipes[pipes.Count - 2].endTransforms[0].rotation;
        }
        if (pipes.Count >= 2 && pipes[pipes.Count - 1].typeOfPipe == PipeObjectSebastian.PipeType.curve)
        {
            CurveRotation();
        }
    }
    void CurveRotation()
    {
        Vector3 rotation1 = new Vector3();
        Vector3 rotationAxis = pipes[pipes.Count - 2].endTransforms[0].position - pipes[pipes.Count - 2].midPoint.position;

        pipes[pipes.Count - 1].transform.RotateAroundLocal(rotationAxis, Random.Range(0, 360));
    }
    void DestroyPipe()
    {
        GameObject.Destroy(pipes[0].gameObject);
        pipes.RemoveAt(0);
    }
}