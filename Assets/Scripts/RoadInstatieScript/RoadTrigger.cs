using UnityEngine;

public class RoadTrigger : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int minHurdles = 1;
    [SerializeField] private int maxHurdles = 3;
    [SerializeField] private Transform spawnAreaOrigin;
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(4, 0, 8);
    [SerializeField] private float releaseDistance = 180f;

    private GameObject lastSpawnedRoad;
    private Transform playerTransform;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            playerTransform = playerObj.transform;
        else
            Debug.LogError("RoadTrigger: Player with tag 'Player' not found.");
    }

    private void Update()
    {
        if (lastSpawnedRoad != null && playerTransform != null)
        {
            float distance = Vector3.Distance(playerTransform.position, lastSpawnedRoad.transform.position);

            if (distance > releaseDistance)
            {
                RoadPoolManager.Instance.ReleaseRoad(lastSpawnedRoad);
                lastSpawnedRoad = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (spawnPoint == null)
        {
            Debug.LogError("RoadTrigger: spawnPoint is not assigned.");
            return;
        }

        if (RoadPoolManager.Instance == null)
        {
            Debug.LogError("RoadTrigger: RoadPoolManager.Instance is null.");
            return;
        }

        GameObject roadSegment = RoadPoolManager.Instance.GetRoad(spawnPoint.position);
        if (roadSegment == null)
        {
            Debug.LogError("RoadTrigger: Failed to get road segment from pool.");
            return;
        }

        lastSpawnedRoad = roadSegment;
        roadSegment.transform.rotation = Quaternion.identity;

        SpawnRandomHurdles();
    }

    private void SpawnRandomHurdles()
    {
        if (spawnAreaOrigin == null)
        {
            Debug.LogWarning("RoadTrigger: Spawn Area Origin is not assigned.");
            return;
        }

        if (RandomObjectPool.Instance == null)
        {
            Debug.LogError("RoadTrigger: RandomObjectPool.Instance is null.");
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

            if (hurdle == null)
            {
                Debug.LogError("RoadTrigger: Failed to get hurdle from pool.");
                continue;
            }

            hurdle.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (spawnAreaOrigin == null) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(spawnAreaOrigin.position, spawnAreaSize);
    }
}
