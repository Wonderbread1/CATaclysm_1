using UnityEngine;
using System.Collections;

public class Player_Attack : MonoBehaviour
{
    public GameObject hitbox;
    public float attack_dur = 0.2f;
    public float hitbox_offset = 1f;

    private bool is_Atk = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (is_Atk) return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(PerformAttack(new Vector3(0f, 1f, 0f)));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(PerformAttack(new Vector3(0f, -2f, 0f)));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(PerformAttack(new Vector3(-1f, 0f, 0f)));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(PerformAttack(new Vector3(1f, 0f, 0f)));
        }
    }

    IEnumerator PerformAttack(Vector3 attack_Dir)
    {
        is_Atk = true;

        if(hitbox != null)
        {
            hitbox.transform.position = transform.position + (attack_Dir * hitbox_offset);
            hitbox.transform.rotation = Quaternion.identity;

            hitbox.SetActive(true);
        }

        yield return new WaitForSeconds(attack_dur);

        if (hitbox != null)
        {
            hitbox.SetActive(false);
        }

        is_Atk = false;
    }
}
