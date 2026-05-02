using Player;
using System;
using System.Collections;
using UnityEngine;

public class PlayerInput : PlayerBase
{
    private int _attackCount = 0;
    private float _comboTimer = 0.0f;

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

        if (Input.GetMouseButtonDown(0))
        {
            InputAttack();
        }

        // コンボ継続中なら
        if(_attackCount > 0)
        {
            _comboTimer += Time.deltaTime;      //タイマーで時間切れを計る

            // 時間切れでコンボ終了
            if(_comboTimer >= Core.PlayerData.AttackComboTime)
            {
                _attackCount = 0;
                _comboTimer = 0.0f;
            }
        }
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
        if (_attackCount == 0)  // 通常攻撃
        {
            TryAttack(AttackSpeedHash, Core.PlayerData.AttackSpeed, AttackTriggerHash);
        }
        else if (_attackCount == 1) //斬り上げ攻撃
        {
            TryAttack( Attack2SpeedHash, Core.PlayerData.Attack2Speed, Attack2TriggerHash);
        }
        else if (_attackCount == 2) //つき攻撃
        {
            TryAttack(Attack3SpeedHash, Core.PlayerData.Attack3Speed, Attack3TriggerHash);
        }
        else if (_attackCount == 3) //回転攻撃
        {
            TryAttack(Attack4SpeedHash, Core.PlayerData.Attack4Speed, Attack4TriggerHash);
        }
    }

    private void TryAttack(int speedHash, float speedValue, int triggerHash)
    {
        // 今攻撃ステートが発動中ならreturnする
        if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            return;
        }

        if (_attackCount == 0)
        {
            _attackCount++;
            StartCoroutine(PlayerAttackHandler.DashAttack(Core.PlayerData.AttackDashDistance));
            SEManager.Instance.PlaySE_Attack1_2();
        }
        else if (_attackCount == 1)
        {
            _attackCount++;
            StartCoroutine(PlayerAttackHandler.DashAttack(Core.PlayerData.Attack2DashDistance));
            SEManager.Instance.PlaySE_Attack1_2();
        }
        else if(_attackCount == 2)
        {
            _attackCount++;
            StartCoroutine(PlayerAttackHandler.DashAttack(Core.PlayerData.Attack3DashDistance));
            SEManager.Instance.PlaySE_Attack3();
        }
        else
        {
            _attackCount = 0;
            StartCoroutine(PlayerAttackHandler.DashAttack(Core.PlayerData.Attack4DashDistance));
            SEManager.Instance.PlaySE_Attack3();
        }

        Anim.SetFloat(speedHash, speedValue);
        Anim.SetTrigger(triggerHash);

        _comboTimer = 0;
    }

}
