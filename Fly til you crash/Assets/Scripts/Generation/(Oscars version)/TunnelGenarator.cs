using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Script creator:   Oscar Oders - Last Updated: 2019-01-25
/// Adjustments:      Sebastian Nilsson 2019-01-24
///
/// Known problems:   There is a problem with the way that the boxGrid centerPoints are calculated, 
///                   in that: if a curved object is spawned the rotation of it might make it go outside the box grid.
///                   a soulution may be to define where the outer most point of the curve is 
///                   and build between startPoint and endPoint via the outer most point of the curve.
///                   BoxGrid problem causes Randomizer() to return the curved object number when a curved object is already the previous object.
///                   
/// </summary>

public class TunnelGenarator : MonoBehaviour {

    [SerializeField] private GameObject startTunnelPrefab;
    [SerializeField] private GameObject[] tunnelPrefabs;
    private GameObject[] tunnelPieces;
    private GameObject[] debugPieces;
    private List<BoxGrid> boxGrid;
    private TunnelPieces currentObject, previousObject;
    private Vector3 startOrigin = new Vector3(0, 0, 0);
    private int[] gridArraySizeForRemovingBoxGridsFromList;
    private int numberOfTunnelObjects = 20;
    private int arrayIndex, previousRandomNumber;


    private void Start() {

        tunnelPieces = new GameObject[numberOfTunnelObjects];
        debugPieces = new GameObject[numberOfTunnelObjects];
        gridArraySizeForRemovingBoxGridsFromList = new int[numberOfTunnelObjects];
        boxGrid = new List<BoxGrid>();

        tunnelPieces[arrayIndex] = Instantiate(startTunnelPrefab, startOrigin, startTunnelPrefab.transform.rotation);

        SpawnOnTrigger.generator = GetComponent<TunnelGenarator>();
        for (int i = 1; i < numberOfTunnelObjects; i++) {

            tunnelPieces[i] = Instantiate(tunnelPrefabs[Random.Range(0, tunnelPrefabs.Length)], tunnelPieces[i - 1].GetComponent<TunnelPieces>().endPoint.position, RotationOfTunnel(tunnelPieces[i - 1].GetComponent<TunnelPieces>()));
            currentObject = tunnelPieces[i].GetComponent<TunnelPieces>();
            GenerateBoxGrid(i);
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

        GenerateBoxGrid(arrayIndex);
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

    // Generates a boxgrid 
    private void GenerateBoxGrid(int index) {

        int numberOfXGrid = (int)NumberOfGrids(currentObject, 'x');
        int numberOfYGrid = (int)NumberOfGrids(currentObject, 'y');
        int numberOfZGrid = (int)NumberOfGrids(currentObject, 'z');

        int currentTempGridArraySize = (numberOfXGrid * numberOfYGrid * numberOfZGrid);
        BoxGrid[] tempGridArray = new BoxGrid[currentTempGridArraySize];

        gridArraySizeForRemovingBoxGridsFromList[index] = currentTempGridArraySize;

        int l = 0;

        for (int i = 0; i < numberOfXGrid; i++) {

            for(int j = 0; j < numberOfYGrid; j++) {

                for(int k = 0; k < numberOfZGrid; k++) {
 
                    tempGridArray[l] = new BoxGrid(new Vector3((currentObject.startPoint.position.x) + i * BoxGrid.gridSize * Mathf.Sign(currentObject.endPoint.position.x - currentObject.startPoint.position.x),
                                                               (currentObject.startPoint.position.y) + j * BoxGrid.gridSize * Mathf.Sign(currentObject.endPoint.position.y - currentObject.startPoint.position.y),
                                                               (currentObject.startPoint.position.z) + k * BoxGrid.gridSize * Mathf.Sign(currentObject.endPoint.position.z - currentObject.startPoint.position.z)));

                    l++;
                }
            }
        }

        index = (index == numberOfTunnelObjects - 1) ? -1 : index;

        int sizeOfFirstInListTempGridArraySize = gridArraySizeForRemovingBoxGridsFromList[index + 1];
        boxGrid.RemoveRange(0, sizeOfFirstInListTempGridArraySize);

        boxGrid.AddRange(tempGridArray);
    }

    //Caluculates the number of boxes eatch object has, by axis.
    private float NumberOfGrids(TunnelPieces currentObject, char axis) {

        float distance = 1; 
        if (axis == 'x')
            distance = Mathf.Abs(currentObject.endPoint.position.x - currentObject.startPoint.position.x);

        if (axis == 'y')
            distance = Mathf.Abs(currentObject.endPoint.position.y - currentObject.startPoint.position.y);

        if (axis == 'z')
            distance = Mathf.Abs(currentObject.endPoint.position.z - currentObject.startPoint.position.z);

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