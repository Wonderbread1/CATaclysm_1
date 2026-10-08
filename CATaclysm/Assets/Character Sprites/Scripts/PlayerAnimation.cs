using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animate;
    private PlayerAttack playerAtk;
    private PlayerBlock playerBlock;
    private PlayerAction playerAction;

    //for walk cycle animations
    public string walk_left = "MC_Walk_Left_Clip";
    public string walk_right = "MC_Walk_Right_Clip";
    public string walk_up = "MC_Walk_Up_Clip";
    public string walk_down = "MC_Walk_Down_Clip";

    //for idle stances when not moving
    public string idle_left = "MC_Idle_Left_Clip";
    public string idle_right = "MC_Idle_Right_Clip";
    public string idle_up = "MC_Idle_Up_Clip";
    public string idle_down = "MC_Idle_Down_Clip";

    //track last direction
    public string lastDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animate = GetComponent<Animator>();
        playerAtk = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        //stops movement animations
        if (playerAtk != null && playerAtk.isAtk)
        {
            return;
        }
        if (playerBlock != null && playerBlock.isBlock)
        {
            return;
        }
        if (playerAction != null && playerAction.isAction)
        {
            return;
        }

        //animation plays when that key is pressed down
        if (Input.GetKey(KeyCode.D))
        {
            animate.Play(walk_right);
            lastDir = "right";
        }
        else if (Input.GetKey(KeyCode.W))
        {
            animate.Play(walk_up);
            lastDir = "up";
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animate.Play(walk_down);
            lastDir = "down";
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animate.Play(walk_left);
            lastDir = "left";
        }
        else
        {
            //keep last direction so character stays facing that way
            switch (lastDir)
            {
                case "right":
                    animate.Play(idle_right);
                    break;
                case "left":
                    animate.Play(idle_left);
                    break;
                case "up":
                    animate.Play(idle_up);
                    break;
                case "down":
                    animate.Play(idle_down);
                    break;
            }
        }
    }
}
