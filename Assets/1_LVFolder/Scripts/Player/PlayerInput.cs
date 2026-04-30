using Player;
using UnityEngine;

public class PlayerInput : PlayerBase
{
    public Vector3 MoveDirection {  get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        InputDirection();
        InputAttack();
    }

    private void InputDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        //カメラを基準としてプレイヤーの方向を決定
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        MoveDirection = (cameraForward * z + cameraRight * x).normalized;
    }

    private void InputAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Anim.SetTrigger("Attack");
        }
    }
}
