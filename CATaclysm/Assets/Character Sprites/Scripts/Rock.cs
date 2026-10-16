
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int damage = 1;

    // ADDED: Random spread when deflecting
    [SerializeField] private float deflectSpread = 10f;

    private Vector2 move_Dir;
    private bool isReflected = false;
    private Rigidbody2D rb;


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
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction)
    {
        move_Dir = direction;
        if (rb != null)
        {
            rb.linearVelocity = move_Dir * speed;

        }
        //causes rock to be stroyed from scene after a few seconds
        Destroy(gameObject, lifetime);
    }

    public void Reflect(Vector2 new_Dir)
    {
        isReflected = true;


        if (rb != null)
        {
            rb.linearVelocity = new_Dir.normalized * (speed * 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ADDED: Reflected rocks activate levers
        if (isReflected)
        {
            LeverSwitch lever = collision.GetComponentInParent<LeverSwitch>();

            if (lever != null)
            {
                lever.TakeHit();
                Destroy(gameObject);
                return;
            }
        }

        // CHANGED: Recognize the player's child hitbox
        if (!isReflected &&
            collision.GetComponentInParent<PlayerBlock>() != null)
        {

            // CHANGED: Find PlayerBlock on the parent
            PlayerBlock playerBlock = collision.GetComponentInParent<PlayerBlock>();

            if (playerBlock != null && playerBlock.isBlock)
            {
                // ADDED: Deflect toward the player's facing direction
                PlayerAnimation playerAni = collision.GetComponentInParent<PlayerAnimation>();

                Vector2 return_Dir = -move_Dir.normalized;

                if (playerAni != null)
                {
                    switch (playerAni.lastDir)
                    {
                        case "right":
                            return_Dir = Vector2.right;
                            break;
                        case "left":
                            return_Dir = Vector2.left;
                            break;
                        case "up":
                            return_Dir = Vector2.up;
                            break;
                        case "down":
                            return_Dir = Vector2.down;
                            break;
                    }
                }

                // ADDED: Slight random spread
                float randomAngle = Random.Range(-deflectSpread, deflectSpread);
                return_Dir = Quaternion.Euler(0f, 0f, randomAngle) * return_Dir;

                Reflect(return_Dir);
            }
            else
            {
                // CHANGED: Find Health on the parent
                Health playerHealth = collision.GetComponentInParent<Health>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }

                Destroy(gameObject);
            }
        }
        else if (isReflected && collision.CompareTag("Enemy"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

}
