using UnityEngine;

public class PressureSensor : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement.PlayerShape allowedShape;

    private Animator animator;

    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        animator.SetBool("IsActive", isActive);
    }

    void OnTriggerStay2D(Collider2D weightObject) {
        if(weightObject.tag == "Player" && PlayerMovement.currentShape == allowedShape) {
            isActive = true;
        }
        else if(weightObject.tag == "Ground Prop") {
            isActive = true;
        }
    }

    void OnTriggerExit2D() {
        if(isActive) {
            isActive = false;
        }
    }
}
