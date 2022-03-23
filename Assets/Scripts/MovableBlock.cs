using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBlock : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement.PlayerShape allowedShape;

    private Rigidbody2D _rigidBody;
    private RigidbodyConstraints2D rbConstraints;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        rbConstraints = _rigidBody.constraints;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Player" && PlayerMovement.currentShape == allowedShape) {
            _rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        if(col.gameObject.tag == "Player") {
            _rigidBody.constraints = rbConstraints;
        }
    }
}
