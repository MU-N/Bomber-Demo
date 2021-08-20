using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] float cameraFollowSpeed = 0.2f;

    Vector3 cameraFollowVelocity = Vector3.zero;

    private void LateUpdate()
    {
        FollowTarget();
    }

    public void FollowTarget()
    {
        Vector3 targtePos = Vector3.SmoothDamp(transform.position , targetTransform.position ,  ref cameraFollowVelocity , cameraFollowSpeed);
        transform.position = targtePos;
    }
}
