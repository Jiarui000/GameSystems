using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSurveillance : MonoBehaviour
{
    public Camera[] cameras;
    public KeyCode prevKey = KeyCode.Q;
    public KeyCode nextKey = KeyCode.E;
    private int camIndex;
    private int camMax;
    private Camera current;

	// Use this for initialization
	void Start ()
    {
        cameras = GetComponentsInChildren<Camera>();
        camMax = cameras.Length - 1;
        ActivateCamera(camIndex);
	}
	void ActivateCamera(int camIndex)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            Camera cam = cameras[i];
            if (i == camIndex)
            {
                cam.gameObject.SetActive(true);
            }
            else
            {
                cam.gameObject.SetActive(false);
            }
        }
    }
    // Update is called once per frame
    void Update ()
    {
	    if (Input.GetKeyDown(nextKey))
        {
            camIndex++;
            if(camIndex > camMax)
            {
                camIndex = 0;
            }
            ActivateCamera(camIndex);
        }
        if (Input.GetKeyDown(prevKey))
        {
            camIndex--;
            if (camIndex < 0)
            {
                camIndex = camMax;
            }
            ActivateCamera(camIndex);
        }
    }
}
