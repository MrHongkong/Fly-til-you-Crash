using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script creators: Oscar Oders

public class TunnelGenarator : MonoBehaviour
{
    [SerializeField] private GameObject[] tunnelPrefabs;
    GameObject[] tunnelPieces;
    TunnelPieces currentObject, previousObject;

    Vector3 startOrigin = new Vector3(0, 0, 0);
    Vector3[] tunnelVectors;
    int numberOfTunnelObjects = 10;
    int arrayIndex;


    private void Start() {
        tunnelPieces = new GameObject[numberOfTunnelObjects];
        tunnelVectors = new Vector3[numberOfTunnelObjects];

        tunnelPieces[0] = Instantiate(tunnelPrefabs[0], startOrigin, tunnelPrefabs[0].transform.rotation);

        for (int i = 1; i < numberOfTunnelObjects; i++) {
            
            tunnelPieces[i] = Instantiate(tunnelPrefabs[Random.Range(0, tunnelPrefabs.Length)], tunnelPieces[i - 1].GetComponent<TunnelPieces>().endPoint.position, tunnelPieces[i - 1].GetComponent<TunnelPieces>().endPoint.rotation);
            currentObject = tunnelPieces[i].GetComponent<TunnelPieces>();

            SaveTunnelVectors(i);
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
            tunnelPieces[arrayIndex] = Instantiate(tunnelPrefabs[Random.Range(0, tunnelPrefabs.Length)], tunnelPieces[numberOfTunnelObjects - 1].GetComponent<TunnelPieces>().endPoint.position, tunnelPieces[numberOfTunnelObjects - 1].GetComponent<TunnelPieces>().endPoint.rotation);
        } else {
            tunnelPieces[arrayIndex] = Instantiate(tunnelPrefabs[Random.Range(0, tunnelPrefabs.Length)], previousObject.endPoint.position, previousObject.endPoint.rotation);
            currentObject = tunnelPieces[arrayIndex].GetComponent<TunnelPieces>();
        }

        SaveTunnelVectors(arrayIndex);
        arrayIndex = (arrayIndex < numberOfTunnelObjects - 1) ? arrayIndex + 1 : 0;
    }

    //Calculates the vector between currentObjects start and end point and saves it in the tunnelVectors array
    void SaveTunnelVectors(int i) {
        Vector3 directionVector = currentObject.endPoint.position - currentObject.startPoint.position;
        tunnelVectors[i] = currentObject.startPoint.position + (directionVector.magnitude * directionVector);
        Debug.Log("vectors: " + tunnelVectors[i]);
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
