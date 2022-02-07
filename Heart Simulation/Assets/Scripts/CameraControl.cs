using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    private Transform resetTarget;
    public Vector3 resetPoint, newPos;

    [SerializeField]
    private int speed;
    public void Start()
    {
        resetTarget = target;
        resetPoint = transform.position;
        newPos = transform.position;
    }

    private void Update()
    {
        var targetRotation = Quaternion.LookRotation(target.position - transform.position);

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);
    }

    public void setTarget(Transform newTarget, Transform newPosition)
    {
        target = newTarget;
        newPos = newPosition.position;
    }

    public void reset()
    {
        target = resetTarget;
        newPos = resetPoint;
    }
}
