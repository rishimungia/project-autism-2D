using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerShape {
        Square,
        Triangle,
    }

    public static PlayerShape currentShape;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private LayerMask groundLayer;

    private Rigidbody2D _rigidBody;         // player rigidbody
    private Animator animator;

    private float moveHorizontal;
    private bool isGrounded;
    
    void Start() {
        _rigidBody = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        currentShape = PlayerShape.Square;
    }

    void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // movement
        _rigidBody.velocity = new Vector2(moveHorizontal * moveSpeed, _rigidBody.velocity.y);

        // restart game if fallen into void
        if (_rigidBody.transform.position.y < -25.0f)
            GameManager.Instance.Restart();
    }

    // movement input
    public void OnMove (InputAction.CallbackContext context) {
        moveHorizontal = context.ReadValue<float>();
    }

    // jump
    public void OnJump (InputAction.CallbackContext context) {
        if(isGrounded)
            _rigidBody.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
    }

    public void OnShapeShift(InputAction.CallbackContext context) {
        Debug.Log("Shape");
        if(currentShape == PlayerShape.Square) {
            currentShape = PlayerShape.Triangle;
            animator.SetBool("isTriangle", true);
            animator.SetBool("isSquare", false);
        }
        else {
            currentShape = PlayerShape.Square;
            animator.SetBool("isTriangle", false);
            animator.SetBool("isSquare", true);
        }
    }

}
