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

    [SerializeField] private AnimationCurve animationCurve;
    private bool active = false;

    private void Start()
    {
        cam = GetComponent<Camera>();
        zoom = cam.orthographicSize;
        transform.position = new Vector3(0f, 20f, -10f);
        pos = transform.position;
    }

    private void Activate()
    {
        active = true;
        pos = transform.position;
        FindObjectOfType<Waves>().StartCoroutine(FindObjectOfType<Waves>().Play());
    }

    public void PlayAnimation()
    {
        LeanTween.moveY(gameObject, 0f, 1.5f).setEase(animationCurve).setOnComplete(Activate);
    }

    private void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame && !active)
        {
            PlayAnimation();
        }

        if (!active) return;

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

        transform.position = Vector3.Lerp(transform.position, pos, Time.unscaledDeltaTime * 16f);

        zoom -= Mouse.current.scroll.ReadValue().y / 200;
        zoom = Mathf.Clamp(zoom, 2f, 12f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, Time.unscaledDeltaTime * 16f);
    }
}
