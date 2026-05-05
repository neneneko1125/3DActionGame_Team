using Player;
using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerInput : PlayerBase
    {
        public int AttackCount = 0;
        private float _comboTimer = 0.0f;

        public Vector3 MoveDirection { get; private set; }

        

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
            if (AttackCount > 0)
            {
                _comboTimer += Time.deltaTime;

                // 時間切れでコンボ終了
                if (_comboTimer >= Core.PlayerData.AttackComboTime)
                {
                    AttackCount = 0;
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
            // _attackCountの値に応じて実行する
            switch (AttackCount)
            {
                case 0:
                    TryAttack(AttackSpeedHash, Core.PlayerData.Attack1, AttackTriggerHash);
                    break;
                case 1:
                    TryAttack(Attack2SpeedHash, Core.PlayerData.Attack2, Attack2TriggerHash);
                    break;
                case 2:
                    TryAttack(Attack3SpeedHash, Core.PlayerData.Attack3, Attack3TriggerHash);
                    break;
                case 3:
                    TryAttack(Attack4SpeedHash, Core.PlayerData.Attack4, Attack4TriggerHash);
                    break;
            }
        }

        private IEnumerator InputSpecialAttack()
        {
            // ゲージのリセット
            Core.PermissionSpecialAttack = false;
            Core.SpecialGage = 0;
            Core.UpdateUI();

            // アニメーション再生
            Anim.SetFloat(AttackSpecialSpeedHash, Core.PlayerData.AttackSpecial.Speed);
            Anim.SetTrigger(AttackSpecialTriggerHash);

            yield return new WaitForSeconds(0.5f);  // ちょっと待機

            // ここで実際に攻撃
            StartCoroutine(PlayerAttackHandler.StepForward(Core.PlayerData.AttackSpecial.DashDistance));
        }

        private void TryAttack(int speedHash, PlayerData.AttackParam param, int triggerHash)
        {
            if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                return;
            }

            // ダッシュ距離を構造体から取得
            StartCoroutine(PlayerAttackHandler.StepForward(param.DashDistance));

            // 攻撃の種類によってSEを決める　コンボ数も操作する
            switch (AttackCount)
            {
                case 0:
                case 1:
                    AttackCount++;
                    SEManager.Instance.PlaySE_Attack1_2();
                    break;
                case 2:
                    AttackCount++;
                    SEManager.Instance.PlaySE_Attack3();
                    break;
                default: // 3段目（最後）の時
                    AttackCount = 0;
                    SEManager.Instance.PlaySE_Attack3();
                    break;
            }

            // アニメーション速度も構造体から取得
            Anim.SetFloat(speedHash, param.Speed);
            Anim.SetTrigger(triggerHash);

            _comboTimer = 0;
        }
    }
}
