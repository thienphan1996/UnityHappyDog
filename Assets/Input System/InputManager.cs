using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerActionMap playerActionMap;
    private Vector2 moveInput;
    public Vector2 MoveInput => moveInput;

    private bool isJumping;
    public bool IsJumping => isJumping;

    private bool isFire;
    public bool IsFire => isFire;

    private void Awake() => playerActionMap = new PlayerActionMap();
    private void OnEnable() => playerActionMap.Enable();
    private void OnDisable() => playerActionMap.Disable();

    private void Update()
    {
        moveInput = playerActionMap.Player.Move.ReadValue<Vector2>();

        isJumping = playerActionMap.Player.Jump.triggered;

        isFire = playerActionMap.Player.Fire.triggered;
    }
}
