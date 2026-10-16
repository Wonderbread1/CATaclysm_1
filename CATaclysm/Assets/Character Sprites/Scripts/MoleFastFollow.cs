
using UnityEngine;

public class MoleFastFollow : MonoBehaviour
{

    private Transform player_pos;

    //movement speeds
    [SerializeField] private float speed = 3f;
    [SerializeField] private float followThrough_speed = 1.5f;

    //attack behavior for what distance each is done
    [SerializeField] private float attack_dis = 1.2f;
    [SerializeField] private float followThrough_dis = 2.5f;

    private enum State { Chasing, FollowThrough, Retreat }
    private State curr_state = State.Chasing;

    private Vector2 follow_through;
    private Vector2 retreat_from_target;

    [SerializeField] private float damage = 1f;

    private bool has_Atk = false;

    // ADDED: Rigidbody movement and obstacle detection
    private Rigidbody2D rb;

    [SerializeField] private LayerMask obstacleLayers;
    [SerializeField] private float detectionDistance = 0.8f;
    [SerializeField] private float probeRadius = 0.25f;

    // ADDED: Remember steering direction to avoid switching sides
    private int steeringSide = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ADDED
        rb = GetComponent<Rigidbody2D>();

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
                // CHANGED: Use physics-based movement with obstacle avoidance
                MoveAroundObstacles(player_pos.position, speed);

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
                    if (dis_player < 0.2f)
                    {
                        if (!has_Atk)
                        {
                            Debug.Log("Bat touched player!");

                            PlayerBlock playerBlock = player_pos.GetComponent<PlayerBlock>();

                            bool isBlock = (playerBlock != null && playerBlock.isBlock);

                            if (isBlock)
                            {
                                Debug.Log("Blocked Bat");

                                //Retreat from block movement 
                                Vector2 return_Dir = (transform.position - player_pos.position).normalized;
                                retreat_from_target = (Vector2)transform.position + (return_Dir * 1.6f);
                                curr_state = State.Retreat;

                            }
                            else
                            {
                                Health playerHealth = player_pos.GetComponent<Health>();
                                if (playerHealth != null)
                                {
                                    playerHealth.TakeDamage(damage);

                                }
                            }
                            has_Atk = true;
                        }
                    }

                }
                break;

            case State.FollowThrough:

                //location past player
                // CHANGED: Use physics-based movement with obstacle avoidance
                MoveAroundObstacles(follow_through, followThrough_speed);

                //swtich back to following player
                if (Vector2.Distance(transform.position, follow_through) < 0.1f)
                {
                    curr_state = State.Chasing;
                    has_Atk = false;
                }
                break;

            case State.Retreat:
                // CHANGED: Use physics-based movement with obstacle avoidance
                MoveAroundObstacles(retreat_from_target, speed * 1.6f);

                if (Vector2.Distance(transform.position, retreat_from_target) < 0.1f)
                {
                    curr_state = State.Chasing;
                    has_Atk = false;
                }
                break;

        }

    }

    // ADDED: Move using Rigidbody2D and steer around obstacles
    private void MoveAroundObstacles(Vector2 target, float moveSpeed)
    {
        if (rb == null) return;

        Vector2 direction = (target - rb.position).normalized;

        if (direction == Vector2.zero) return;

        Vector2 moveDirection = FindOpenDirection(direction);

        rb.MovePosition(
            rb.position + moveDirection * moveSpeed * Time.deltaTime
        );
    }

    // ADDED: Find a direction that avoids walls and rocks
    private Vector2 FindOpenDirection(Vector2 desired)
    {
        if (IsPathClear(desired))
            return desired;

        float[] angles = { 30f, 60f, 90f, 120f, 150f, 180f };

        foreach (float angle in angles)
        {
            Vector2 alternative =
                Quaternion.Euler(0f, 0f, angle * steeringSide) * desired;

            if (IsPathClear(alternative))
                return alternative.normalized;
        }

        steeringSide *= -1;

        foreach (float angle in angles)
        {
            Vector2 alternative =
                Quaternion.Euler(0f, 0f, angle * steeringSide) * desired;

            if (IsPathClear(alternative))
                return alternative.normalized;
        }

        return Vector2.zero;
    }

    // ADDED: Check whether an obstacle blocks movement
    private bool IsPathClear(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.CircleCast(
            rb.position,
            probeRadius,
            direction,
            detectionDistance,
            obstacleLayers
        );

        return hit.collider == null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player_Attack") && curr_state != State.Retreat)
        {
            Debug.Log("bat was hit");

            Vector2 return_Dir = (transform.position - player_pos.position).normalized;
            retreat_from_target = (Vector2)transform.position + (return_Dir * 1.6f);

            curr_state = State.Retreat;

            has_Atk = false;
        }
    }


}
