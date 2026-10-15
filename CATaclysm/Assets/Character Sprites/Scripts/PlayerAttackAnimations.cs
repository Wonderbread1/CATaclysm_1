using UnityEngine;

public class PlayerAttackAnimations : MonoBehaviour
{
    private Animator animate;
    private PlayerBlock playerBlock;
    private PlayerAction playerAction;
    private PlayerAttackAnimations playerAtk;

    

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
        playerBlock = GetComponent<PlayerBlock>();
        playerAction = GetComponent<PlayerAction>();
        playerAtk = GetComponent<PlayerAttackAnimations>();

        
       
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


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
           PerformAttack(atk_right);
            
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PerformAttack(atk_up);
           
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PerformAttack(atk_down);
            
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PerformAttack(atk_left);
            
        }

    }

    void PerformAttack(string animationName)
    {
        isAtk = true;

        
       

        animate.Play(animationName, 0, 0f);

        Invoke(nameof(ResetAtk), atkLength);

        

    }

    private void ResetAtk()
    {
        isAtk = false;

       
    }
    

    
}
