using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Script creator:   Oscar Oders - Last Updated: 2019-01-24
/// Adjustments:
/// 
/// Known problems:   the BoxGridIntersection method sometimes returns true, even if cellCenter is no way near.
/// 
/// </summary>

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
    internal bool BoxGridIntersection(TunnelPieces currentObject, Line line) {

        Vector3 lineOrigo = currentObject.endPoint.position;

        float xIntersectMin, xIntersectMax, yIntersectMin, yIntersectMax, zIntersectMin, zIntersectMax;

        if (line.inverseDirection.x >= 0) { 

            xIntersectMin = (this.boxFace.left.x - lineOrigo.x) / line.inverseDirection.x;
            xIntersectMax = (this.boxFace.right.x - lineOrigo.x) / line.inverseDirection.x;
        } else {

            xIntersectMin = (this.boxFace.right.x - lineOrigo.x) / line.inverseDirection.x;
            xIntersectMax = (this.boxFace.left.x - lineOrigo.x) / line.inverseDirection.x;
        }

        if (line.inverseDirection.y >= 0) {

            yIntersectMin = (this.boxFace.down.y - lineOrigo.y) / line.inverseDirection.y;
            yIntersectMax = (this.boxFace.up.y - lineOrigo.y) / line.inverseDirection.y;
        } else {

            yIntersectMin = (this.boxFace.up.y - lineOrigo.y) / line.inverseDirection.y;
            yIntersectMax = (this.boxFace.down.y - lineOrigo.y) / line.inverseDirection.y;
        }

        if (xIntersectMin > yIntersectMax || yIntersectMin > xIntersectMax) 
            return false;

        if (yIntersectMin > xIntersectMin)
            xIntersectMin = yIntersectMin;

        if (yIntersectMax < xIntersectMax)
            xIntersectMax = yIntersectMax;

        if (line.inverseDirection.z >= 0) {

            zIntersectMin = (this.boxFace.back.z - lineOrigo.z) / line.inverseDirection.z;
            zIntersectMax = (this.boxFace.forward.z - lineOrigo.z) / line.inverseDirection.z;
        } else {

            zIntersectMin = (this.boxFace.forward.z - lineOrigo.z) / line.inverseDirection.z;
            zIntersectMax = (this.boxFace.back.z - lineOrigo.z) / line.inverseDirection.z;
        }

        if (xIntersectMin > zIntersectMax || zIntersectMin > xIntersectMax) 
            return false;

        if (zIntersectMin > xIntersectMin)
            xIntersectMin = zIntersectMin;

        if (zIntersectMax < xIntersectMax)
            xIntersectMax = zIntersectMax;

        float t = xIntersectMin;

        if (t < 0) {
            t = xIntersectMax;
            if (t < 0) return false;
        } 

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