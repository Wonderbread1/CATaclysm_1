using UnityEngine;

public class Golem : MonoBehaviour
{
    private Animator animate;

    //golem walk animations
    public string walk_right = "Golem_Walk_Right_Clip";
    public string walk_left = "Golem_Walk_Left_Clip";
    public string walk_up = "Golem_Walk_Up_Clip";
    public string walk_down = "Golem_Walk_Down_Clip";

    //golem rising when player character gets close animations
    public string golem_rise_right = "Golem_Rise_Right_Clip";
    public string golem_rise_left = "Golem_Rise_Left_Clip";

    //golem  basic attack animations
    public string attack_right = "Golem_Attack_Right_Clip";
    public string attack_left = "Golem_Attack_Left_Clip";
    public string attack_up = "Golem_Attack_Up_Clip";
    public string attack_down = "Golem_Attack_Down_Clip";

    //golem death animations
    public string death_right = "Golem_Death_Right_Clip";
    public string death_left = "Golem_Death_Left_Clip";

    //roll animations
    public string roll_right = "Golem_Roll_Right_CLip";
    public string roll_left = "Golem_Roll_Left_Clip";

    //ball to golem transformation aniamtions
    public string ball_to_golem_right = "Ball_To_Golem_Right_Clip";
    public string ball_to_golem_left = "Ball_To_Golem_Left_Clip";

    //golem to ball animations for start of roll attack/action
    public string golem_to_ball_right = "Golem_To_Ball_Right_Clip";
    public string golem_to_ball_left = "Golem_To_Ball_Left_Clip";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animate = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
