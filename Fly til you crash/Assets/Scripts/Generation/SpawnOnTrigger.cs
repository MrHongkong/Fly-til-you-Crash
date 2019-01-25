using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// Script creator: Sebastian Nilsson, Oscar Oders 2019-01-24
/// Adjustments:  
/// 
/// Known problems: 
/// 
/// </summary>

public class SpawnOnTrigger : MonoBehaviour
{
    internal static TunnelGenarator generator;
    private void OnTriggerEnter(Collider other)
    {
        generator.GenerateNewTunnelPiece();
        Destroy(gameObject);
    }
}