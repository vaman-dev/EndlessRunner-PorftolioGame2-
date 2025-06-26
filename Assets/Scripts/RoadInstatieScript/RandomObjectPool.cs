using UnityEngine;
using System.Collections.Generic;

public class RandomObjectPool : MonoBehaviour
{
    public static RandomObjectPool Instance { get; private set; }

    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 30; // make this large enough to cover all use cases

    private readonly Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Pre-warm the pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    /// <summary>
    /// Get a single object and spawn at position. Returns null if pool is empty.
    /// </summary>
    public GameObject GetObject(Vector3 position)
    {
        if (pool.Count == 0)
        {
            Debug.LogWarning("RandomObjectPool exhausted. Consider increasing poolSize.");
            return null;
        }

        GameObject obj = pool.Dequeue();
        obj.transform.position = position;
        obj.transform.rotation = Quaternion.identity;
        obj.SetActive(true);

        // Optional: reset behavior
        var resettable = obj.GetComponent<IPoolable>();
        resettable?.OnObjectReuse();

        return obj;
    }

    /// <summary>
    /// Get multiple objects at given positions.
    /// </summary>
    public List<GameObject> GetMultipleObjects(List<Vector3> positions)
    {
        List<GameObject> spawned = new List<GameObject>();

        foreach (var pos in positions)
        {
            GameObject obj = GetObject(pos);
            if (obj != null)
                spawned.Add(obj);
        }

        return spawned;
    }

    /// <summary>
    /// Return object to the pool.
    /// </summary>
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
