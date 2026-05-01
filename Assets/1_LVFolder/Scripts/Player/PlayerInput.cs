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
        if (Core.IsStunned)
        {
            MoveDirection = Vector3.zero;
            return;
        }

        InputMoveDirection();
        InputAttack();
    }

    private void InputMoveDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // カメラを基準にして、方向を決定する
        Vector3 cameraRight = Camera.main.transform.right;
        Vector3 cameraForward = Camera.main.transform.forward;
        MoveDirection = (cameraRight * x + cameraForward * z).normalized;
    }

    private void InputAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 今攻撃ステートが発動中ならreturnする
            if (Anim.GetCurrentAnimatorStateInfo(0).IsName(AttackStateName))
            {
                return;     
            }

            Anim.SetFloat(AnimAttackSpeed, Core.PlayerData.AttackSpeed);
            Anim.SetTrigger(AnimAttackTrigger);
        }
    }
}
