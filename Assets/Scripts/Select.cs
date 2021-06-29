using UnityEngine;
using UnityEngine.InputSystem;

public class Select : MonoBehaviour
{
    [SerializeField] Color validColor;
    [SerializeField] Color badColor;

    public static Vector2 selection = Vector2.zero;
    public static bool valid = false;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        selection = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
        selection = new Vector2(Mathf.Clamp(selection.x, -10, 10), Mathf.Clamp(selection.y, -4, 4));

        transform.position = Vector2.Lerp(transform.position, selection, Time.unscaledDeltaTime * 16);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -10, 10), Mathf.Clamp(transform.position.y, -4, 4));

        if (Mathf.Abs(selection.y) != 1 || Mathf.Abs(selection.x) > 8 || selection.x == 0)
        {
            sprite.color = badColor;
            valid = false;
        }
        else
        {
            sprite.color = validColor;
            valid = true;
        }
    }
}
