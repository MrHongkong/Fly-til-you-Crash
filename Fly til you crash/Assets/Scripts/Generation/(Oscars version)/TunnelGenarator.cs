using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Script creator:   Oscar Oders - Last Updated: 2019-01-29
/// Adjustments:      Sebastian Nilsson 2019-01-24
///
/// Known problems:   
///                   
/// </summary>

[RequireComponent(typeof(BoxGridGenerator))]
public class TunnelGenarator : MonoBehaviour {

    [Header("---Start tunnel piece---")] 
    [SerializeField] private GameObject startTunnelPrefab;
    [Space]

    [SerializeField] private GameObject[] tunnelPrefabs;

    private BoxGridGenerator boxGridGenerator;
    private GameObject[] tunnelPieces;
    private List<BoxGrid> boxGrid;
    private TunnelPieces currentObject, previousObject;
    private Vector3 startOrigin = new Vector3(0, 0, 0);
    private int numberOfTunnelObjects = 10;
    private int arrayIndex, previousRandomNumber;

    private void Awake() {

        boxGridGenerator = GetComponent<BoxGridGenerator>();
        tunnelPieces = new GameObject[numberOfTunnelObjects];
        boxGrid = new List<BoxGrid>();
    }

    private void Start() {

        tunnelPieces[arrayIndex] = Instantiate(startTunnelPrefab, startOrigin, startTunnelPrefab.transform.rotation);
        currentObject = tunnelPieces[arrayIndex].GetComponent<TunnelPieces>();
        boxGridGenerator.SetUpBoxGrid(currentObject, ref boxGrid);

        SpawnOnTrigger.generator = GetComponent<TunnelGenarator>();
        for (int i = 1; i < numberOfTunnelObjects; i++) {

            GenerateNewTunnelPiece(i, false);
        }
    }

    //private void Update() {

    //    if (Input.GetKey(KeyCode.O) || Input.GetKeyDown(KeyCode.N)) {

    //        GenerateNewTunnelPiece(arrayIndex, true);
    //    }
    //}

    //Increments the arrayIndex variable by 1. If last object in array arrayIndex sets to 0;
    private void IncrementArrayIndex(int arrayLength) {

        arrayIndex = (arrayIndex < arrayLength - 1) ? arrayIndex + 1 : 0;
    }

    // Generates a new tunnel piece at the end of the current tunnel.
    internal void GenerateNewTunnelPiece(int arrayIndex, bool isStartTunnelSetup) {

        previousObject = currentObject;

        TunnelPieceInstatiation(arrayIndex);

        bool wayIsClear = false;
        do {

            for (int i = 0; i < boxGrid.Count; i++) {

                if (boxGrid[i].BoxGridIntersection(currentObject, new Line(TunnelsDirection(currentObject)))) {

                    TunnelPieceInstatiation(arrayIndex);
                    break;
                }

                wayIsClear = (i == boxGrid.Count - 1) ? true : false;
            }

            wayIsClear = (boxGrid.Count == 0) ? true : wayIsClear;
        } while (!wayIsClear);

        boxGridGenerator.SetUpBoxGrid(currentObject, ref boxGrid);

        if (isStartTunnelSetup) {
            IncrementArrayIndex(numberOfTunnelObjects);
        }
    }

    //Instantiate new tunnelPices
    private void TunnelPieceInstatiation(int arrayIndex) {

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

    internal int GetNumberOfTunnelObjects() {
        return numberOfTunnelObjects;
    }

    internal int GetArrayIndex() {
        return arrayIndex;
    }

    ////Displays boxgrid as red cubes in sceneview
    //private void OnDrawGizmos() {

    //    if (boxGrid != null) {

    //        foreach (BoxGrid cell in boxGrid) {

    //            Gizmos.color = new Color(1, 0, 0, 0.5f);
    //            Gizmos.DrawCube(cell.cellCenter, new Vector3(BoxGrid.gridSize, BoxGrid.gridSize, BoxGrid.gridSize));
    //        }
    //    }
    //}
}

public class Line {

    internal Vector3 direction, inverseDirection;

    public Line(Vector3 _direction) {

        direction = _direction;
        inverseDirection = new Vector3(1 / _direction.x, 1 / _direction.y, 1 / _direction.z);
    }
}