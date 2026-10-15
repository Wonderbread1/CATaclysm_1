using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour
{
    public Image fadeScreen;
    public string nextSceneName;
    public float doorDelay = 3f;
    public float fadeDuration = 1f;

    private bool transitioning = false;

    private IEnumerator Start()
    {
        if (fadeScreen == null) yield break;

        Color color = fadeScreen.color;
        color.a = 1f;
        fadeScreen.color = color;

        yield return Fade(0f);
    }

    public void BeginTransition()
    {
        if (!transitioning)
            StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        transitioning = true;

        yield return new WaitForSeconds(doorDelay);
        yield return Fade(1f);

        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeScreen.color.a;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(
                startAlpha,
                targetAlpha,
                elapsed / fadeDuration
            );

            Color color = fadeScreen.color;
            color.a = alpha;
            fadeScreen.color = color;

            yield return null;
        }

        Color finalColor = fadeScreen.color;
        finalColor.a = targetAlpha;
        fadeScreen.color = finalColor;
    }
}