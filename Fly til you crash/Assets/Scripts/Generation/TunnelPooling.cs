using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelPooling : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabObjects;
    [SerializeField] private int numberOfDuplicates;
    private GameObject[] pooledGameObjects;
    private void Awake()
    {
        pooledGameObjects = new GameObject[prefabObjects.Length * numberOfDuplicates];
        int index = 0;
        for(int i = 0; i < prefabObjects.Length; i++)
        {
            for (int j = 0; j < numberOfDuplicates; j++)
            {
                pooledGameObjects[index] = Instantiate(prefabObjects[i]);
                pooledGameObjects[index].SetActive(false);
                index++;
            }
        }
    }
    public GameObject GetNextObject(GameObject[] activeObjects)
    {
        List<GameObject> unusedObjects = new List<GameObject>(pooledGameObjects);
        for (int i = 0; i < activeObjects.Length; i++)
        {
            unusedObjects.Remove(activeObjects[i]);
        }
        int rand = Random.Range(0, unusedObjects.Count);
        return unusedObjects[rand];
    }
}