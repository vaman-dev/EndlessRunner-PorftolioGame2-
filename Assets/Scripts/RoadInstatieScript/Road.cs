using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private GameObject roadSegmentPrefab;
    [SerializeField] private Transform spawnPoint;

    public void SpawnNextSegment()
    {
        Instantiate(roadSegmentPrefab, spawnPoint.position, Quaternion.identity);
    }
}
