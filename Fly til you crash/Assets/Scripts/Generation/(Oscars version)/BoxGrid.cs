using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script creators: Oscar Oders

//Class BoxGrid: to use as a tracking of where there already has been generated objects.
public class BoxGrid
{
    public const float gridSize = 2f;
    public Vector3 cellCenter;
    BoxFace boxFace;

    public BoxGrid(Vector3 _cellCenter) {
        cellCenter = _cellCenter;
        boxFace = new BoxFace(cellCenter, gridSize);
    }


    // returns cellCenter
    public Vector3 GetCellCenter() {
        return cellCenter;
    }

    //returns boxFace
    public BoxFace GetBoxFace() {
        return boxFace;
    }
}

//Struct BoxFace: defines thecenterpoint of the faces of the "grid".
public struct BoxFace {
    Vector3 up, down, forward, back, right, left;

    public BoxFace(Vector3 cellCenter, float gridSize) {
        up = cellCenter + Vector3.up * (gridSize / 2); 
        down = cellCenter - Vector3.up * (gridSize / 2);
        forward = cellCenter + Vector3.forward * (gridSize / 2);
        back = cellCenter - Vector3.forward * (gridSize / 2);
        right = cellCenter + Vector3.right * (gridSize / 2);
        left = cellCenter - Vector3.right * (gridSize / 2);
    }
}
