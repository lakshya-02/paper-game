/*This Script is for properties of Paper , check if is it thrown or not , add the Sin function , 
add the reset ball function , Define the logic of Resetball.*/ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paper : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool isThrown = false;
    public float throwForce = 5f; 
    private float dragStartTime;
    private Vector3 initialPos;

    public List<Transform> spawnPoints; // Holds possible spawn locations

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; 
        rb.drag = 0.2f;
        rb.angularDrag = 0.5f;
        initialPos = transform.position;
    }
    void Update()
    {
        if (!isThrown)
        {
            // Floating Effect before throw
            transform.position += new Vector3(0, Mathf.Sin(Time.time * 2f) * 0.005f, 0);
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            startPos = Input.mousePosition;
            dragStartTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            endPos = Input.mousePosition;
            float dragDuration = Time.time - dragStartTime;

            Vector3 difference = endPos - startPos;
            Vector3 throwDirection = new Vector3(difference.x, difference.y, difference.magnitude).normalized;

            rb.velocity = throwDirection * throwForce * dragDuration;
            isThrown = true;

            StartCoroutine(EnableGravityAndSlowDown());
        }
    }

    IEnumerator EnableGravityAndSlowDown()
    {
        yield return new WaitForSeconds(0.5f);
        rb.useGravity = true;

        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            rb.drag = Mathf.Lerp(0.2f, 3f, t);
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("score") || other.gameObject.CompareTag("floor"))
        {
            ResetBall();
        }
    }

    public void ResetBall()
    {
        if (spawnPoints != null && spawnPoints.Count > 0)
        {
            int index = Random.Range(0, spawnPoints.Count);
            transform.position = spawnPoints[index].position;
        }
        else
        {
            transform.position = initialPos;
        }
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.drag = 0.2f;
        isThrown = false;
    }
}
