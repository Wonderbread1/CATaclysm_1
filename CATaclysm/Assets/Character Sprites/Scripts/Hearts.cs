using UnityEngine;


public class Hearts : MonoBehaviour
{
    private Animator animate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        animate = GetComponent<Animator>();
    }

    public void SetHeartState(int heart_Status)
    {
        if(animate != null)
        {
            animate.SetInteger("HealthState", heart_Status);
        }
    }

}
