
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControlsScreenLoader : MonoBehaviour
{
    [Header("Loading Settings")]
    public float displayDuration = 15f;
    public string nextScene = "level 1";

    private IEnumerator Start()
    {
        // Ensure time is running normally.
        Time.timeScale = 1f;

        // Display controls for 15 seconds.
        yield return new WaitForSecondsRealtime(displayDuration);

        // Load Level 1.
        SceneManager.LoadScene(nextScene);
    }
}
