using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform Target;
    public float MouseSensitivity = 10f;

    private float _verticalRotation;
    private float _horizontalRotation = 0.0f;
    private Keyboard keyboard;

    private void Awake()
    {
        keyboard = Keyboard.current;
    }
    private void LateUpdate()
    {
        if (Target == null)
            return;
        keyboard = Keyboard.current;
        transform.position = Target.position;

        // マウスの移動量を Vector2 で直接取得
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x;
        float mouseY = mouseDelta.y;

        _verticalRotation -= mouseY * MouseSensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -70f, 70f);

        _horizontalRotation += mouseX * MouseSensitivity;

        transform.rotation = Quaternion.Euler(_verticalRotation, _horizontalRotation, 0);
    }
}
