using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemy")]
public class EnemyData : ScriptableObject
{
    public float walkSpeed;
    public float runSpeed;
    public int damage;
    public float detectionRange;
    public float chaseRange;
    public int rotationSpeed;
}
