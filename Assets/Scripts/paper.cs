using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool isThrown = false;
    private float throwForce = 5f; // Increased force for a natural throw

    // Stores initial spawn position
    private Vector3 initialPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Initially disable gravity
        rb.drag = 0.2f;  // Low drag for smooth movement
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
        }

        if (Input.GetMouseButtonUp(0)) // Release Drag
        {
            endPos = Input.mousePosition;
            Vector3 difference = endPos - startPos;

            // Convert screen-space movement to world-space
            Vector3 throwDirection = new Vector3(difference.x, difference.y, difference.magnitude).normalized;

            // Apply force to the ball
            rb.velocity = throwDirection * throwForce;
            isThrown = true;

            // Start gravity and slow down effect
            StartCoroutine(EnableGravityAndSlowDown());
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            ResetBall();
        }
        if (other.gameObject.CompareTag("floor"))
        {
            ResetBall();
        }
    }

    IEnumerator EnableGravityAndSlowDown()
    {
        yield return new WaitForSeconds(0.5f); // Wait before enabling gravity

        rb.useGravity = true; // Now gravity applies

        // Gradually increase drag to slow down the ball
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            rb.drag = Mathf.Lerp(0.2f, 3f, t); // Increase drag over time
            yield return null;
        }
    }

    void ResetBall()
    {
        transform.position = initialPos;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false; // Disable gravity again
        rb.drag = 0.2f; // Reset drag
        isThrown = false;
    }
}
