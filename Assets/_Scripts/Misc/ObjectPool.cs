using System.Collections.Generic;
using UnityEngine;
public class ObjectPool : MonoBehaviour
{
    
    [SerializeField] private List<Pool> Poolsi;
    
    public static ObjectPool Instance;
    private Dictionary<string, Queue<GameObject>> pooldic;
    
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int poolSize;
    }
    
    private void Awake()
    {
        // Singleton did not save the explosions so i decided to just make a new one here
        pooldic = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in Poolsi)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();
            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectQueue.Enqueue(obj);
            }
            pooldic.Add(pool.tag, objectQueue);
        }
    }
    
    public GameObject GetFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!pooldic.ContainsKey(tag))
        {
            return null;
        }

        GameObject obj;

        if (pooldic[tag].Count > 0)
        {
            obj = pooldic[tag].Dequeue();
        }
        else
        {
            obj = Instantiate(GetPrefab(tag));
            obj.GetComponent<PoolObject>().tag = tag;
        }
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        return obj;
    }
    
    public void ReleaseToPool(string tag, GameObject go)
    {
        go.SetActive(false);
        pooldic[tag].Enqueue(go);
    }
    
    private GameObject GetPrefab(string tag)
    {
        foreach (Pool pool in Poolsi)
        {
            if (pool.tag == tag)
                return pool.prefab;
        }

        return null;
    }
    
}
