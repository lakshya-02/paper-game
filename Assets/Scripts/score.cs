using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class score : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("ball"))
            {
                Destroy(other.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
