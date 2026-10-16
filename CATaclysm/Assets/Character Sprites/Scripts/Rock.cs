using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int damage = 1;

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
        

        if(rb != null)
        {
            rb.linearVelocity = new_Dir.normalized * (speed * 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isReflected && collision.CompareTag("Player"))
        {

            PlayerBlock playerBlock = collision.GetComponent<PlayerBlock>();
            if (playerBlock != null && playerBlock.isBlock)
            {
                GameObject mole = GameObject.FindGameObjectWithTag("Enemy");
                if (mole != null)
                {
                    Vector2 return_Dir = (mole.transform.position - transform.position).normalized;
                    Reflect(return_Dir);
                }
            }
            else
            {
                Health playerHealth = collision.GetComponent<Health>();
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
