using Player;
using System;
using System.Collections;
using UnityEngine;

public class PlayerInput : PlayerBase
{
    private int _attackCount = 0;
    private float _comboTimer = 0.0f;

    public Vector3 MoveDirection { get; private set; }

    public int AttackCount => _attackCount;

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
            if (Input.GetKey(KeyCode.LeftShift) && Core.PermissionSpecialAttack)
            {
                StartCoroutine(InputSpecialAttack());
            }
            else
            {
                InputAttack();
            }
        }

        // コンボ継続中なら
        if (_attackCount > 0)
        {
            _comboTimer += Time.deltaTime;

            // 時間切れでコンボ終了
            if (_comboTimer >= Core.PlayerData.AttackComboTime)
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

        Vector3 cameraRight = Camera.main.transform.right;
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        MoveDirection = (cameraRight * x + cameraForward * z).normalized;
    }

    private void InputAttack()
    {
        // 構造体ごと TryAttack に渡すように変更
        if (_attackCount == 0)
        {
            TryAttack(AttackSpeedHash, Core.PlayerData.Attack1, AttackTriggerHash);
        }
        else if (_attackCount == 1)
        {
            TryAttack(Attack2SpeedHash, Core.PlayerData.Attack2, Attack2TriggerHash);
        }
        else if (_attackCount == 2)
        {
            TryAttack(Attack3SpeedHash, Core.PlayerData.Attack3, Attack3TriggerHash);
        }
        else if (_attackCount == 3)
        {
            TryAttack(Attack4SpeedHash, Core.PlayerData.Attack4, Attack4TriggerHash);
        }
    }

    private IEnumerator InputSpecialAttack()
    {
        // ゲージリセット
        Core.PermissionSpecialAttack = false;
        Core.SpecialGage = 0;   
        Core.UpdateUI();

        // スペシャル攻撃も構造体を参照
        Anim.SetFloat(AttackSPSpeedHash, Core.PlayerData.AttackSP.Speed);
        Anim.SetTrigger(AttackSPTriggerHash);

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(PlayerAttackHandler.DashAttack(Core.PlayerData.AttackSP.DashDistance));
    }

    private void TryAttack(int speedHash, PlayerData.AttackParam param, int triggerHash)
    {
        if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            return;
        }

        // ダッシュ距離を構造体から取得
        StartCoroutine(PlayerAttackHandler.DashAttack(param.DashDistance));

        if (_attackCount == 0)
        {
            _attackCount++;
            SEManager.Instance.PlaySE_Attack1_2();
        }
        else if (_attackCount == 1)
        {
            _attackCount++;
            SEManager.Instance.PlaySE_Attack1_2();
        }
        else if (_attackCount == 2)
        {
            _attackCount++;
            SEManager.Instance.PlaySE_Attack3();
        }
        else
        {
            _attackCount = 0;
            SEManager.Instance.PlaySE_Attack3();
        }

        // アニメーション速度も構造体から取得
        Anim.SetFloat(speedHash, param.Speed);
        Anim.SetTrigger(triggerHash);

        _comboTimer = 0;
    }

   
}