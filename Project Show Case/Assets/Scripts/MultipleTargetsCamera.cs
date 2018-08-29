using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetsCamera : MonoBehaviour
{
    [SerializeField]
    private List<Transform> targets;

    private Vector3 velocity;

    [SerializeField]
    private float smoothTime;

    [SerializeField]
    private float minZoom, maxZoom, zoomLimiter;

    private Camera myCamera;

    private void Awake()
    {
        myCamera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        MoveCamera();
        ZoomCamera();
    }

    private void MoveCamera()
    {
        if (targets.Count==0)
        {
            return;
        }

        Vector3 centerPoint = GetCenterPoint();
        centerPoint = new Vector3(centerPoint.x, centerPoint.y, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, centerPoint, ref velocity, smoothTime);
    }

    private void ZoomCamera()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance()/zoomLimiter);
        myCamera.orthographicSize = Mathf.Lerp(myCamera.orthographicSize, newZoom, Time.deltaTime);
    }

    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
            return targets[0].transform.position;

        var bounds = new Bounds(targets[0].transform.position,Vector3.zero);

        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }

    private float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);

        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

}
