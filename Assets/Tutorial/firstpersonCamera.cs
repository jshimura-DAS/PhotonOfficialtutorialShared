using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform Target;
    public float MouseSensitivity = 10f;

    private float _verticalRotation;
    private float _horizontalRotation;
    private Keyboard keyboard;

    private void Awake()
    {
        keyboard = Keyboard.current;
    }
    private void LateUpdate()
    {
        if (Target == null)
            return;

        transform.position = Target.position;

        float mouseX = (keyboard.dKey.isPressed ? 1 : 0) - (keyboard.aKey.isPressed ? 1 : 0);
        float mouseY = (keyboard.wKey.isPressed ? 1 : 0) - (keyboard.sKey.isPressed ? 1 : 0);

        _verticalRotation -= mouseY * MouseSensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -70f, 70f);

        _horizontalRotation += mouseX * MouseSensitivity;

        transform.rotation = Quaternion.Euler(_verticalRotation, _horizontalRotation, 0);
    }
}
