using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private Transform finalPos;

    [SerializeField]
    private float moveTime;

    [SerializeField]
    private bool oscillate;

    [SerializeField]
    private PressureSensor activator;
    
    private Rigidbody2D _rigidBody;

    // Start is called before the first frame update
    void Start() {
        _rigidBody = GetComponent<Rigidbody2D>();

        transform.position = startPos.position;
    }

    void FixedUpdate() {

        if(activator.isActive) {
            if(transform.position != finalPos.position && !oscillate) {
                _rigidBody.MovePosition(Vector2.Lerp(finalPos.position, transform.position, moveTime));
            }

            else if(oscillate) {
                Debug.Log("Oscillate");
                if (transform.position != finalPos.position)
                    _rigidBody.MovePosition(Vector2.Lerp(finalPos.position, transform.position, moveTime));
                else if(transform.position != startPos.position)
                    _rigidBody.MovePosition(Vector2.Lerp(startPos.position, transform.position, moveTime));
            }
        }
        
        if(!activator.isActive) {
            if(transform.position != startPos.position && !oscillate) {
                _rigidBody.MovePosition(Vector2.Lerp(startPos.position, transform.position, moveTime));
            }
        }
    }
}
