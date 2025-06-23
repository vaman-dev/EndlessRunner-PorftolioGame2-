using UnityEngine;

public class RoadTrigger : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject roadSegment = RoadPoolManager.Instance.GetRoad(spawnPoint.position);
            roadSegment.transform.rotation = Quaternion.identity;

            // Optional: disable this trigger after use to prevent re-triggering
           
        }
    }
}
