using UnityEngine;
using System.Collections.Generic;

public class RoadPoolManager : MonoBehaviour
{
    // for the current instance of the RoadPoolManager 
    public static RoadPoolManager Instance;

    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private int poolSize = 5;

    private List<GameObject> roadPool = new List<GameObject>();
    private int nextIndex = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Create the pool
        for (int i = 0; i < poolSize; i++)
        {
            // Instantiate a road segment and add it to the pool 
            // Initially, the road segment is inactive 
            // Add into the list 
            GameObject road = Instantiate(roadPrefab);
            road.SetActive(false);
            roadPool.Add(road);
        }
    }

    public GameObject GetRoad(Vector3 position)
    {
        GameObject road = roadPool[nextIndex];

        road.transform.position = position;
        road.SetActive(true);

        // Move to next index
        nextIndex = (nextIndex + 1) % roadPool.Count;

        return road;
    }
}
