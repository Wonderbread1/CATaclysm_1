using UnityEngine;

public class Mole_Fast : MonoBehaviour
{
    private Animator animate;

    //fast mole animations
    public string run_right = "Mole_F_Right_Clip";
    public string run_left = "Mole_F_Left_Clip";
    public string run_up = "Mole_F_Up_Clip";
    public string run_down = "Mole_F_Down_Clip";

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
