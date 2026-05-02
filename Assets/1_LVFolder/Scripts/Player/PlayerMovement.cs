using Player;
using System;
using UnityEngine;

public class PlayerMovement : PlayerBase
{
    [SerializeField] private float _checkRadius = 1.0f;
    [SerializeField] private LayerMask _groundLayer;

    private PlayerInput _input;

    protected override void Awake()
    {
        base.Awake();
        _input = GetComponent<PlayerInput>();
    }

    void FixedUpdate()
    {
        if (IsGrounded())
        {
            // 弱い重力
            Rb.AddForce(Vector3.down * Core.PlayerData.GroundGravity, ForceMode.Acceleration);
        }
        else
        {
            // 強めの重力
            Rb.AddForce(Vector3.down * Core.PlayerData.FallGravity, ForceMode.Acceleration);
        }

        // 今攻撃ステートが発動中ならreturnする
        if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            return;
        }
        PerformMove();
    }

    private bool IsGrounded()
    {
        Vector3 spherePos = transform.position + Vector3.up * 0.05f;     //少し上にあげる
        return Physics.CheckSphere(spherePos, _checkRadius, _groundLayer);
    }

    private void PerformMove()
    {
        // Y速度を保存
        float currentYVelocity = Rb.linearVelocity.y;

        Vector3 targetVelocity = _input.MoveDirection * Core.PlayerData.MoveSpeed;

        // ここで保存したYを使う　重力と競合しないようにするため
        Rb.linearVelocity = new Vector3(targetVelocity.x, currentYVelocity, targetVelocity.z);

        //移動入力があれば
        if (_input.MoveDirection.sqrMagnitude > 0.01f)
        {
            var targetRotation = Quaternion.LookRotation(_input.MoveDirection); //目標の回転
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Core.PlayerData.TurningSpeed);    //今の回転から目標の回転にゆっくり向かう

            Anim.SetBool(IsWalkingHash, true);
        }
        else
        {
            Anim.SetBool(IsWalkingHash, false);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Vector3 spherePos = transform.position + Vector3.up * 0.05f;

        bool isGrounded = IsGrounded();
        Gizmos.color = isGrounded ? Color.green : Color.red;

        Gizmos.DrawWireSphere(spherePos, _checkRadius);
    }
}
