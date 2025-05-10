using System.Collections;
using UnityEngine;

public class CameraMovementControl : MonoBehaviour
{
    private Camera _mainCamera;

    private const float xLimit = 3f;
    private const float yLimit = 6f;
    private const float CameraZ = -10f;

    private const KeyCode _activation = KeyCode.Mouse1;

    private Vector2 _mouseClickPos;

    private void Awake()
    {
        _mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_activation))
        {
            _mouseClickPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            StartCoroutine(HandleCameraMovement());
        }
        HandleBoundaries();
    }

    private IEnumerator HandleCameraMovement()
    {
        while (Input.GetKey(_activation))
        {
            Vector2 mouseCurrentPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 distance = mouseCurrentPos - _mouseClickPos;
            transform.Translate(-distance.x, -distance.y, 0);
            yield return new WaitForEndOfFrame();
        }
    }

    private void HandleBoundaries()
    {
        Vector3 currentPosition = transform.position;
        float x = currentPosition.x;
        float y = currentPosition.y;

        float clampedX = Mathf.Clamp(x, -xLimit, xLimit);
        float clampedY = Mathf.Clamp(y, -yLimit, yLimit);

        transform.position = new Vector3(clampedX, clampedY, CameraZ);
    }
}
