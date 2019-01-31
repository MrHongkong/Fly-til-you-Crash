using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Script creator:   Oscar Oders - Last Updated: 2019-01-31
/// Adjustments:
/// 
/// Known problems:  
/// 
/// </summary>

//Class BoxGrid: to use as a tracking of where there already has been generated objects.
public class BoxGrid {

    internal const float gridSize = 30f;
    private float radius = Mathf.Sqrt(Mathf.Pow(gridSize/2, 2));
    internal Vector3 cellCenter;
    internal BoxFace boxFace;

    internal BoxGrid(Vector3 _cellCenter) {

        cellCenter = _cellCenter;
        boxFace = new BoxFace(cellCenter, gridSize);
    }

    internal bool Intersects(Line line) {

        Vector3 B1 = FindSmallest();
        Vector3 B2 = FindLargest();
        Vector3 L1 = line.origo;
        Vector3 L2 = line.origo + line.direction * 500;
        Vector3 Hit = new Vector3(0, 0, 0);

        if (CheckLineBox(B1, B2, L1, L2, ref Hit)) {

            return true;
        }

        return false;
    }

    private Vector3 FindSmallest() {

        float x = 0, y = 0, z = 0;

        float[] fX = {boxFace.up.x, boxFace.down.x, boxFace.left.x, boxFace.right.x, boxFace.forward.x, boxFace.back.x};
        float[] fY = {boxFace.up.y, boxFace.down.y, boxFace.left.y, boxFace.right.y, boxFace.forward.y, boxFace.back.y};
        float[] fZ = {boxFace.up.z, boxFace.down.z, boxFace.left.z, boxFace.right.z, boxFace.forward.z, boxFace.back.z};

        for (int i = 0; i < fX.Length; i++) {
            x = (x < fX[i]) ? x : fX[i];
            y = (y < fY[i]) ? y : fY[i];
            z = (z < fZ[i]) ? z : fZ[i];
        }

        return new Vector3(x, y, z);
    }

    private Vector3 FindLargest() {

        float x = 0, y = 0, z = 0;

        float[] fX = { boxFace.up.x, boxFace.down.x, boxFace.left.x, boxFace.right.x, boxFace.forward.x, boxFace.back.x };
        float[] fY = { boxFace.up.y, boxFace.down.y, boxFace.left.y, boxFace.right.y, boxFace.forward.y, boxFace.back.y };
        float[] fZ = { boxFace.up.z, boxFace.down.z, boxFace.left.z, boxFace.right.z, boxFace.forward.z, boxFace.back.z };

        for (int i = 0; i < fX.Length; i++) {
            x = (x > fX[i]) ? x : fX[i];
            y = (y > fY[i]) ? y : fY[i];
            z = (z > fZ[i]) ? z : fZ[i];
        }

        return new Vector3(x, y, z);
    }

    private bool CheckLineBox(Vector3 B1, Vector3 B2, Vector3 L1, Vector3 L2, ref Vector3 Hit) {

        if (L2.x < B1.x && L1.x < B1.x)
            return false;

        if (L2.x > B2.x && L1.x > B2.x)
            return false;

        if (L2.y < B1.y && L1.y < B1.y)
            return false;

        if (L2.y > B2.y && L1.y > B2.y)
            return false;

        if (L2.z < B1.z && L1.z < B1.z)
            return false;

        if (L2.z > B2.z && L1.z > B2.z)
            return false;

        if (L1.x > B1.x && L1.x < B2.x && L1.y > B1.y && L1.y < B2.y && L1.z > B1.z && L1.z < B2.z) {

            Hit = L1;
            return true;
        }

        if ( (GetIntersection(L1.x - B1.x, L2.x - B1.x, L1, L2, ref Hit) && InBox(Hit, B1, B2, 1))
          || (GetIntersection(L1.y - B1.y, L2.y - B1.y, L1, L2, ref Hit) && InBox(Hit, B1, B2, 2))
          || (GetIntersection(L1.z - B1.z, L2.z - B1.z, L1, L2, ref Hit) && InBox(Hit, B1, B2, 3))
          || (GetIntersection(L1.x - B2.x, L2.x - B2.x, L1, L2, ref Hit) && InBox(Hit, B1, B2, 1))
          || (GetIntersection(L1.y - B2.y, L2.y - B2.y, L1, L2, ref Hit) && InBox(Hit, B1, B2, 2))
          || (GetIntersection(L1.z - B2.z, L2.z - B2.z, L1, L2, ref Hit) && InBox(Hit, B1, B2, 3)))
            return true;

        return false;
    }

    private bool GetIntersection(float fDst1, float fDst2, Vector3 P1, Vector3 P2, ref Vector3 Hit) {

        if ((fDst1 * fDst2) >= 0.0f)
            return false;

        if (fDst1 == fDst2)
            return false;

        Hit = P1 + (P2 - P1) * (-fDst1 / (fDst2 - fDst1));
        return true;
    }

    private bool InBox(Vector3 Hit, Vector3 B1, Vector3 B2, int Axis) {

        if (Axis == 1 && Hit.z > B1.z && Hit.z < B2.z && Hit.y > B1.y && Hit.y < B2.y)
            return true;

        if (Axis == 2 && Hit.z > B1.z && Hit.z < B2.z && Hit.x > B1.x && Hit.x < B2.x)
            return true;

        if (Axis == 3 && Hit.x > B1.x && Hit.x < B2.x && Hit.y > B1.y && Hit.y < B2.y)
            return true;

        return false;
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