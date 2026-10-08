using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class PlayerAction : MonoBehaviour
{
    private Animator animate;
    private PlayerAnimation playerAni;
    private PlayerBlock playerBlock;
    private PlayerAttack playerAtk;

    //simple action animations
    public string action_right = "MC_Action_Right_Clip";
    public string action_left = "MC_Action_Left_Clip";
    public string action_up = "MC_Action_Up_Clip";
    public string action_down = "MC_Action_Down_Clip";

    public float actionLength = 0.4f;

    public bool isAction = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animate = GetComponent<Animator>();
        playerAni = GetComponent<PlayerAnimation>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAtk != null && playerAtk.isAtk)
        {
            return;
        }
        if (playerBlock != null && playerBlock.isBlock)
        {
            return;
        }

        if (!isAction && Input.GetKey(KeyCode.E))
        {
            PerformAction();
        }
    }

    void PerformAction()
    {
        //finds the last direction to play the right action animation that faces the same direction
        switch (playerAni.lastDir)
        {
            case "right":
                animate.Play(action_right);
                break;
            case "left":
                animate.Play(action_left);
                break;
            case "up":
                animate.Play(action_up);
                break;
            case "down":
                animate.Play(action_down);
                break;
        }

        Invoke(nameof(ResetAction), actionLength);
    }

    private void ResetAction()
    {
        isAction = false;
    }

}
