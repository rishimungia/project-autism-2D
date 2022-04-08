using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeamPlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private Vector2 moveVector;

    private Rigidbody2D _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        _rigidBody.velocity = moveVector * moveSpeed;
    }

    public void PlayerOneMove(InputAction.CallbackContext context) {
        moveVector = context.ReadValue<Vector2>();
    }
}
