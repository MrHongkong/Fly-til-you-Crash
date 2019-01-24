using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script creator:   Oscar Oders - Last Updated: 2019-01-24
//Adjustments: 

//Known problems:   the BoxGridIntersection method sometimes returns true, even if cellCenter is no way near. 

//Class BoxGrid: to use as a tracking of where there already has been generated objects.
public class BoxGrid {

    internal const float gridSize = 150f;
    internal Vector3 cellCenter;
    internal BoxFace boxFace;

    internal BoxGrid(Vector3 _cellCenter) {

        cellCenter = _cellCenter;
        boxFace = new BoxFace(cellCenter, gridSize);
    }

    //Checks if the a tunnelpiece is in risk of intersect with the boxGrid.
    internal bool BoxGridIntersection(TunnelPieces currentObject, Vector3 lineDirection) {

        Vector3 lineOrigo = currentObject.endPoint.position;

        float xIntersectMin, xIntersectMax, yIntersectMin, yIntersectMax, zIntersectMin, zIntersectMax;

        xIntersectMin = (this.boxFace.left.x - lineOrigo.x) / lineDirection.x;
        xIntersectMax = (this.boxFace.right.x - lineOrigo.x) / lineDirection.x;

        yIntersectMin = (this.boxFace.down.y - lineOrigo.y) / lineDirection.y;
        yIntersectMax = (this.boxFace.up.y - lineOrigo.y) / lineDirection.y;

        if (xIntersectMin > yIntersectMax || yIntersectMin > xIntersectMax)
            return false;

        if (yIntersectMin > xIntersectMin)
            xIntersectMin = yIntersectMin;

        if (yIntersectMax < xIntersectMax)
            xIntersectMax = yIntersectMax;

        zIntersectMin = (this.boxFace.back.z - lineOrigo.z) / lineDirection.z;
        zIntersectMax = (this.boxFace.forward.z - lineOrigo.z) / lineDirection.z;

        if (xIntersectMin > zIntersectMax || zIntersectMin > xIntersectMax)
            return false;

        if (zIntersectMin > xIntersectMin)
            xIntersectMin = zIntersectMin;

        if (zIntersectMax < xIntersectMax)
            xIntersectMax = zIntersectMax;

        return true;
    }

    // returns cellCenter
    internal Vector3 GetCellCenter() {

        return cellCenter;
    }

    //returns boxFace
    internal BoxFace GetBoxFace() {

        return boxFace;
    }
}

//Struct BoxFace: defines thecenterpoint of the faces of the "grid".
public struct BoxFace {

    internal Vector3 up, down, forward, back, right, left;

    internal BoxFace(Vector3 cellCenter, float gridSize) {

        up = cellCenter + Vector3.up * (gridSize / 2); 
        down = cellCenter - Vector3.up * (gridSize / 2);
        forward = cellCenter + Vector3.forward * (gridSize / 2);
        back = cellCenter - Vector3.forward * (gridSize / 2);
        right = cellCenter + Vector3.right * (gridSize / 2);
        left = cellCenter - Vector3.right * (gridSize / 2);
    }
}