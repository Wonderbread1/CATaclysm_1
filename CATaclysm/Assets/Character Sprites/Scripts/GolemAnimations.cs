using UnityEngine;

public class GolemAnimations : MonoBehaviour
{
    [SerializeField] private float trigger_rad = 3.5f;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float delay = 1f;
    private float timer;
    private Transform player_pos;
    private Animator animate;

    private bool isOut = false;

    [SerializeField] private float atk_rad = 0.1f;
    [SerializeField] private float atk_cooldown = 2f;
    private float atk_timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player_pos = player.transform;
        }
        if (animate == null)
        {
            animate = GetComponent<Animator>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (player_pos == null) return;

        float dis_to_player = Vector2.Distance(transform.position, player_pos.position);
        if (dis_to_player <= atk_rad)
        {
            if(atk_timer <= 0f)
            {
                Attack();
                atk_timer = atk_cooldown;
            }
        }

        else if (dis_to_player <= trigger_rad)
        {
            Vector2 direction = (player_pos.position - transform.position).normalized;

            animate.SetFloat("MoveX", direction.x);
            animate.SetFloat("MoveY", direction.y);

            

            if (!isOut)
            {
                isOut = true;
                animate.SetTrigger("TriggerOut");
                timer = delay;

            }
            else
            {
                if(timer > 0f)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, player_pos.position, speed * Time.deltaTime);
                }
                    
            }

            

        }
        else
        {
            if (isOut)
            {
                isOut = false;
                
            }
        }
        
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }

        if(atk_timer > 0f)
        {
            atk_timer -= Time.deltaTime;
        }
        
            
        
    }

    void Attack()
    {
        animate.SetTrigger("Attack");

        if (player_pos != null)
        {
            Health playerHealth = player_pos.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(2f);
            }
        }
    }
}
