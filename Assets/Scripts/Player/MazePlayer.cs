using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MazePlayer : MonoBehaviour
{

    private Rigidbody2D _rigidBody;         // player rigidbody
    private Animator animator;
    private BoxCollider2D boxColloider;

    private float moveHorizontal;
    private bool isGrounded;
    private float moveSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        boxColloider = GetComponent<BoxCollider2D>();

    }

    void FixedUpdate()
    {
        // movement
        _rigidBody.velocity = new Vector2(moveHorizontal * moveSpeed, _rigidBody.velocity.y);
    }

    // Update is called once per frame

    public void OnMove(InputAction.CallbackContext context)
    {
        moveHorizontal = context.ReadValue<float>();
    }

}
