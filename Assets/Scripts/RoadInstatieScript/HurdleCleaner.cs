using UnityEngine;

public class HurdleCleaner : MonoBehaviour
{
    [SerializeField] private float destroyZOffset = -15f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (transform.position.z < player.position.z + destroyZOffset)
        {
            RandomObjectPool.Instance.ReturnObject(gameObject);
        }
    }
}
