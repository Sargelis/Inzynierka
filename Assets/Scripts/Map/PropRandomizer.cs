using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PropRandomizer : MonoBehaviour
{
    [SerializeField] private List<GameObject> propSpawnPoint;
    [SerializeField] private List<GameObject> propPrefabs;

    void Start()
    {
        SpawnProps();
    }

    void SpawnProps()
    {
        foreach (GameObject sp in propSpawnPoint) 
        {
            int rand = Random.Range(0, propPrefabs.Count);
            GameObject prop = Instantiate(propPrefabs[rand], sp.transform.position, Quaternion.identity);
            prop.transform.parent = sp.transform;
        }
    }
}