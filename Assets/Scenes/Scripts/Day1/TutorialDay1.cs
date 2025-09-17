using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TutorialDay1 : MonoBehaviour
{
    [Header("UI References")]
    public GameObject tutorialPanel;       // Main tutorial panel
    public Canvas growthCanvas;            // Canvas for the growth bar
    public GameObject pressSpaceHint;      // Optional "Press Space" hint text

    [Header("Settings")]
    public float fadeDuration = 0.4f;      // Fade-out time
    public float hintDelay = 1f;           // Delay before showing press-space hint
    public bool useFade = true;            // Toggle fade-out effect

    public static bool IsTutorialActive { get; private set; } = true;

    private float originalFixedDelta;

    void Awake()
    {
        originalFixedDelta = Time.fixedDeltaTime;
    }

    void Start()
    {
        // Show tutorial panel
        if (tutorialPanel != null) tutorialPanel.SetActive(true);

        // Hide growth bar during tutorial
        if (growthCanvas != null) growthCanvas.enabled = false;

        // Hide hint initially
        if (pressSpaceHint != null) pressSpaceHint.SetActive(false);

        // Pause game
        Time.timeScale = 0f;
        Time.fixedDeltaTime = originalFixedDelta * Time.timeScale;

        IsTutorialActive = true;

        // Show "Press Space" hint after a delay
        if (pressSpaceHint != null)
            StartCoroutine(ShowHintAfterDelay());
    }

    void Update()
    {
        if (IsTutorialActive && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(EndTutorialRoutine());
        }
    }

    private IEnumerator ShowHintAfterDelay()
    {
        yield return new WaitForSecondsRealtime(hintDelay);
        if (pressSpaceHint != null && tutorialPanel.activeSelf)
        {
            pressSpaceHint.SetActive(true);
        }
    }

    private IEnumerator EndTutorialRoutine()
    {
        // Optional fade-out
        if (useFade && tutorialPanel != null && fadeDuration > 0f)
        {
            CanvasGroup cg = tutorialPanel.GetComponent<CanvasGroup>();
            if (cg == null)
            {
                cg = tutorialPanel.AddComponent<CanvasGroup>();
                cg.alpha = 1f;
            }

            float t = 0f;
            while (t < fadeDuration)
            {
                t += Time.unscaledDeltaTime;
                cg.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
                yield return null;
            }
        }

        // Hide UI
        if (tutorialPanel != null) tutorialPanel.SetActive(false);
        if (pressSpaceHint != null) pressSpaceHint.SetActive(false);

        // Resume game
        Time.timeScale = 1f;
        Time.fixedDeltaTime = originalFixedDelta;
        if (growthCanvas != null) growthCanvas.enabled = true;

        IsTutorialActive = false;
        Debug.Log("✅ Tutorial finished, gameplay active");
    }

    void OnDestroy()
    {
        // Safety reset if destroyed mid-tutorial
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = originalFixedDelta;
        }

        IsTutorialActive = false;
    }
}