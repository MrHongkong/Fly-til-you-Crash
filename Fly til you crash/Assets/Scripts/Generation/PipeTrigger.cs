using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Sebastian Nilsson
/// </summary>

public class PipeTrigger : MonoBehaviour
{
    private bool hasTrigged;
    internal PipeGenerationSebastian generation;
    [SerializeField]private Transform endPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (!hasTrigged)
        {
            generation.GeneratePipe(endPoint);
            hasTrigged = true;
        }
    }
}