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
        playerAction = GetComponent<PlayerAction>();
        playerAtk = GetComponent<PlayerAttack>();
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

        if (!isBlock && Input.GetMouseButtonDown(1))
        {
            PerformBlock();
        }
    }

    void PerformBlock()
    {
        isBlock = true;

        string statePlay = "";

        //finds the last direction to play the right block animation that faces the same direction
        switch (playerAni.lastDir)
        {
            case "right":
                statePlay = (block_right);
                break;
            case "left":
                statePlay = (block_left);
                break;
            case "up":
                statePlay = (block_up);
                break;
            case "down":
                statePlay = (block_down);
                break;
        }

        if (!string.IsNullOrEmpty(statePlay))
        {
            animator.Play(statePlay, 0, 0f);

        }
       


        Invoke(nameof(ResetBlock), blockLength);


    }

    private void ResetBlock()
    {
        isBlock = false;
    }

}
