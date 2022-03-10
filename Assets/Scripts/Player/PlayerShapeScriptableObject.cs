using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerShape", menuName = "ScriptableObjects/PlayerShapeScriptableObject")]
public class PlayerShapeScriptableObject : ScriptableObject
{
    public float moveSpeed;
    public float jumpForce;

    public float playerMass;
}
