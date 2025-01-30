using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public Text scoretext;
    private int currentscore = 0;
     void Start()
     {
        updatescorer();
     }

     private void OnCollisionEnter(Collision collision)
     {
        if(collision.gameObject.CompareTag("score"))
        {
            currentscore++;
            updatescorer();
        }
     }
     void updatescorer()
     {
         scoretext.text = "Score: " + currentscore;
         Debug.Log("Score: " + currentscore);
     }
}
