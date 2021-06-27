using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Volume volume;
    private ColorAdjustments colorAdjustments;
    private float postExposure;

    private bool isEnding = false;

    private void Awake()
    {
        instance = this;
        volume = FindObjectOfType<Volume>();
        volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);

        LeanTween.value(gameObject, postExposureUpdate, -10f, postExposure, 0.5f);
    }

    private void Update()
    {

        if (Keyboard.current.spaceKey.wasPressedThisFrame && !isEnding)
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }
    }

    private void postExposureUpdate(float val) => colorAdjustments.postExposure.value = val;

    public static void End()
    {
        instance.StartCoroutine(instance.EndAnim());
    }

    private IEnumerator EndAnim()
    {
        isEnding = true;
        yield return new WaitForSeconds(0.2f);

        void slowMoUpdate(float val) => Time.timeScale = val;
        LeanTween.value(gameObject, slowMoUpdate, 1f, 0f, 1f).setEase(LeanTweenType.easeOutQuad).setIgnoreTimeScale(true);

        yield return new WaitForSecondsRealtime(1.5f);

        LeanTween.value(gameObject, postExposureUpdate, colorAdjustments.postExposure.value, -10f, 0.5f).setIgnoreTimeScale(true);

        yield return new WaitForSecondsRealtime(0.5f);

        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
