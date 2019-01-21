using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script creator: Oscar Oders
//Adjustments: 

public class TunnelPieces : MonoBehaviour{

    [HideInInspector]
    public Transform startPoint;

    public Transform endPoint;

    private void Awake() {
        startPoint = GetComponent<Transform>();
    }
}