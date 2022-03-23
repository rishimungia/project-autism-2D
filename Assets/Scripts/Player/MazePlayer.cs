using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MazePlayer : MonoBehaviour
{
    private Rigidbody2D _rigidBody;


    private float moveHorizontal;
    private float moveVertical;
    private float moveSpeed = 10.0f;


    [SerializeField]
    private Transform topCheck;
    [SerializeField]
    private Transform bottomCheck;
    [SerializeField]
    private Transform leftCheck;
    [SerializeField]
    private Transform rightCheck;

    [SerializeField]
    private LayerMask wallLayer;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (_rigidBody.velocity.x == 0 && _rigidBody.velocity.y == 0)
        {
            // horizontal movement
            if (moveHorizontal > 0 && !Physics2D.OverlapCircle(rightCheck.position, 0.01f, wallLayer))
            {
                _rigidBody.velocity = new Vector2(moveHorizontal * moveSpeed, 0);
            }
            if (moveHorizontal < 0 && !Physics2D.OverlapCircle(leftCheck.position, 0.01f, wallLayer))
            {
                _rigidBody.velocity = new Vector2(moveHorizontal * moveSpeed, 0);
            }
            if (moveVertical < 0 && !Physics2D.OverlapCircle(bottomCheck.position, 0.01f, wallLayer))
            {
                _rigidBody.velocity = new Vector2(0, moveVertical * moveSpeed);
            }
            if (moveVertical > 0 && !Physics2D.OverlapCircle(topCheck.position, 0.01f, wallLayer))
            {
                _rigidBody.velocity = new Vector2(0, moveVertical * moveSpeed);
            }
        }
        
    }

    // Update is called once per frame

    public void OnMoveHorizontal(InputAction.CallbackContext context)
    {
        moveHorizontal = context.ReadValue<float>();
    }
    
    public void OnMoveVertical(InputAction.CallbackContext context)
    {
        moveVertical = context.ReadValue<float>();

    }

}
