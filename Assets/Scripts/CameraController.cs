using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    private float zoom;

    private Vector3 origin;
    private Vector3 difference;
    private Vector3 pos;

    private bool drag = false;

    private void Start()
    {
        cam = GetComponent<Camera>();
        zoom = cam.orthographicSize;
        pos = transform.position;
    }

    private void Update()
    {
        if (Mouse.current.rightButton.isPressed || Mouse.current.middleButton.isPressed)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();

            difference = cam.ScreenToWorldPoint(mousePos) - transform.position;

            if (!drag)
            {
                origin = cam.ScreenToWorldPoint(mousePos);
                drag = true;
            }
        }
        else
        {
            drag = false;
        }

        if (drag)
        {
            pos = origin - difference;
        }

        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 16f);

        zoom -= Mouse.current.scroll.ReadValue().y / 200;
        zoom = Mathf.Clamp(zoom, 2f, 12f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, Time.deltaTime * 16f);
    }
}
