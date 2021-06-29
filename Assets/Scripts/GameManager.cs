using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Volume volume;
    private ColorAdjustments colorAdjustments;
    private float postExposure;

    private bool isEnding = false;

    public static bool freeze = false;
    public static bool pause = false;

    [SerializeField] private TMP_Text pauseText;
    [SerializeField] private CanvasGroup canvas;

    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider sfxVolume;

    [SerializeField] private AudioSource music;
    [SerializeField] private AudioLowPassFilter musicEffect;
    [SerializeField] private AudioSource deathSound;

    private void Awake()
    {
        instance = this;
        volume = FindObjectOfType<Volume>();
        volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);

        LeanTween.value(gameObject, postExposureUpdate, -10f, postExposure, 0.5f);
    }

    private void Update()
    {
        pauseText.enabled = pause;
        musicEffect.enabled = pause;

        if (Keyboard.current.spaceKey.wasPressedThisFrame && !isEnding)
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            pause = !pause;
        }

        music.volume = musicVolume.value;
        deathSound.volume = sfxVolume.value;
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

    public static void EndGame()
    {
        LeanTween.value(instance.gameObject, instance.postExposureUpdate, instance.colorAdjustments.postExposure.value, -10f, 1f)
        .setIgnoreTimeScale(true)
        .setOnComplete(() =>
        {
            LeanTween.alphaCanvas(instance.canvas, 1f, 1f).setOnComplete(() =>
            {
                instance.canvas.transform.GetChild(0).gameObject.SetActive(true);
            });
        });
    }
}
