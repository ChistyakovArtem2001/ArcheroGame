using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;
 
    public float rotationSpeed = 3f;
    private float currentRotationX = 0f;
    private float currentRotationY = 0f;
    public float minHeight = 1.0f;
    public float maxDistanceFromCollider = 5.0f;
    public float minPosition = 5f;
    void Update()
    {
        float HorizontalInput = Input.GetAxis("Mouse X") * rotationSpeed;
        float VerticalInput = Input.GetAxis("Mouse Y") * rotationSpeed;
        currentRotationX -= VerticalInput;
        currentRotationX = Mathf.Clamp(currentRotationX, minPosition, 80f);
        currentRotationY += HorizontalInput;
        Quaternion targetRotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);
        transform.position = target.position + rotation * dir;



    }
}
