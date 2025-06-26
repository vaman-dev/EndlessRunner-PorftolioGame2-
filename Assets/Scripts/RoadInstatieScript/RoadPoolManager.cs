using UnityEngine;
using System.Collections.Generic;

public class RoadPoolManager : MonoBehaviour
{
    public static RoadPoolManager Instance;

    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private int poolSize = 50;

    private List<GameObject> availableRoads = new List<GameObject>();
    private List<GameObject> activeRoads = new List<GameObject>();

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Create the object pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject road = Instantiate(roadPrefab);
            road.SetActive(false);
            availableRoads.Add(road);
        }
    }

    /// <summary>
    /// Gets a road from the pool and activates it at the specified position.
    /// </summary>
    public GameObject GetRoad(Vector3 position)
    {
        if (availableRoads.Count == 0)
        {
            Debug.LogWarning("🚨 RoadPoolManager: No available road segments left!");
            return null;
        }

        GameObject road = availableRoads[0];
        availableRoads.RemoveAt(0);

        road.transform.position = position;
        road.transform.rotation = Quaternion.identity;
        road.SetActive(true);

        activeRoads.Add(road);

        return road;
    }

    /// <summary>
    /// Releases a road segment back into the pool.
    /// </summary>
    public void ReleaseRoad(GameObject road)
    {
        if (road == null)
            return;

        if (activeRoads.Contains(road))
        {
            activeRoads.Remove(road);
            road.SetActive(false);
            availableRoads.Add(road);
        }
        else
        {
            Debug.LogWarning("🚨 RoadPoolManager: Tried to release a road that wasn't tracked as active.");
        }
    }

    /// <summary>
    /// Call this to release all roads (optional utility).
    /// </summary>
    public void ResetAllRoads()
    {
        foreach (var road in activeRoads)
        {
            road.SetActive(false);
            availableRoads.Add(road);
        }

        activeRoads.Clear();
    }
}
