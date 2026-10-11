using UnityEngine;

public class Bat : MonoBehaviour
{
    private Animator animate;
    private Vector3 last_pos;
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animate = GetComponent<Animator>();
        last_pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement_dir = (transform.position - last_pos).normalized;

        if (movement_dir != Vector3.zero)
        {
            //plays the right direction for animations using animator with a blend tree with these pramaters
            animate.SetFloat("MoveX", movement_dir.x);
            animate.SetFloat("MoveY", movement_dir.y);
        }
       
        last_pos = transform.position;
    }
}
