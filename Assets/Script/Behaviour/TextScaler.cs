using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScaler : MonoBehaviour
{
    private Vector3 StartScale;
    private Vector3 ResizeScale;
    private bool ResizeSml;

    public GameObject LoginPanel;
    private bool IsLoginPanel = true;

    // Start is called before the first frame update
    void Start()
    {
        StartScale = new Vector3(0.7f, 0.7f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsLoginPanel) { return; }
        if (LoginPanel.activeSelf == false)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 415, transform.position.z);
            transform.localScale = StartScale;
            IsLoginPanel = false;
            return;
        }
        
        if (!ResizeSml)
        {
            ResizeScale = new Vector3(ResizeScale.x + Time.deltaTime, ResizeScale.y + Time.deltaTime, ResizeScale.z + Time.deltaTime);
            if (ResizeScale.x >= 1.5f)
            {
                ResizeSml = true;
            }
        }
        else
        {
            ResizeScale = new Vector3(ResizeScale.x - Time.deltaTime, ResizeScale.y - Time.deltaTime, ResizeScale.z - Time.deltaTime);
            if (ResizeScale.x <= StartScale.x)
            {
                ResizeSml = false;
            }
        }

        transform.localScale = ResizeScale;
    }
}
