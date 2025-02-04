using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool isThrown = false;
    public float throwForce = 5f;  // Base throw force
    private float dragStartTime;   // Record drag start time
    private Vector3 initialPos;

    // Assign spawn points in the Unity Inspector
    public List<Transform> spawnPoints;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;  // Initially disable gravity
        rb.drag = 0.2f;         // Low drag for smooth movement
        rb.angularDrag = 0.5f;
        initialPos = transform.position;
    }

    void Update()
    {
        if (!isThrown)
        {
            // Floating Effect (before throw)
            transform.position += new Vector3(0, Mathf.Sin(Time.time * 2f) * 0.005f, 0);
        }

        if (Input.GetMouseButtonDown(0)) // Start Drag
        {
            startPos = Input.mousePosition;
            dragStartTime = Time.time; // Record drag start time
        }

        if (Input.GetMouseButtonUp(0)) // Release Drag
        {
            endPos = Input.mousePosition;
            float dragDuration = Time.time - dragStartTime; // Calculate duration

            Vector3 difference = endPos - startPos;

            // Convert screen-space movement to world-space direction
            Vector3 throwDirection = new Vector3(difference.x, difference.y, difference.magnitude).normalized;

            // Scale throwForce by dragDuration for increased power but limit max force
            float forceMultiplier = Mathf.Clamp(dragDuration, 0.5f, 2f); // Prevent extreme power values
            rb.velocity = throwDirection * throwForce * forceMultiplier;

            isThrown = true;
            rb.useGravity = true; // Gravity starts immediately after the throw
            StartCoroutine(SmoothSlowdown());
        }

        // Instant reset on pressing "R"
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetBall();
        }
    }

    IEnumerator SmoothSlowdown()
    {
        // Gradually increase drag to slow down the ball
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            rb.drag = Mathf.Lerp(0.2f, 3f, t); // Smooth drag increase
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("score") || other.gameObject.CompareTag("floor"))
        {
            ResetBall();
        }
    }

    void ResetBall()
    {
        StopAllCoroutines(); // Stop any slowdown coroutine to prevent unwanted effects

        // Select a random spawn point if available
        if (spawnPoints != null && spawnPoints.Count > 0)
        {
            int index = Random.Range(0, spawnPoints.Count);
            transform.position = spawnPoints[index].position;
        }
        else
        {
            // If no spawn points are set, reset to the initial position
            transform.position = initialPos;
        }

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.drag = 0.2f;
        isThrown = false;
    }
}
