using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelPooling : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabObjectsEasy, prefabObjectsMedium, prefabObjectsHard;
    [SerializeField] private int numberOfDuplicates;
    private GameObject[] pooledObjectsEasy, pooledObjectsMedium, pooledObjectsHard;
    [SerializeField] private int secondsToMedium, secondsToHard;
    private void Awake()
    {
        pooledObjectsEasy = new GameObject[prefabObjectsEasy.Length * numberOfDuplicates];
        pooledObjectsMedium = new GameObject[(prefabObjectsEasy.Length + prefabObjectsMedium.Length) * numberOfDuplicates];
        pooledObjectsHard = new GameObject[(prefabObjectsEasy.Length + prefabObjectsMedium.Length + prefabObjectsHard.Length) * numberOfDuplicates];
        int index = 0;
        for(int i = 0; i < prefabObjectsEasy.Length; i++)
        {
            for (int j = 0; j < numberOfDuplicates; j++)
            {
                pooledObjectsEasy[index] = Instantiate(prefabObjectsEasy[i]);
                pooledObjectsMedium[index] = pooledObjectsEasy[index];
                pooledObjectsHard[index] = pooledObjectsEasy[index];
                pooledObjectsEasy[index].SetActive(false);
                index++;
            }
        }
        for(int i = 0; i < prefabObjectsMedium.Length; i++)
        {
            for (int j = 0; j < numberOfDuplicates; j++)
            {
                pooledObjectsMedium[index] = Instantiate(prefabObjectsMedium[i]);
                pooledObjectsHard[index] = pooledObjectsMedium[index];
                pooledObjectsMedium[index].SetActive(false);
                index++;
            }
        }
        for (int i = 0; i < prefabObjectsHard.Length; i++)
        {
            for (int j = 0; j < numberOfDuplicates; j++)
            {
                pooledObjectsHard[index] = Instantiate(prefabObjectsHard[i]);
                pooledObjectsHard[index].SetActive(false);
                index++;
            }
        }
    }
    public GameObject GetNextObject(GameObject[] activeObjects)
    {
        List<GameObject> unusedObjects;

        if (Time.timeSinceLevelLoad > secondsToMedium)
            unusedObjects = new List<GameObject>(pooledObjectsMedium);
        else if(Time.timeSinceLevelLoad > secondsToHard)
            unusedObjects = new List<GameObject>(pooledObjectsHard);
        else
            unusedObjects = new List<GameObject>(pooledObjectsEasy);

        for (int i = 0; i < activeObjects.Length; i++)
        {
            unusedObjects.Remove(activeObjects[i]);
        }
        int rand = Random.Range(0, unusedObjects.Count);
        return unusedObjects[rand];
    }
}