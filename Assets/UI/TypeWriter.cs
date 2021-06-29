using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    private TMP_Text textComponent;
    private string text;

    public float delay = 0f;

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        text = textComponent.text;
        textComponent.text = "";
    }

    private void OnEnable()
    {
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        yield return new WaitForSecondsRealtime(delay);
        for (int i = 0; i < text.Length; i++)
        {
            textComponent.text += text[i];
            yield return new WaitForSecondsRealtime(Random.Range(0.05f, 0.1f));
        }
    }
}
