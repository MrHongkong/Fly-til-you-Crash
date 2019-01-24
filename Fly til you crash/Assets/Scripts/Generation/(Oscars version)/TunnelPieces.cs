using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script creator: Oscar Oders - Last Updated: 2019-01-23
//Adjustments: 

public class TunnelPieces : MonoBehaviour {

    internal Transform startPoint;
    [SerializeField] internal Transform endPoint;
    [SerializeField] internal Transform midPoint;

    private void Awake() {

        startPoint = GetComponent<Transform>();
    }
}