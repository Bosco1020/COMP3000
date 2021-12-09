using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    private Transform resetPoint;
    private bool zoomed;
    Camera cam;
    private int speed;
    public void Start()
    {
        resetPoint = target;
        cam = Camera.main;
        speed = 5;
    }

    private void Update()
    {
        var targetRotation = Quaternion.LookRotation(target.position - transform.position);

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);

        if (zoomed && cam.fieldOfView != 3)
        {
            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, 1, 30 * Time.deltaTime);
        }
        else if (!zoomed && cam.fieldOfView != 60)
        {
            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, 60, 30 * Time.deltaTime);
        }
    }

    public void setTarget(Transform newTarget)
    {
        target = newTarget;
        zoomed = true;
        speed = 5;
        //Camera.main.fieldOfView = 8;
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 30);
    }

    public void reset()
    {
        target = resetPoint;
        zoomed = false;
        speed = 1;
        //Camera.main.fieldOfView = 60;
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 30);
    }
}
