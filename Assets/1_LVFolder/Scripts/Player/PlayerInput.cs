using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3 MoveDirection {  get; private set; }

    private void Update()
    {
        InputDirection();
    }

    private void InputDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        MoveDirection = (cameraForward * z + cameraRight * x).normalized;
    }
}
