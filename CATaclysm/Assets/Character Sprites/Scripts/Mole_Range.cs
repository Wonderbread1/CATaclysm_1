using UnityEngine;

public class Mole_Range : MonoBehaviour
{
    private Animator animate;

    //Ranged mole attack animations
    public string attack_right = "Mole_R_Right_Clip";
    public string attack_left = "Mole_R_Left_Clip";
    public string attack_up = "Mole_R_Up_Clip";
    public string attack_down = "Mole_R_Down_Clip";

    //rising from mole hole
    public string rise_right = "Mole_Hole_Right_CLip";
    public string rise_left = "Mole_Hole_Left_CLip";
    public string rise_up = "Mole_Hole_Up_CLip";
    public string rise_down = "Mole_Hole_Down_CLip";

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
