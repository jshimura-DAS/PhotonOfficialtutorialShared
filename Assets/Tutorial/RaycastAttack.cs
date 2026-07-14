using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastAttack : NetworkBehaviour
{
    public float Damage = 10;
    public PlayerMovement PlayerMovement;

    private void Update()
    {
        if (HasStateAuthority == false)
            return;

        Ray ray = PlayerMovement.Camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        ray.origin += PlayerMovement.Camera.transform.forward;

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Debug.Log($"RaycastAttack: {ray.origin} -> {ray.direction}");
            Debug.DrawRay(ray.origin, ray.direction, Color.red, 1.0f);
            if (Physics.Raycast(ray.origin, ray.direction, out var hit))
            {
                if (hit.transform.TryGetComponent<Health>(out var health))
                {
                    health.DealDamageRpc(Damage);
                }
            }
        }
    }
}
