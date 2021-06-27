using UnityEngine;
using UnityEngine.InputSystem;

public class Select : MonoBehaviour
{
    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = Vector2.Lerp(transform.position,
                                          new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y)),
                                          Time.unscaledDeltaTime * 16);

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -9, 9), Mathf.Clamp(transform.position.y, -4, 4));
    }
}
