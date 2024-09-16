using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float moveSpeed = 5f;
    Vector2 moveDirection = Vector2.zero;

    Inputs playerInputs;
    InputAction move;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInputs = new Inputs();
    }

    private void OnEnable()
    {
        move = playerInputs.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(moveDirection.x, 0, moveDirection.y) * moveSpeed;
    }
}
