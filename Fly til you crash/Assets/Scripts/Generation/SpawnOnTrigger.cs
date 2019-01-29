using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// Script creator: Sebastian Nilsson - Last updated: 2019-01-24 , Oscar Oders - last updated: 2019-01-29
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
        generator.GenerateNewTunnelPiece(generator.GetArrayIndex(), true);
        Destroy(gameObject);
    }
}