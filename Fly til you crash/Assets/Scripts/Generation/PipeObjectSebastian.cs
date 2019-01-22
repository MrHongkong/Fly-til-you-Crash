using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Sebastian Nilsson
/// </summary>

public class PipeObjectSebastian : MonoBehaviour
{
    [SerializeField] internal Transform endTransPos;
    [SerializeField] internal Transform midPoint;
    internal enum PipeType {straight, curve, split};
    [SerializeField]internal PipeType typeOfPipe;
}