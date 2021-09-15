using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHigh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) //This will be called when our coin triggered
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<PlayerController>().CallJumpHigh();
            GetComponent<Renderer>().enabled = false;
        }
    }

}
