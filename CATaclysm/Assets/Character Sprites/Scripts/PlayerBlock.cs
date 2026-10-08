using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    private Animator animator;
    private PlayerAnimation playerAni;
    private PlayerAction playerAction;
    private PlayerAttack playerAtk;

    //animations for block
    public string block_right = "MC_Block_Right_Clip";
    public string block_left = "MC_Block_Left_Clip";
    public string block_up = "MC_Block_Up_Clip";
    public string block_down = "MC_Block_Down_Clip";

    public float blockLength = 0.4f;

    public bool isBlock = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        playerAni = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAtk != null && playerAtk.isAtk)
        {
            return;
        }
        if (playerAction != null && playerAction.isAction)
        {
            return;
        }

        if (!isBlock && Input.GetMouseButton(1))
        {
            PerformBlock();
        }
    }

    void PerformBlock()
    {
        //finds the last direction to play the right block animation that faces the same direction
        switch (playerAni.lastDir)
        {
            case "right":
                animator.Play(block_right);
                break;
            case "left":
                animator.Play(block_left);
                break;
            case "up":
                animator.Play(block_up);
                break;
            case "down":
                animator.Play(block_down);
                break;
        }

        Invoke(nameof(ResetBlock), blockLength);


    }

    private void ResetBlock()
    {
        isBlock = false;
    }

}
