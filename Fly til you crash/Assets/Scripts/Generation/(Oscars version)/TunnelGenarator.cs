using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script creators: Oscar Oders

public class TunnelGenarator : MonoBehaviour
{
    [SerializeField] private GameObject[] tunnelPrefabs;
    GameObject[] tunnelPieces;
    List<BoxGrid> boxGrid;
    TunnelPieces currentObject, previousObject;

    Vector3 startOrigin = new Vector3(0, 0, 0);
    Vector3[] tunnelVectors;
    int[] gridArraySizeForRemovingBoxGridsFromList;
    int numberOfTunnelObjects = 10;
    int arrayIndex, previousRandomNumber;


    private void Start() {
        tunnelPieces = new GameObject[numberOfTunnelObjects];
        tunnelVectors = new Vector3[numberOfTunnelObjects];
        gridArraySizeForRemovingBoxGridsFromList = new int[numberOfTunnelObjects];
        boxGrid = new List<BoxGrid>();

        tunnelPieces[0] = Instantiate(tunnelPrefabs[0], startOrigin, tunnelPrefabs[0].transform.rotation);

        for (int i = 1; i < numberOfTunnelObjects; i++) {
            
            tunnelPieces[i] = Instantiate(tunnelPrefabs[Random.Range(0, tunnelPrefabs.Length)], tunnelPieces[i - 1].GetComponent<TunnelPieces>().endPoint.position, tunnelPieces[i - 1].GetComponent<TunnelPieces>().endPoint.localRotation);
            currentObject = tunnelPieces[i].GetComponent<TunnelPieces>();

            SaveTunnelVectors(i);
            GenerateBoxGrid(i);
        }  
    }

    private void Update() {

        if (Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.S)) {
            GenerateNewTunnelPiece();
        }
    }

    // Generates a new tunnel piece at the end of the current tunnel.
    void GenerateNewTunnelPiece() {

        previousObject = currentObject;
        Destroy(tunnelPieces[arrayIndex]);

        if (arrayIndex == 0) {
            tunnelPieces[arrayIndex] = Instantiate(tunnelPrefabs[Randomizer()], tunnelPieces[numberOfTunnelObjects - 1].GetComponent<TunnelPieces>().endPoint.position, tunnelPieces[numberOfTunnelObjects - 1].GetComponent<TunnelPieces>().endPoint.localRotation);
            currentObject = tunnelPieces[arrayIndex].GetComponent<TunnelPieces>();
        } else {
            tunnelPieces[arrayIndex] = Instantiate(tunnelPrefabs[Randomizer()], previousObject.endPoint.position, previousObject.endPoint.localRotation);
            currentObject = tunnelPieces[arrayIndex].GetComponent<TunnelPieces>();
        }

        SaveTunnelVectors(arrayIndex);
        GenerateBoxGrid(arrayIndex);
        arrayIndex = (arrayIndex < numberOfTunnelObjects - 1) ? arrayIndex + 1 : 0;
    }

    //Returns a random value between 0 and the length of tunnelPrefabs. 
    //If that value is the same as the last, then it returns 0.
    int Randomizer() {
        int randomNumber = Random.Range(0, tunnelPrefabs.Length);
        randomNumber = (randomNumber == previousRandomNumber) ? 0 : randomNumber;
        previousRandomNumber = randomNumber;
        
        return randomNumber;
    }

    //Calculates the vector between currentObjects start and end point and saves it in the tunnelVectors array
    void SaveTunnelVectors(int i) {
        Vector3 directionVector = currentObject.endPoint.position - currentObject.startPoint.position;
        tunnelVectors[i] = currentObject.startPoint.position + (directionVector.magnitude * directionVector.normalized);
    }


    // Generates a boxgrid - To generate grid all position values in tunnelprefabs need to be devideble by 2. (is this possible to fix?)
    void GenerateBoxGrid(int index) {
        float numberOfXGrid = NumberOfGrids(currentObject, 'x');
        float numberOfYGrid = NumberOfGrids(currentObject, 'y');
        float numberOfZGrid = NumberOfGrids(currentObject, 'z');

        int currentTempGridArraySize = (int)(numberOfXGrid * numberOfYGrid * numberOfZGrid);
        BoxGrid[] tempGridArray = new BoxGrid[currentTempGridArraySize];
        Debug.Log("arraySize: " + currentTempGridArraySize);

        gridArraySizeForRemovingBoxGridsFromList[index] = currentTempGridArraySize;

        int l = 0;

        for (int i = 0; i < numberOfXGrid; i++) {
            for(int j = 0; j < numberOfYGrid; j++) {
                for(int k = 0; k < numberOfZGrid; k++) {
                    Debug.Log("l: " + l);
                    tempGridArray[l] = new BoxGrid(new Vector3((currentObject.startPoint.position.x + BoxAdjustment(numberOfXGrid)) + i * BoxGrid.gridSize,
                                                               (currentObject.startPoint.position.y + BoxAdjustment(numberOfYGrid)) + j * BoxGrid.gridSize,
                                                               (currentObject.startPoint.position.z + BoxAdjustment(numberOfZGrid)) + k * BoxGrid.gridSize));

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
    float NumberOfGrids(TunnelPieces currentObject, char axis) {
        float distance = 1; 
        if (axis == 'x')
            distance = Mathf.Abs(currentObject.endPoint.position.x - currentObject.startPoint.position.x);

        if (axis == 'y')
            distance = Mathf.Abs(currentObject.endPoint.position.y - currentObject.startPoint.position.y);

        if (axis == 'z')
            distance = Mathf.Abs(currentObject.endPoint.position.z - currentObject.startPoint.position.z);

        if (!(distance < BoxGrid.gridSize + 1)) {
            return (distance / BoxGrid.gridSize) + 1;
        } else {
            return 1;
        }
    }

    //Adjust the position of the box
    float BoxAdjustment(float numberOfGrids) {
        float temp = (numberOfGrids < 2) ? 0 : 0; //BoxGrid.gridSize / 2; <- to use if boxes is not allowed to be outside the tunnel ends.
        return temp;
    }

    //Displays boxgrid as red cubes in sceneview
    void OnDrawGizmos() {

        foreach (BoxGrid cell in boxGrid) {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(cell.cellCenter, new Vector3(2, 2, 2));
        }
    }
}























//Vector3 levelOrigin;
//public GameObject[] tunnelPrefabs;
//TunnelPieces[] tunnelPieces;
//int numberOfSpawnedPeices = 10;
//int arrayIndex;

//// Start is called before the first frame update
//void Start() {
//    levelOrigin = new Vector3(0, 0, 0);
//    tunnelPieces = new TunnelPieces[numberOfSpawnedPeices];
//    tunnelPieces[arrayIndex] = new TunnelPieces(tunnelPrefabs[0], levelOrigin);
//    arrayIndex++;

//    //fills the tunnelPices array with tunnelPrefab objects.
//    for (int i = arrayIndex; i < numberOfSpawnedPeices; i++) {
//        tunnelPieces[i] = new TunnelPieces(tunnelPrefabs[Random.Range(0, tunnelPrefabs.Length)], PrevoiusEnd(i));

//    }
//}

//// Update is called once per frame
//void Update() {

//}

////returns the end point of previous tunnelpiece in the tunnelPieces array as an Vector3
//Vector3 PrevoiusEnd(int i) {
//    Vector3 previousEnd = tunnelPieces[i - 1].CalculateEnd();
//    return previousEnd;
//}
