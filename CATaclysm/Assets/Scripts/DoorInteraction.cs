using UnityEngine;

public class DoorInteraction : MonoBehaviour
{

    public Animator doorAnimator;
    public LevelTransition levelTransition;

    private int doorState = 0;

    public void Interact(bool hasCorrectKey)
    {
        if (doorState == 0 && hasCorrectKey)
        {
            doorAnimator.SetTrigger("InsertKey");
            doorState = 1;
        }
        else if (doorState == 1)
        {
            doorAnimator.SetTrigger("Open");
            doorState = 2;

            levelTransition.BeginTransition();
        }
    }

    [ContextMenu("Test Door Interaction")]
    private void TestDoorInteraction()
    {
        Interact(true);
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
