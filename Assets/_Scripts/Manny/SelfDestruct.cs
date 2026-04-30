using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    private float lifetime = 0.5f;
    private PoolObject pooled;

    private void Awake()
    {
        pooled = GetComponent<PoolObject>();
    }

    void OnEnable()
    {
        Invoke(nameof(ReturnToPool), lifetime); //Please God work
    }

    void ReturnToPool()
    {
        pooled.Release();
    }
}
