using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGenerationSebastian : MonoBehaviour
{
    [SerializeField] private GameObject[] pipePrefabs;
    private List<PipeObjectSebastian> pipes = new List<PipeObjectSebastian>();
    private GameObject bannedPipe;
    private Vector3 startPos = new Vector3(0, 0, 0);
    void Start()
    {
        int index = 0;
        
        for (int i = 0; i < 10; i++)
        {
            index = (int)Random.Range(0, pipePrefabs.Length);
            pipes.Add(Instantiate<GameObject>(pipePrefabs[index], startPos, pipePrefabs[index].transform.rotation).GetComponent<PipeObjectSebastian>());
            startPos -= pipes[pipes.Count - 1].GetEndPosition();

            //Debug.Log(startPos);
        }
    }
    void Update()
    {
        
    }
}