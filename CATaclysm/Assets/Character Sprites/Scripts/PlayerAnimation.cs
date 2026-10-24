
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animate;
    private PlayerAttackAnimations playerAtk;
    private PlayerBlock playerBlock;
    private PlayerAction playerAction;

    private Player_Movement playerMovement;

    // Walk cycle animations
    public string walk_left = "MC_Walk_Left_Clip";
    public string walk_right = "MC_Walk_Right_Clip";
    public string walk_up = "MC_Walk_Up_Clip";
    public string walk_down = "MC_Walk_Down_Clip";

    // Idle stance animations
    public string idle_left = "MC_Idle_Left_Clip";
    public string idle_right = "MC_Idle_Right_Clip";
    public string idle_up = "MC_Idle_Up_Clip";
    public string idle_down = "MC_Idle_Down_Clip";

    // Track last direction
    public string lastDir;

    [Header("Input System")]
    [SerializeField] private InputActionAsset controls;

    private InputAction moveAction;

    private void Start()
    {
        animate = GetComponent<Animator>();
        playerAtk = GetComponent<PlayerAttackAnimations>();
        playerBlock = GetComponent<PlayerBlock>();
        playerAction = GetComponent<PlayerAction>();
        playerMovement = GetComponent<Player_Movement>();

        SetControlMap("Normal_Keyboard");
    }

    private void OnDisable()
    {
        if (moveAction != null)
            moveAction.Disable();
    }

    public void SetControlMap(string mapName)
    {
        if (controls == null)
        {
            Debug.LogError("Animation Controls asset is not assigned!");
            return;
        }

        InputActionMap map = controls.FindActionMap(mapName);

        if (map == null)
        {
            Debug.LogError("Animation Action Map not found: " + mapName);
            return;
        }

        moveAction = map.FindAction("Move");

        if (moveAction == null)
        {
            Debug.LogError("Move action not found in " + mapName);
            return;
        }

        moveAction.Enable();
    }

    private void Update()
    {
        if (playerAtk != null && playerAtk.isAtk)
            return;

        if (playerBlock != null && playerBlock.isBlock)
            return;

        if (playerAction != null && playerAction.isAction)
            return;

        if (moveAction == null)
            return;

        Vector2 input = moveAction.ReadValue<Vector2>();

        if (input.x > 0.1f)
        {
            animate.Play(walk_right);
            lastDir = "right";
        }
        else if (input.y > 0.1f)
        {
            animate.Play(walk_up);
            lastDir = "up";
        }
        else if (input.y < -0.1f)
        {
            animate.Play(walk_down);
            lastDir = "down";
        }
        else if (input.x < -0.1f)
        {
            animate.Play(walk_left);
            lastDir = "left";
        }
        else
        {
            switch (lastDir)
            {
                case "right":
                    animate.Play(idle_right);
                    break;
                case "left":
                    animate.Play(idle_left);
                    break;
                case "up":
                    animate.Play(idle_up);
                    break;
                case "down":
                    animate.Play(idle_down);
                    break;
            }
        }
    }
}
