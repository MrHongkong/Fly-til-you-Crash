using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Script creator:   Oscar Oders - Last Updated: 2019-01-28
/// Adjustments:      Sebastian Nilsson 2019-01-24
///
/// Known problems:   BoxGrid problem causes Randomizer() to return the curved object number when a curved object is already the previous object?? - does this still apply?
///                   
///                   break out the code thats generates the boxgrid into a seperate script. and work on abstraction levels!
///                   
/// </summary>

public class TunnelGenarator : MonoBehaviour {

    [SerializeField] private GameObject startTunnelPrefab;
    [SerializeField] private GameObject[] tunnelPrefabs;
    private GameObject[] tunnelPieces;
    private List<BoxGrid> boxGrid;
    private TunnelPieces currentObject, previousObject;
    private Vector3 startOrigin = new Vector3(0, 0, 0);
    private int[] gridArraySizeForRemovingBoxGridsFromList;
    private int numberOfTunnelObjects = 10;
    private int arrayIndex, previousRandomNumber, previousGeneratorIndex;


    private void Start() {

        tunnelPieces = new GameObject[numberOfTunnelObjects];
        gridArraySizeForRemovingBoxGridsFromList = new int[numberOfTunnelObjects];
        boxGrid = new List<BoxGrid>();

        tunnelPieces[arrayIndex] = Instantiate(startTunnelPrefab, startOrigin, startTunnelPrefab.transform.rotation);

        SpawnOnTrigger.generator = GetComponent<TunnelGenarator>();
        for (int i = 1; i < numberOfTunnelObjects; i++) {

            tunnelPieces[i] = Instantiate(tunnelPrefabs[Random.Range(0, tunnelPrefabs.Length)], tunnelPieces[i - 1].GetComponent<TunnelPieces>().endPoint.position, RotationOfTunnel(tunnelPieces[i - 1].GetComponent<TunnelPieces>()));
            currentObject = tunnelPieces[i].GetComponent<TunnelPieces>();
            SetUpBoxGrid(currentObject, i);
        }  
    }

    private void Update() {
        
        if (Input.GetKey(KeyCode.O) || Input.GetKeyDown(KeyCode.N)) {

            GenerateNewTunnelPiece();
        }
    }

    //Increments the arrayIndex variable by 1. If last object in array arrayIndex sets to 0;
    private void IncrementArrayIndex(int arrayLength) {

        arrayIndex = (arrayIndex < arrayLength - 1) ? arrayIndex + 1 : 0;
    }

    // Generates a new tunnel piece at the end of the current tunnel.
    internal void GenerateNewTunnelPiece() {

        previousObject = currentObject;

        TunnelPieceInstatiation();

        bool wayIsClear = false;
        do {

            for (int i = 0; i < boxGrid.Count; i++) {

                if (boxGrid[i].BoxGridIntersection(currentObject, new Line(TunnelsDirection(currentObject)))) {

                    TunnelPieceInstatiation();
                    break;
                }

                wayIsClear = (i == boxGrid.Count - 1) ? true : false;
            }
        } while (!wayIsClear);

        SetUpBoxGrid(currentObject, arrayIndex);
        IncrementArrayIndex(numberOfTunnelObjects);
    }

    //Instantiate new tunnelPices
    private void TunnelPieceInstatiation() {

        int randomNumber = Randomizer(tunnelPrefabs.Length);

        Destroy(tunnelPieces[arrayIndex]);

        previousObject = (arrayIndex != 0) ? previousObject : tunnelPieces[numberOfTunnelObjects - 1].GetComponent<TunnelPieces>();
        tunnelPieces[arrayIndex] = Instantiate(tunnelPrefabs[randomNumber], previousObject.endPoint.position, RotationOfTunnel(previousObject));
        currentObject = tunnelPieces[arrayIndex].GetComponent<TunnelPieces>();
    }

    //Returns a random value between 0 and the passed in length. 
    //If that value is the same as the last, then it returns 0.
    private int Randomizer(int length) {

        int randomNumber = Random.Range(0, length);
        randomNumber = (randomNumber == previousRandomNumber) ? 0 : randomNumber;
        randomNumber = (previousRandomNumber == 0) ? Random.Range(1, length) : randomNumber;
        previousRandomNumber = randomNumber;
        
        return randomNumber;
    }

    //returns the rotation of tunnelPiece
    private Quaternion RotationOfTunnel(TunnelPieces rotateAroundZOfTunnelPiece) {

        Vector3 tempRotation = rotateAroundZOfTunnelPiece.endPoint.eulerAngles;
        tempRotation.z = Random.Range(0, 360);
        return Quaternion.Euler(tempRotation);
    }

