
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    [Header("Input System")]
    [SerializeField] private InputActionAsset controls;

    private InputActionMap currentActionMap;
    private InputAction moveAction;

    private PlayerAttackAnimations playerAtk;
    private PlayerBlock playerBlock;
    private PlayerAction playerAction;

    private void Start()
    {
        playerAtk = GetComponent<PlayerAttackAnimations>();
        playerBlock = GetComponent<PlayerBlock>();
        playerAction = GetComponent<PlayerAction>();

        SetControlMap("Normal_Keyboard");
    }

    private void OnDisable()
    {
        if (currentActionMap != null)
        {
            currentActionMap.Disable();
        }
    }

    public void SetControlMap(string mapName)
    {
        if (controls == null)
        {
            Debug.LogError("Controls Input Actions asset is not assigned!");
            return;
        }

        if (currentActionMap != null)
        {
            currentActionMap.Disable();
        }

        currentActionMap = controls.FindActionMap(mapName);

        if (currentActionMap == null)
        {
            Debug.LogError("Action Map not found: " + mapName);
            return;
        }

        moveAction = currentActionMap.FindAction("Move");

        if (moveAction == null)
        {
            Debug.LogError("Move action not found in " + mapName);
            return;
        }

        currentActionMap.Enable();

        Debug.Log("Active control map: " + mapName);
    }

    private void Update()
    {
        // Stop movement while attacking.
        if (playerAtk != null && playerAtk.isAtk)
            return;

        // Stop movement while blocking.
        if (playerBlock != null && playerBlock.isBlock)
            return;

        // Stop movement during other actions.
        if (playerAction != null && playerAction.isAction)
            return;

        if (moveAction == null)
            return;

        Vector2 movementInput = moveAction.ReadValue<Vector2>();

        Vector3 direction = new Vector3(
            movementInput.x,
            movementInput.y,
            0f
        ).normalized;

        transform.position += direction * speed * Time.deltaTime;
    }
}
