using Enemy;
using Player;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Player
{
    public class PlayerAttackHandler : PlayerBase
    {
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private LayerMask _enemyLayer;

        [Header("カメラの振動関連")]
        [SerializeField] private float duration;
        [SerializeField] private float strength;
        [SerializeField] private float vibrato;

        [Header("エフェクト")]
        [SerializeField] private GameObject _effectPrefab;
        [SerializeField] private GameObject _effectPrefabSpecial;

        protected override void Awake()
        {
            base.Awake();
        }

        // アニメーションのイベントから呼び出される
        // 引数によって、元々の攻撃力を基準として与えるダメージを変更することもできる
        public void OnHit(int powerPercent)
        {
            // 現在の攻撃パラメータを取得
            PlayerData.AttackParam currentParam = GetCurrentAttackParam(powerPercent);

            // 攻撃判定を行い、当たった敵を処理する
            PerformAttackDetection(currentParam.Range, powerPercent);
        }

        private PlayerData.AttackParam GetCurrentAttackParam(int powerPercent)
        {
            if(powerPercent >= Core.PlayerData.SpecialAttackThreshold)    // SP攻撃
            {
                return Core.PlayerData.AttackSpecial;
            }

            PlayerData.AttackParam currentParam;
            
            switch (PlayerInput.AttackCount)    // 通常攻撃なら現在のコンボ数を見る
            {
                case 1: currentParam = Core.PlayerData.Attack1; break;
                case 2: currentParam = Core.PlayerData.Attack2; break;
                case 3: currentParam = Core.PlayerData.Attack3; break;
                case 0: currentParam = Core.PlayerData.Attack4; break;
                default: currentParam = Core.PlayerData.Attack1; break;
            }
            return currentParam;
        }

        private void PerformAttackDetection(float range, int powerPercent)
        {   
            // 攻撃判定を出す
            Collider[] hits = Physics.OverlapSphere(_attackPoint.position, range, _enemyLayer);
            foreach (var h in hits)
            { 
                if(h.TryGetComponent<IDamaged>(out var target))
                {
                    Hit(target, h.transform.position, powerPercent);    // 当たったときの処理へ
                }
            }
        }

        private void Hit(IDamaged target, Vector3 hitPosition, int powerPercent)
        {
            // ダメージを計算して、敵のHPを操作
            float damage = CalculateFinalDamage(powerPercent);
            target.ChangeHP(-damage);

            // CameraManager.Instance.StartShake(duration, strength, vibrato);
            PlayHitEffects(hitPosition, powerPercent);

            // ゲージ管理
            HandleSpecialGage();
        }

        private float CalculateFinalDamage(int powerPercent)
        {
            // 攻撃の種類ごとの倍率を反映
            float baseDamage = Mathf.RoundToInt(Core.PlayerData.AttackPower * (powerPercent / 100));

            // レベルによる攻撃力の上昇を反映
            float finalDamage = baseDamage * Core.PlayerLevel + (Core.PlayerLevel - 1) * Core.PlayerData.AttackPowerBonusPerLevel;

            return finalDamage;
        }

        private void PlayHitEffects(Vector3 hitPosition, int powerPercent)
        {
            // SP攻撃の場合、SEとエフェクトは固定
            if (powerPercent >= Core.PlayerData.SpecialAttackThreshold)
            {
                SEManager.Instance.PlaySE_AttackHit();
                Instantiate(_effectPrefabSpecial, hitPosition, Quaternion.identity);
            }
            else
            {
                // 通常攻撃の場合、SEをランダムにする
                int rnd = Random.Range(0, 2);
                if (rnd == 0)
                {
                    SEManager.Instance.PlaySE_AttackHit();

                }
                else
                {
                    SEManager.Instance.PlaySE_AttackHit2();
                }

                Instantiate(_effectPrefab, hitPosition, Quaternion.identity);  // エフェクト
            }
        }

        private void HandleSpecialGage()
        {
            Core.SpecialGage++;

            // ゲージがたまっていれば
            if (Core.SpecialGage >= Core.PlayerData.MaxSpecialGage)
            {
                Core.PermissionSpecialAttack = true;    // SP攻撃を許可
            }

            Core.UpdateUI();
        }

        // 攻撃時に一瞬前にダッシュする
        public IEnumerator StepForward(float dashMaxSpeed)
        {
            Vector3 currentDir = transform.forward;
            Vector3 dashDir = new Vector3(currentDir.x, 0, currentDir.z).normalized;    // Y成分を消す

            float duration = Core.PlayerData.AttackDashDuration;    // ダッシュ時間
            float timer = 0;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                float normalizedTime = timer / duration;

                // 徐々に減速しながら進む
                float currentSpeed = dashMaxSpeed * (1 - normalizedTime);
                Rb.linearVelocity = new Vector3(dashDir.x * currentSpeed, Rb.linearVelocity.y, dashDir.z * currentSpeed);

                yield return null;
            }
            Rb.linearVelocity = new Vector3(0, Rb.linearVelocity.y, 0);     //Y成分以外を0にする
        }
    }
}