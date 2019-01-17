using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGenerationSebastian : MonoBehaviour
{
    [SerializeField] private GameObject[] pipePrefabs;
    private List<PipeObjectSebastian> pipes = new List<PipeObjectSebastian>();
    private GameObject bannedPipe;
    private Vector3 startPos = new Vector3(0, 0, 0);
    int index = 0;
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GeneratePipe();
            //Debug.Log(startPos);
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
    }
    void GeneratePipe()
    {
        index = (int)Random.Range(0, pipePrefabs.Length);
        if(pipes.Count > 0)
            startPos -= pipes[pipes.Count - 1].GetEndPosition();
        pipes.Add(Instantiate<GameObject>(pipePrefabs[index], startPos, pipePrefabs[index].transform.rotation).GetComponent<PipeObjectSebastian>());
        if (pipes[pipes.Count - 1].typeOfPipe == PipeObjectSebastian.PipeType.smallCurve || pipes[pipes.Count - 1].typeOfPipe == PipeObjectSebastian.PipeType.bigCurve)
        {
            CurveRotation();
        }
        if (pipes.Count >= 2)
        {
            Vector3 rotation2 = pipes[pipes.Count - 2].transform.rotation.eulerAngles;
            if (pipes[pipes.Count - 2].typeOfPipe == PipeObjectSebastian.PipeType.bigCurve)
            {
                rotation2 += new Vector3(-90, 0, 0);
                pipes[pipes.Count - 1].transform.Rotate(rotation2);
            }
            else
            {
                pipes[pipes.Count - 1].transform.Rotate(rotation2);
            }
        }
    }
    void CurveRotation()
    {
        Vector3 rotation1 = pipes[pipes.Count - 1].transform.rotation.eulerAngles;
        rotation1.y = Random.Range(0, 45);
        //rotation1.x *= Mathf.Sign(Random.Range(-10, 10));
        //Debug.Log(rotation1.x);
        pipes[pipes.Count - 1].transform.rotation = Quaternion.Euler(rotation1);
    }
    void DestroyPipe()
    {
        //Debug.Log("Remove");
        GameObject.Destroy(pipes[0].gameObject);
        pipes.RemoveAt(0);
    }
}