using UnityEngine;

public class LeverSwitch : MonoBehaviour
{
    public Animator leverAnimator;

    public bool isOn = false;

    public LeverPuzzleManager puzzleManager;

    public void TakeHit()
    {
        isOn = !isOn;
        leverAnimator.SetTrigger("Activate");

        if (puzzleManager != null)
            puzzleManager.CheckLevers();

        Debug.Log(gameObject.name + " is now " +
                  (isOn ? "ON" : "OFF"));
    }

    // Temporary testing method
    [ContextMenu("Test Lever Hit")]
    private void TestLeverHit()
    {
        TakeHit();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
