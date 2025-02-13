/* This Script Delay the spawn of Ball on score or miss and 
connected to paper and score the half score increase logic is there*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public score scoreManager; // Reference to Score script
    public paper paperScript;  // Reference to Paper script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("paper")) // Ensure the paper is correctly tagged
        {
            if (scoreManager != null)
            {
                scoreManager.AddScore(); // Update score
            }

            if (paperScript != null)
            {
                StartCoroutine(ResetBallAfterDelay()); // Reset ball after delay
            }
        }
    }

    private IEnumerator ResetBallAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // Small delay to prevent instant reset
        paperScript.ResetBall();
    }
}
