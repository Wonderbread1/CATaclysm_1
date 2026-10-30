
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackAnimations : MonoBehaviour
{
    private Animator animate;
    private PlayerBlock playerBlock;
    private PlayerAction playerAction;

    [Header("Input System")]
    [SerializeField] private InputActionAsset controls;

    private InputAction attackAction;

    [Header("Attack Animations")]
    public string atk_right = "MC_Attack_Right_Clip";
    public string atk_left = "MC_Attack_Left_Clip";
    public string atk_up = "MC_Attack_Up_Clip";
    public string atk_down = "MC_Attack_Down_Clip";

    public float atkLength = 0.4f;

    public bool isAtk = false;

    //Inventory Check
    public InventorySelector inventorySelector;
    public InventoryManager inventoryManager;

    private void Start()
    {
        animate = GetComponent<Animator>();
        playerBlock = GetComponent<PlayerBlock>();
        playerAction = GetComponent<PlayerAction>();

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
            Debug.LogError("Animation Controls asset is not assigned!");
            return;
        }

        if (attackAction != null)
            attackAction.Disable();

        InputActionMap map = controls.FindActionMap(mapName);

        if (map == null)
        {
            Debug.LogError("Animation Action Map not found: " + mapName);
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
        if (playerBlock != null && playerBlock.isBlock)
            return;

        if (playerAction != null && playerAction.isAction)
            return;

        if (isAtk || attackAction == null)
            return;

        if (inventorySelector != null && inventoryManager != null)
        {
            string currentItem = inventoryManager.GetSelectedItemType(inventorySelector.selectedIndex);
            if (currentItem != "Sword")
            {
                return;
            }
        }

        if (!attackAction.WasPressedThisFrame())
            return;

        if (inventorySelector != null && inventoryManager != null)
        {
            string currentItem = inventoryManager.GetSelectedItemType(inventorySelector.selectedIndex);
            if (currentItem != "Sword")
            {
                return;
            }
        }

        Vector2 input = attackAction.ReadValue<Vector2>();

        if (Mathf.Abs(input.y) >= Mathf.Abs(input.x))
        {
            if (input.y > 0f)
                PerformAttack(atk_up);
            else if (input.y < 0f)
                PerformAttack(atk_down);
        }
        else
        {
            if (input.x > 0f)
                PerformAttack(atk_right);
            else if (input.x < 0f)
                PerformAttack(atk_left);
        }
    }

    private void PerformAttack(string animationName)
    {
        isAtk = true;

        animate.Play(animationName, 0, 0f);

        Invoke(nameof(ResetAtk), atkLength);
    }

    private void ResetAtk()
    {
        isAtk = false;
    }
}
