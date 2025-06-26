using UnityEngine;
using System.Collections.Generic;

public class RoadTrigger : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int minHurdles = 1;
    [SerializeField] private int maxHurdles = 3;
    [SerializeField] private Transform spawnAreaOrigin;
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(4, 0, 8);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject roadSegment = RoadPoolManager.Instance.GetRoad(spawnPoint.position);
            roadSegment.transform.rotation = Quaternion.identity;

            SpawnRandomHurdles();
        }
    }

    private void SpawnRandomHurdles()
    {
        if (spawnAreaOrigin == null)
        {
            Debug.LogWarning("Spawn Area Origin is not assigned.");
            return;
        }

        int count = Random.Range(minHurdles, maxHurdles + 1);

        for (int i = 0; i < count; i++)
        {
            Vector3 offset = new Vector3(
                Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
                0f,
                Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f)
            );

            Vector3 spawnPos = spawnAreaOrigin.position + offset;
            GameObject hurdle = RandomObjectPool.Instance.GetObject(spawnPos);
            hurdle.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (spawnAreaOrigin == null)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(spawnAreaOrigin.position, spawnAreaSize);
    }
}
