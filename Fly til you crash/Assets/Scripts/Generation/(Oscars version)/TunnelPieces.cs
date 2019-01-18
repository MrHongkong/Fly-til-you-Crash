using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script creators: Oscar Oders

public class TunnelPieces : MonoBehaviour{

    Transform startPoint;
    public Transform endPoint;

    private void Awake() {
        startPoint = GetComponent<Transform>();
    }
}






























//GameObject tunnelPiece;
//BoxGrid startCell, endCell;
//Vector3 startPoint, endPoint;

//public TunnelPieces(GameObject _tunnelPiece, Vector3 _startPoint) {
//    tunnelPiece = _tunnelPiece;
//    startPoint = _startPoint;
//}

//void Create(Transform lastEndTransform) {
//    GameObject piece = Instantiate(tunnelPiece);
//    Transform tempTransform = piece.transform;
//    tempTransform.rotation = lastEndTransform.rotation;
//    tempTransform.position = lastEndTransform.position;

//    piece.transform.position = tempTransform.position;
//    piece.transform.rotation = tempTransform.rotation;
//}



//internal Vector3 CalculateEnd() {
//    Vector3 endPoint = tunnelPiece.transform.localScale;
//    endPoint = new Vector3(endPoint.x / 2, endPoint.y / 2, endPoint.z);
//}
