
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TBCEnding : MonoBehaviour
{
    [Header("Ending Screen")]
    public Image tbcImage;
    public float fadeDuration = 2f;

    [Header("Restart Settings")]
    public float restartDelay = 10f;
    public string firstLevelScene = "level 1";

    private bool endingStarted = false;

    private void Start()
    {
        Time.timeScale = 1f;

        if (tbcImage != null)
        {
            Color color = tbcImage.color;
            color.a = 0f;
            tbcImage.color = color;

            tbcImage.raycastTarget = false;
        }
    }

    public void StartEnding()
    {
        if (endingStarted || tbcImage == null)
            return;

        endingStarted = true;
        StartCoroutine(EndingSequence());
    }

    private IEnumerator EndingSequence()
    {
        float elapsed = 0f;

        // Fade in the TBC screen.
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;

            Color color = tbcImage.color;
            color.a = Mathf.Clamp01(elapsed / fadeDuration);
            tbcImage.color = color;

            yield return null;
        }

        Color finalColor = tbcImage.color;
        finalColor.a = 1f;
        tbcImage.color = finalColor;

        // Freeze gameplay while the ending is displayed.
        Time.timeScale = 0f;

        Debug.Log("CATaclysm beta completed!");

        // Wait 10 seconds, even while gameplay is frozen.
        yield return new WaitForSecondsRealtime(restartDelay);

        // Reset saved objective key progress.
        GameProgress.hasObjectiveKey = false;

        // Restore normal gameplay time.
        Time.timeScale = 1f;

        // Restart the game from Level 1.
        SceneManager.LoadScene(firstLevelScene);
    }
}
