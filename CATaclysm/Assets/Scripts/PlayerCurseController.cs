
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCurseController : MonoBehaviour
{
    private Player_Movement movement;
    private Player_Attack attack;
    private PlayerAttackAnimations attackAnimations;
    private PlayerAnimation movementAnimations;

    private void Awake()
    {
        movement = GetComponent<Player_Movement>();
        attack = GetComponent<Player_Attack>();
        attackAnimations = GetComponent<PlayerAttackAnimations>();
        movementAnimations = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        // Temporary testing shortcuts
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.f1Key.wasPressedThisFrame)
            SetNormalControls();

        if (Keyboard.current.f2Key.wasPressedThisFrame)
            SetSwappedCurse();

        if (Keyboard.current.f3Key.wasPressedThisFrame)
            SetInvertedCurse();

        if (Keyboard.current.f4Key.wasPressedThisFrame)
            SetChaosCurse();
    }

    public void SetNormalControls()
    {
        ChangeControls("Normal_Keyboard");
    }

    public void SetSwappedCurse()
    {
        ChangeControls("Curse_Swapped_Keyboard");
    }

    public void SetInvertedCurse()
    {
        ChangeControls("Curse_Inverted_Keyboard");
    }

    public void SetChaosCurse()
    {
        ChangeControls("Curse_CHAOS_Keyboard");
    }

    private void ChangeControls(string mapName)
    {
        if (movement != null)
            movement.SetControlMap(mapName);

        if (attack != null)
            attack.SetControlMap(mapName);

        if (attackAnimations != null)
            attackAnimations.SetControlMap(mapName);

        if (movementAnimations != null)
            movementAnimations.SetControlMap(mapName);

        Debug.Log("Controls changed to: " + mapName);
    }
}
