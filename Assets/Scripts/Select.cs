using UnityEngine;
using UnityEngine.InputSystem;

public class Select : MonoBehaviour
{
    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = Vector2.Lerp(transform.position,
                                          new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y)),
                                          Time.deltaTime * 16);
    }
}
