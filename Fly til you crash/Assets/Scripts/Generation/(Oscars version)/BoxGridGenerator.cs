using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Script creator:     Oscar Oders - Last Updated: 2019-01-30
/// Adjustments:  
/// 
/// Known problems:     
/// 
/// </summary>

[RequireComponent(typeof(TunnelGenarator))]
public class BoxGridGenerator : MonoBehaviour {

    private TunnelGenarator tunnelGenarator;
    private int[] gridArraySizeForRemovingBoxGridsFromList;
    private int generatorIndex, previousGeneratorIndex;

    private void Awake() {

        tunnelGenarator = GetComponent<TunnelGenarator>();
        gridArraySizeForRemovingBoxGridsFromList = new int[tunnelGenarator.GetNumberOfTunnelObjects() + 1];
    }

    //Checks if there are any mid points set up in the object and calles generateBoxGrid.
    internal void SetUpBoxGrid(TunnelPieces currentObject, ref List<BoxGrid> boxGrid) {

        List<Transform> midPoints = GetChildrenWithTag(currentObject.transform, "midPoint");

        if (midPoints.Count != 0) {

            GenerateBoxGrid(currentObject.startPoint, midPoints[0], ref boxGrid);

            for (int i = 0; i < midPoints.Count; i++) {
                if (i != midPoints.Count - 1) {

                    GenerateBoxGrid(midPoints[i], midPoints[i + 1], ref boxGrid);
                }
            }

            GenerateBoxGrid(midPoints[midPoints.Count - 1], currentObject.endPoint, ref boxGrid);
        } else {

            GenerateBoxGrid(currentObject.startPoint, currentObject.endPoint, ref boxGrid);
        }
    }

    //Returns a list of Transforms of the children tagged with tagName, of a parent GameObject.
    private List<Transform> GetChildrenWithTag(Transform parent, string tagName) {

        List<Transform> taggedChildren = new List<Transform>();

        for (int i = 0; i < parent.childCount; i++) {
            Transform child = parent.GetChild(i);

            if (child.parent.CompareTag(tagName)) {

                taggedChildren.Add(child.parent);
            }
        }

        return taggedChildren;
    }

    // Generates a boxgrid 
    private void GenerateBoxGrid(Transform startPoint, Transform endPoint, ref List<BoxGrid> boxGrid) {

        generatorIndex++;

        int numberOfXGrid = (int)NumberOfGrids(startPoint, endPoint, 'x');
        int numberOfYGrid = (int)NumberOfGrids(startPoint, endPoint, 'y');
        int numberOfZGrid = (int)NumberOfGrids(startPoint, endPoint, 'z');

        int currentTempGridArraySize = (numberOfXGrid * numberOfYGrid * numberOfZGrid);
        BoxGrid[] tempGridArray = new BoxGrid[currentTempGridArraySize];

        if (generatorIndex == previousGeneratorIndex) {

            gridArraySizeForRemovingBoxGridsFromList[generatorIndex] = gridArraySizeForRemovingBoxGridsFromList[generatorIndex] + currentTempGridArraySize;
        } else {

            gridArraySizeForRemovingBoxGridsFromList[generatorIndex] = currentTempGridArraySize;
        }

        int l = 0;

        for (int i = 0; i < numberOfXGrid; i++) {

            for (int j = 0; j < numberOfYGrid; j++) {

                for (int k = 0; k < numberOfZGrid; k++) {

                    tempGridArray[l] = new BoxGrid(new Vector3((startPoint.position.x) + i * BoxGrid.gridSize * Mathf.Sign(endPoint.position.x - startPoint.position.x),
                                                               (startPoint.position.y) + j * BoxGrid.gridSize * Mathf.Sign(endPoint.position.y - startPoint.position.y),
                                                               (startPoint.position.z) + k * BoxGrid.gridSize * Mathf.Sign(endPoint.position.z - startPoint.position.z)));

                    l++;
                }
            }
        }

        generatorIndex = (generatorIndex == tunnelGenarator.GetNumberOfTunnelObjects()) ? -1 : generatorIndex;

        boxGrid.RemoveRange(0, gridArraySizeForRemovingBoxGridsFromList[generatorIndex + 1]);

        previousGeneratorIndex = generatorIndex;

        boxGrid.AddRange(tempGridArray);
    }

    //Caluculates the number of boxes eatch object has, by axis.
    private float NumberOfGrids(Transform startPoint, Transform endPoint, char axis) {

        float distance = 1;
        if (axis == 'x')
            distance = Mathf.Abs(endPoint.position.x - startPoint.position.x);

        if (axis == 'y')
            distance = Mathf.Abs(endPoint.position.y - startPoint.position.y);

        if (axis == 'z')
            distance = Mathf.Abs(endPoint.position.z - startPoint.position.z);

        if (distance > BoxGrid.gridSize / 2) {

            return (distance / BoxGrid.gridSize) + 2; // +2 is because the BoxGrid is shifted .5 in each end of a tunnelPice, and because the angles to small will make some pices go outeside the box area.
        } else {

            return 1;
        }
    }
}
