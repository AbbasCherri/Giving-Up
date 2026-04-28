using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitDetections : MonoBehaviour
{
    [SerializeField] LayerMask grabbableLayers;

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 3f, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit, 3f, grabbableLayers))
        {
            Debug.Log("I hit " + hit.collider.name);
        }
    }
}
