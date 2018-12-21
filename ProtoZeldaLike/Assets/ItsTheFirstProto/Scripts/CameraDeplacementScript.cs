using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDeplacementScript : MonoBehaviour {

    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private Vector3 smoothedPosition;

    private Camera mainCamera;

    private bool isFollowing = true;

    public Vector2 margin = new Vector2(1,1);

    private Vector3 min, max;

    public BoxCollider2D cameraBounds;

    private void Start()
    {
        min = cameraBounds.bounds.min;
        max = cameraBounds.bounds.max;

        mainCamera = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }

    void Update()
    {
        var x = transform.position.x;
        var y = transform.position.y;

        if (isFollowing)
        {
            if (Mathf.Abs(x - target.position.x) > margin.x)
                x = Mathf.Lerp(x, target.position.x, smoothSpeed * Time.deltaTime);

            if (Mathf.Abs(y - target.position.y) > margin.y)
                y = Mathf.Lerp(y, target.position.y, smoothSpeed * Time.deltaTime);
        }

        // ortographicSize is the haldf of the height of the Camera.
        var cameraHalfWidth = mainCamera.orthographicSize * ((float)Screen.width / Screen.height);

        x = Mathf.Clamp(x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
        y = Mathf.Clamp(y, min.y + mainCamera.orthographicSize, max.y - mainCamera.orthographicSize);

        transform.position = new Vector3(x, y, transform.position.z);
    }

    // PixelPerfectScript.
    public static float RoundToNearestPixel(float unityUnits, Camera viewingCamera)
    {
        float valueInPixels = (Screen.height / (viewingCamera.orthographicSize * 2)) * unityUnits;
        valueInPixels = Mathf.Round(valueInPixels);
        float adjustedUnityUnits = valueInPixels / (Screen.height / (viewingCamera.orthographicSize * 2));
        return adjustedUnityUnits;
    }

    void LateUpdate()
    {
        Vector3 newPos = transform.position;
        Vector3 roundPos = new Vector3(RoundToNearestPixel(newPos.x, mainCamera), RoundToNearestPixel(newPos.y, mainCamera), newPos.z);
        transform.position = roundPos;
    }

    public void UpdateBounds()
    {
        min = cameraBounds.bounds.min;
        max = cameraBounds.bounds.max;
    }
}
