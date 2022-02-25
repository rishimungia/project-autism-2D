using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeDoor : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement.PlayerShape allowedShape;
    [SerializeField]
    private LayerMask allowedLayers;
    [SerializeField]
    private float detectionRadius;

    private Animator animator;

    private bool inRange;
    private bool isUnlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        inRange = Physics2D.OverlapCircle(transform.position, detectionRadius, allowedLayers);

        isUnlocked = (inRange && PlayerMovement.currentShape == allowedShape);

        animator.SetBool("isUnlocked", isUnlocked);
    }
}
