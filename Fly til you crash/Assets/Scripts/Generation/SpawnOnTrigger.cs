using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script creator: Sebastian Nilsson, Oscar Oders 2019-01-24
/// </summary>
public class SpawnOnTrigger : MonoBehaviour
{
    internal TunnelGenarator generator;
    private void OnTriggerEnter(Collider other)
    {
        generator.GenerateNewTunnelPiece();
        Destroy(gameObject);
    }
}