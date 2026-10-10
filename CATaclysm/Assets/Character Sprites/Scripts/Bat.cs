using UnityEngine;

public class Bat : MonoBehaviour
{
    private Animator animate;

    //bat flying animations
    public string fly_right = "Bat_Right_Clip";
    public string fly_left = "Bat_Left_Clip";
    public string fly_up = "Bat_Up_Clip";
    public string fly_down = "Bat_Down_Clip";

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
