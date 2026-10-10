using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Animator animate;

    //player character death animations
    public string death_right = "MC_Death_Right_CLip";
    public string death_left = "MC_Death_Left_CLip";
    public string death_left_up = "MC_Death_Left_Up_CLip";
    public string death_right_down = "MC_Death_Right_Down_CLip";

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
