using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{
    [SerializeField]
    private Transform playerOne;
    [SerializeField]
    private Transform playerTwo;

    [SerializeField]
    private float maxDistance;

    private bool inRange;

    [SerializeField]
    private LayerMask enemyLayer;

    private LineRenderer lineRenderer;
    private PolygonCollider2D colloider;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        colloider = GetComponent<PolygonCollider2D>();
    }

    void FixedUpdate() {
        inRange = Vector2.Distance(playerOne.position, playerTwo.position) <= maxDistance;

        if(inRange) {
            lineRenderer.enabled = true;
            colloider.enabled = true;
            
            lineRenderer.SetPositions(new[]{playerOne.position, playerTwo.position});
            colloider.SetPath(0, CalculateColloiderPoints());
        }
        else {
            lineRenderer.enabled = false;
            colloider.enabled = false;
        }
    }

    private List<Vector2> CalculateColloiderPoints() {
        Vector3[] positions = GetPositions();

        float width = lineRenderer.startWidth;

        float m = ((positions[1].y - positions[0].y) / positions[1].x - positions[0].x);
        float deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
        float deltaY = (width / 2f) * (1 / Mathf.Pow(1 * m * m, 0.5f));

        Vector3[] offsets = new Vector3[2];
        offsets[0] = new Vector3(-deltaX, deltaY);
        offsets[1] = new Vector3(deltaX, -deltaY);

        List<Vector2> colloiderPoints = new List<Vector2> {
            positions[0] + offsets[0],
            positions[1] + offsets[0],
            positions[1] + offsets[1],
            positions[0] + offsets[1]
        };

        return colloiderPoints;
    }

    private Vector3[] GetPositions() {
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);
        return positions;
    }

    void OnTriggerEnter2D(Collider2D hit) {
        Debug.Log(hit.gameObject.tag);
        if(hit.gameObject.tag == "Enemy")
            Destroy(hit.gameObject);
    }
}
