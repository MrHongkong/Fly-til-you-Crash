using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelPooling : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabObjects;
    [SerializeField] private int numberOfDuplicates;
    private GameObject[] pooledObjects;
    private void Awake()
    {
        pooledObjects = new GameObject[prefabObjects.Length * numberOfDuplicates];
        int index = 0;
        for(int i = 0; i < prefabObjects.Length; i++)
        {
            for (int j = 0; j < numberOfDuplicates; j++)
            {
                pooledObjects[index] = Instantiate(prefabObjects[i]);
                pooledObjects[index].SetActive(false);
                index++;
            }
        }
    }
    public GameObject GetNextObject(GameObject[] activeObjects)
    {
        List<GameObject> unusedObjects;

        unusedObjects = new List<GameObject>(pooledObjects);

        for (int i = 0; i < activeObjects.Length; i++)
        {
            unusedObjects.Remove(activeObjects[i]);
        }
        int rand = Random.Range(0, unusedObjects.Count);
        return unusedObjects[rand];
    }
}