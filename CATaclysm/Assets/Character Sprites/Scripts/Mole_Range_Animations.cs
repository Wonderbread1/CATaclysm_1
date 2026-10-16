using UnityEngine;

public class Mole_Range_Animations : MonoBehaviour
{
    private Transform player_pos;

    [SerializeField] private float trigger_rad = 3.5f;
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float fireRate = 1.5f;
     private Animator animate;
    
    private bool isOut = false;
    private float fireTimer;

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
        if (dis_to_player <= trigger_rad)
        {
            Vector2 direction = (player_pos.position - transform.position).normalized;

            animate.SetFloat("MoveX", direction.x);
            animate.SetFloat("MoveY", direction.y);

            if (!isOut)
            {
                isOut = true;
                animate.SetTrigger("TriggerOut");

                fireTimer = fireRate;
            }

            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0)
            {
                ThrowRock();
                fireTimer = fireRate;
            }

        }
        else
        {
            if (isOut)
            {
                isOut = false;
                animate.SetBool("IsOut", false);
            }
        }

        
    }

    public void ThrowRock()
    {
        if(player_pos == null || rockPrefab == null) return;

        Vector3 spawnPos = throwPoint != null ? throwPoint.transform.position : transform.position;

        Vector2 thrown_Dir = (player_pos.position - spawnPos).normalized;

        GameObject rockObj = Instantiate(rockPrefab, spawnPos, Quaternion.identity);
        Rock rock = rockObj.GetComponent<Rock>();
        if (rock != null)
        {
            rock.Launch(thrown_Dir);
        }
    }

}
