using UnityEngine;

public class BreakableBarrel : MonoBehaviour
{

    private Animator animator;
    private bool isBroken = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeHit()
    {
        // Prevent the barrel from breaking more than once.
        if (isBroken) return;

        isBroken = true;

        // Play the destruction animation.
        animator.SetTrigger("Break");
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
