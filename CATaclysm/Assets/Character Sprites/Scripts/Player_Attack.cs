
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player_Attack : MonoBehaviour
{
    public GameObject hitbox;
    public float attack_dur = 0.2f;
    public float hitbox_offset = 1f;

    [Header("Input System")]
    [SerializeField] private InputActionAsset controls;

    private InputAction attackAction;
    private bool is_Atk = false;

    private void Start()
    {
        SetControlMap("Normal_Keyboard");
    }

    private void OnDisable()
    {
        if (attackAction != null)
            attackAction.Disable();
    }

    public void SetControlMap(string mapName)
    {
        if (controls == null)
        {
            Debug.LogError("Attack Controls asset is not assigned!");
            return;
        }

        if (attackAction != null)
            attackAction.Disable();

        InputActionMap map = controls.FindActionMap(mapName);

        if (map == null)
        {
            Debug.LogError("Attack Action Map not found: " + mapName);
            return;
        }

        attackAction = map.FindAction("Attack");

        if (attackAction == null)
        {
            Debug.LogError("Attack action not found in " + mapName);
            return;
        }

        attackAction.Enable();
    }

    private void Update()
    {
        if (is_Atk || attackAction == null)
            return;

        // Only attack when a direction is first pressed.
        if (!attackAction.WasPressedThisFrame())
            return;

        Vector2 input = attackAction.ReadValue<Vector2>();

        if (Mathf.Abs(input.y) >= Mathf.Abs(input.x))
        {
            if (input.y > 0f)
            {
                StartCoroutine(
                    PerformAttack(new Vector3(0f, 1f, 0f))
                );
            }
            else if (input.y < 0f)
            {
                StartCoroutine(
                    PerformAttack(new Vector3(0f, -2f, 0f))
                );
            }
        }
        else
        {
            if (input.x < 0f)
            {
                StartCoroutine(
                    PerformAttack(new Vector3(-1f, 0f, 0f))
                );
            }
            else if (input.x > 0f)
            {
                StartCoroutine(
                    PerformAttack(new Vector3(1f, 0f, 0f))
                );
            }
        }
    }

    private IEnumerator PerformAttack(Vector3 attack_Dir)
    {
        is_Atk = true;

        if (hitbox != null)
        {
            hitbox.transform.position =
                transform.position + (attack_Dir * hitbox_offset);

            hitbox.transform.rotation = Quaternion.identity;
            hitbox.SetActive(true);
        }

        yield return new WaitForSeconds(attack_dur);

        if (hitbox != null)
            hitbox.SetActive(false);

        is_Atk = false;
    }
}
