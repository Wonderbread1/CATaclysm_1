using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animate;
    private PlayerAnimation playerAni;
    private PlayerBlock playerBlock;
    private PlayerAction playerAction;
    private PlayerAttack playerAtk;
    

    //Get animation for attack
    public string atk_right = "MC_Attack_Right_Clip";
    public string atk_left = "MC_Attack_Left_Clip";
    public string atk_up = "MC_Attack_Up_Clip";
    public string atk_down = "MC_Attack_Down_Clip";

    public float atkLength = 0.4f;

    public bool isAtk = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animate = GetComponent<Animator>();
        playerAni = GetComponent<PlayerAnimation>();
        playerBlock = GetComponent<PlayerBlock>();
        playerAction = GetComponent<PlayerAction>();
        playerAtk = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerBlock != null && playerBlock.isBlock)
        {
            return;
        }
        if (playerAction != null && playerAction.isAction)
        {
            return;
        }
        if (playerAtk != null && playerAtk.isAtk)
        {
            return;
        }
       

        if (!isAtk && Input.GetMouseButton(0))
        {
            PerformAttack();
        }
    }

    void PerformAttack()
    {
        isAtk = true;

        string statePlay = "";

        //finds the last direction to play the right attack animation that faces the same direction
        switch (playerAni.lastDir)
        {
            case "right":
                statePlay = atk_right;
                break;
            case "left":
                statePlay = atk_left;
                break;
            case "up":
                statePlay = atk_up;
                break;
            case "down":
                statePlay = atk_down;
                break;
        }

        animate.Play(statePlay, 0, 0f);

        Invoke(nameof(ResetAtk), atkLength);


    }

    private void ResetAtk()
    {
       isAtk = false;
    }


}
