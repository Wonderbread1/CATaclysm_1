
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerDeathRestart : MonoBehaviour
{
    [Header("Restart Settings")]
    public float restartDelay = 2f;

    private Health playerHealth;
    private bool isRestarting = false;

    private void Start()
    {
        playerHealth = GetComponent<Health>();
    }

    private void Update()
    {
        if (isRestarting || playerHealth == null)
            return;

        if (playerHealth.curr_Health <= 0)
        {
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        if (isRestarting)
            return;

        isRestarting = true;
        StartCoroutine(RestartAfterDelay());
    }

    private IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSecondsRealtime(restartDelay);

        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