    //returns the vector in witch the endpoint is facing.
    private Vector3 TunnelsDirection(TunnelPieces tunnelObject) {

        Vector3 directionVector = tunnelObject.endPoint.position - tunnelObject.midPoint.position;
        return directionVector.normalized;
    }

    //Checks if there are any mid points set up in the object and calles generateBoxGrid.
    private void SetUpBoxGrid(TunnelPieces currentObject, int index) { 

        List<Transform> midPoints = GetChildrenWithTag(currentObject.transform, "midPoint");

        if (midPoints.Count != 0) {
            
            GenerateBoxGrid(currentObject.startPoint, midPoints[0], index);

            for (int i = 0; i < midPoints.Count; i++) {
                if(i != midPoints.Count - 1) {

                    GenerateBoxGrid(midPoints[i], midPoints[i + 1], index);
                } 
            }

            GenerateBoxGrid(midPoints[midPoints.Count - 1], currentObject.endPoint, index);
        } else {
            
            GenerateBoxGrid(currentObject.startPoint, currentObject.endPoint, index);
        }
    }

    //Returns a list of Transforms of the children tagged with tagName, of a parent GameObject.
    private List<Transform> GetChildrenWithTag(Transform parent, string tagName) {

        List<Transform> taggedChildren = new List<Transform>();

        foreach (Transform child in parent) {

            if (child.parent.CompareTag(tagName)) {

                taggedChildren.Add(child.parent);
            }
        }

        return taggedChildren;
    }

    // Generates a boxgrid 
    private void GenerateBoxGrid(Transform startPoint, Transform endPoint, int index) {

        int generatorIndex = index;

        int numberOfXGrid = (int)NumberOfGrids(startPoint, endPoint, 'x');
        int numberOfYGrid = (int)NumberOfGrids(startPoint, endPoint, 'y');
        int numberOfZGrid = (int)NumberOfGrids(startPoint, endPoint, 'z');

        int currentTempGridArraySize = (numberOfXGrid * numberOfYGrid * numberOfZGrid);
        BoxGrid[] tempGridArray = new BoxGrid[currentTempGridArraySize];

        if (generatorIndex == previousGeneratorIndex) {
            Debug.Log("same");
            gridArraySizeForRemovingBoxGridsFromList[generatorIndex] = gridArraySizeForRemovingBoxGridsFromList[generatorIndex] + currentTempGridArraySize;
        } else {
            Debug.Log("not same");
            gridArraySizeForRemovingBoxGridsFromList[generatorIndex] = currentTempGridArraySize;
        }

        int l = 0;

        for (int i = 0; i < numberOfXGrid; i++) {

            for(int j = 0; j < numberOfYGrid; j++) {

                for(int k = 0; k < numberOfZGrid; k++) {
 
                    tempGridArray[l] = new BoxGrid(new Vector3((startPoint.position.x) + i * BoxGrid.gridSize * Mathf.Sign(endPoint.position.x - startPoint.position.x),
                                                               (startPoint.position.y) + j * BoxGrid.gridSize * Mathf.Sign(endPoint.position.y - startPoint.position.y),
                                                               (startPoint.position.z) + k * BoxGrid.gridSize * Mathf.Sign(endPoint.position.z - startPoint.position.z)));

                    l++;
                }
            }
        }

        generatorIndex = (generatorIndex == numberOfTunnelObjects - 1) ? -1 : generatorIndex;

        int sizeOfFirstInListTempGridArraySize = gridArraySizeForRemovingBoxGridsFromList[generatorIndex + 1];
        boxGrid.RemoveRange(0, sizeOfFirstInListTempGridArraySize);

        boxGrid.AddRange(tempGridArray);

        previousGeneratorIndex = generatorIndex;
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

    //Displays boxgrid as red cubes in sceneview
    private void OnDrawGizmos() {

        if(boxGrid != null) {

            foreach (BoxGrid cell in boxGrid) {

                Gizmos.color = new Color(1, 0, 0, 0.5f);
                Gizmos.DrawCube(cell.cellCenter, new Vector3(BoxGrid.gridSize, BoxGrid.gridSize, BoxGrid.gridSize));
            }
        }   
    }
}

public class Line {

    internal Vector3 direction, inverseDirection;

    public Line(Vector3 _direction) {

        direction = _direction;
        inverseDirection = new Vector3(1 / _direction.x, 1 / _direction.y, 1 / _direction.z);
    }
}