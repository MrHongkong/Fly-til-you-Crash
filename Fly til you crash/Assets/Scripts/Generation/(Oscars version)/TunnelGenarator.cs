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
            
            tunnelPieces[i] = Instantiate(tunnelPrefabs[Random.Range(0, tunnelPrefabs.Length)], tunnelPieces[i - 1].GetComponent<TunnelPieces>().endPoint.position, tunnelPieces[i - 1].GetComponent<TunnelPieces>().endPoint.rotation);
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
            tunnelPieces[arrayIndex] = Instantiate(tunnelPrefabs[Randomizer()], tunnelPieces[numberOfTunnelObjects - 1].GetComponent<TunnelPieces>().endPoint.position, tunnelPieces[numberOfTunnelObjects - 1].GetComponent<TunnelPieces>().endPoint.rotation);
        } else {
            tunnelPieces[arrayIndex] = Instantiate(tunnelPrefabs[Randomizer()], previousObject.endPoint.position, previousObject.endPoint.rotation);
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
        tunnelVectors[i] = currentObject.startPoint.position + (directionVector.magnitude * directionVector);
    }


    // Generates a imagenary boxgrid...?
    void GenerateBoxGrid(int index) {
        float numberOfXGrid = 1;
        float numberOfYGrid = 1;
        float numberOfZGrid = 1;

        float xDistance = Mathf.Abs(currentObject.endPoint.position.x - currentObject.startPoint.position.x);
        if (xDistance > 1) {
            numberOfXGrid = (xDistance) / BoxGrid.gridSize;
        }

        float yDistance = Mathf.Abs(currentObject.endPoint.position.y - currentObject.startPoint.position.y);
        if (yDistance > 1) {
            numberOfYGrid = (yDistance) / BoxGrid.gridSize;
        }

        float zDistance = Mathf.Abs(currentObject.endPoint.position.z - currentObject.startPoint.position.z);
        if (zDistance > 1) {
            numberOfZGrid = (zDistance) / BoxGrid.gridSize;
        }

        int currentTempGridArraySize = (int)(numberOfXGrid * numberOfYGrid * numberOfZGrid);
        BoxGrid[] tempGridArray = new BoxGrid[currentTempGridArraySize];

        gridArraySizeForRemovingBoxGridsFromList[index] = currentTempGridArraySize;

        int l = 0;

        for (int i = 0; i < numberOfXGrid; i++) {
            for(int j = 0; j < numberOfYGrid; j++) {
                for(int k = 0; k < numberOfZGrid; k++) {

                    tempGridArray[l] = new BoxGrid(new Vector3((currentObject.startPoint.position.x + BoxGrid.gridSize / 2) + i * BoxGrid.gridSize,
                                                        (currentObject.startPoint.position.y + BoxGrid.gridSize / 2) + j * BoxGrid.gridSize,
                                                        (currentObject.startPoint.position.z + BoxGrid.gridSize / 2) + k * BoxGrid.gridSize));

                    l++;
                }
            }
        }

        index = (index == numberOfTunnelObjects - 1) ? -1 : index;
        int sizeOfFirstInListTempGridArraySize = gridArraySizeForRemovingBoxGridsFromList[index + 1];

        boxGrid.RemoveRange(0, sizeOfFirstInListTempGridArraySize);

        boxGrid.AddRange(tempGridArray);

        Debug.Log(boxGrid.Count);
        
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
