using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;
    public bool hideCursor = true;
    [Header("Orbit")]
    public Vector3 offset = new Vector3(0, 5f, 0);
    public float xSpeed = 120f;
    public float ySpeed = 120f;
    public float yMinLimit = -20;
    public float yMaxLimit = 80;
    public float distanceMax = 15f;
    [Header("Collision")]
    public bool cameraCollision = true;//is cam collision enabled?
    public float camRaius = 0.3f;// radius of cam collision cast
    public LayerMask ignoreLayers;// layers ignored by 
    private Vector3 originalOffset;
    private float distance;
    private float rayDistance = 1000f;
    private float x, y;
    



    // Use this for initialization
    void Start ()
    {
        // Detach camera from parent
        transform.SetParent(null);
        //Set Target
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // is the cursor supposed to be hidden?
        if (hideCursor)//if hide cursor is true 
        {
            //Lock...
            Cursor.lockState = CursorLockMode.Locked;
            //...hide the Cursor
            Cursor.visible = false;
        }
        //Calculate original offset from target position
        originalOffset = transform.position - target.position;
        Vector3 angles = transform.eulerAngles;
        //set X and Y degrees to current camera rotation
        x = angles.y;
        y = angles.x;
	}

    void FixedUpdate()
    {
        if (target)
        {
            if (cameraCollision)
            {
                Ray camRay = new Ray(target.position, -transform.forward);
                RaycastHit hit;
                if(Physics.SphereCast(camRay, camRaius, out hit, rayDistance, ~ignoreLayers, QueryTriggerInteraction.Ignore))
                {
                    distance = hit.distance;
                    return;
                }
            }
            // Set distance to original distance
            distance = originalOffset.magnitude;
        }
    }

    // Update is called once per frame
    void Update ()
    {
		//if target
        if (target)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
            //c
            y = ClampAngle(y, yMinLimit, yMaxLimit);
            transform.rotation = Quaternion.Euler(y, x, 0);
        }
	}
    public static float ClampAngle (float angle, float min,float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
            
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
    void LateUpdate()
    {
        Vector3 localOffset = transform.TransformDirection(offset);
        transform.position = (target.position + localOffset) + -transform.forward * distance;
    }
}