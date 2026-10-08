using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    private PlayerAttack playerAtk;
    private PlayerBlock playerBlock;
    private PlayerAction playerAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAtk = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, y, 0f).normalized;

        transform.position += direction * speed * Time.deltaTime;

        //stop movement while attacking
        if (playerAtk != null && playerAtk.isAtk)
        {
            return;
        }
        if (playerBlock != null && playerBlock.isBlock)
        {
            return;
        }
        if (playerAction != null && playerAction.isAction)
        {
            return;
        }

    }
}
