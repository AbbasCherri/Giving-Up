using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public string tag;

    public void Release()
    {
        ObjectPool.Instance.ReleaseToPool(tag, gameObject);
    }
}