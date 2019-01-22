using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Sebastian Nilsson
/// </summary>

public class PipeTrigger : MonoBehaviour
{
    private PipeGenerationSebastian generation;
    private void Start()
    {
        generation = GameObject.Find("PipeGenerator").GetComponent<PipeGenerationSebastian>();
    }
    private void OnTriggerEnter(Collider other)
    {
        generation.GeneratePipe(transform);
    }
}