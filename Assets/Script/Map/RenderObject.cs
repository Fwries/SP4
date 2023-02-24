using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderObject : MonoBehaviour
{
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera.transform.position.z - this.transform.position.z > 1.5)
        {
            this.gameObject.GetComponent<Renderer>().enabled = false;
        }
        else if (this.transform.position.z - mainCamera.transform.position.z > 17)
        {
            this.gameObject.GetComponent<Renderer>().enabled = false;
        }
        else if (mainCamera.transform.position.x - this.transform.position.x > 15.3)
        {
            this.gameObject.GetComponent<Renderer>().enabled = false;
        }
        else if (this.transform.position.x - mainCamera.transform.position.x > 15.3)
        {
            this.gameObject.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().enabled = true;
        }
    }
}
