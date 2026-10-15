using UnityEngine;

public class BatFollow : MonoBehaviour
{

    private Transform player_pos;

    //movement speeds
    [SerializeField] private float speed = 3f;    
    [SerializeField] private float followThrough_speed = 1.5f;

    //attack behavior for what distance each is done
    [SerializeField] private float attack_dis = 1.2f;
    [SerializeField] private float followThrough_dis = 2.5f;

    private enum State {Chasing, FollowThrough, Retreat }
    private State curr_state = State.Chasing;

    private Vector2 follow_through;
    private Vector2 retreat_from_target;

    [SerializeField] private float damage = 1f;

    private bool has_Atk = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //finds player character using tags
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player_pos = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player_pos == null) return;
        

        switch (curr_state)
        {
            case State.Chasing:

                //movement towards player
                transform.position = Vector2.MoveTowards(transform.position, player_pos.position, speed * Time.deltaTime);

                //find how close to player
                float dis_to_player = Vector2.Distance(transform.position, player_pos.position);
                if (dis_to_player <= attack_dis)
                {
                    Vector2 dir_to_player = (player_pos.position - transform.position).normalized;
                    follow_through = (Vector2)player_pos.position + (dir_to_player * followThrough_dis);

                    curr_state = State.FollowThrough;
                }

                if (player_pos != null && curr_state != State.Retreat)
                {
                    float dis_player = Vector2.Distance(transform.position, player_pos.position);
                    if(dis_player < 0.2f)
                    {
                        if (!has_Atk)
                        {
                            Debug.Log("Bat touched player!");
                            Health playerHealth = player_pos.GetComponent<Health>();
                            if (playerHealth != null)
                            {
                                playerHealth.TakeDamage(damage);
                                has_Atk = true;
                            }
                        }
                    }
                        
                }
                break;

            case State.FollowThrough:

                //location past player
                transform.position = Vector2.MoveTowards(transform.position, follow_through, followThrough_speed * Time.deltaTime);

                //swtich back to following player
                if (Vector2.Distance(transform.position, follow_through) < 0.1f)
                {
                    curr_state = State.Chasing;
                    has_Atk = false; 
                }
                break;

            case State.Retreat:
                transform.position = Vector2.MoveTowards(transform.position, retreat_from_target, speed * 1.6f * Time.deltaTime);

                if (Vector2.Distance(transform.position, retreat_from_target) < 0.1f)
                {
                    curr_state = State.Chasing;
                }
                break;

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player_Attack") && curr_state != State.Retreat)
        {
            Debug.Log("bat was hit");

            Vector2 return_Dir = (transform.position - player_pos.position).normalized;
            retreat_from_target = (Vector2)transform.position + (return_Dir * 1.6f);

            curr_state = State.Retreat;

            has_Atk = false;
        }
    }
    

}
