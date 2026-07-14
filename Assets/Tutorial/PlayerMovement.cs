using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    private Vector3 _velocity;
    private bool _jumpPressed;

    private CharacterController _controller;

    public float PlayerSpeed = 2f;

    public float JumpForce = 5f;
    public float GravityValue = -9.81f;

    public Camera Camera;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _jumpPressed = true;
        }
    }

    public override void Spawned()
    {
        if (HasStateAuthority == false)
            return;

        Camera = Camera.main;
        Camera.GetComponent<FirstPersonCamera>().Target = transform;
    }

    public override void FixedUpdateNetwork()
    {
        // FixedUpdateNetwork is only executed on the StateAuthority

        if (_controller.isGrounded)
        {
            _velocity = new Vector3(0, -1, 0);
        }

        // 移動
        var keyboard = Keyboard.current;
        Quaternion cameraRotationY = Quaternion.Euler(0, Camera.transform.rotation.eulerAngles.y, 0);
        var move = new Vector3(
            (keyboard.dKey.isPressed ? 1 : 0) - (keyboard.aKey.isPressed ? 1 : 0),
            0f,
            (keyboard.wKey.isPressed ? 1 : 0) - (keyboard.sKey.isPressed ? 1 : 0)
        );

        move = cameraRotationY * move;

        _velocity.y += GravityValue * Runner.DeltaTime;
        if (_jumpPressed && _controller.isGrounded)
        {
            _velocity.y += JumpForce;
        }
        _controller.Move(move + _velocity*Runner.DeltaTime);
   
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        _jumpPressed = false;
    }
}
