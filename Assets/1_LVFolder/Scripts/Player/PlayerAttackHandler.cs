using Enemy;
using Player;
using System.Collections;
using UnityEngine;

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
    [SerializeField] private GameObject _effectPrefab2;
    [SerializeField] private GameObject _effectPrefabSP;

    protected override void Awake()
    {
        base.Awake();
    }

    // アニメーションのイベントから呼び出される
    // 引数によって、元々の攻撃力を基準として与えるダメージを変更することもできる
    public void OnHit(int powerPercent)
    {
        PlayerData.AttackParam currentParam;

        if (powerPercent >= 500)    //スペシャル攻撃
        {
            currentParam = Core.PlayerData.AttackSP;
        }
        else
        {
            // 2. 通常コンボの判定
            int comboIndex = GetComponent<PlayerInput>().AttackCount;
            switch (comboIndex)
            {
                case 1: currentParam = Core.PlayerData.Attack1; break;
                case 2: currentParam = Core.PlayerData.Attack2; break;
                case 3: currentParam = Core.PlayerData.Attack3; break;
                case 0: currentParam = Core.PlayerData.Attack4; break;
                default: currentParam = Core.PlayerData.Attack1; break;
            }
        }

        float currentRange = currentParam.Range;

        float baseDamage = Mathf.RoundToInt(Core.PlayerData.AttackPower * (powerPercent / 100.0f));
        float finalDamage = baseDamage + (Core.PlayerLevel - 1) * Core.PlayerData.AttackPowerBonusPerLevel;

        Collider[] hits = Physics.OverlapSphere(_attackPoint.position, currentRange, _enemyLayer);

        foreach (var h in hits)
        {
            if (h.TryGetComponent<IDamaged>(out var target))
            {
                target.ChangeHP(-finalDamage);
                CameraManager.Instance.StartShake(duration, strength, vibrato);

                if (powerPercent >= 500)    //スペシャル攻撃
                {
                    SEManager.Instance.PlaySE_AttackHit();
                    Instantiate(_effectPrefabSP, h.transform.position, Quaternion.identity);
                }

                int rnd = Random.Range(0, 2);
                if (rnd == 0)
                {
                    SEManager.Instance.PlaySE_AttackHit();
                    Instantiate(_effectPrefab, h.transform.position, Quaternion.identity);

                }
                else
                {
                    SEManager.Instance.PlaySE_AttackHit2();
                    Instantiate(_effectPrefab2, h.transform.position, Quaternion.identity);

                }

                if (Core.SpecialGage >= Core.PlayerData.MaxSpecialGage)
                {
                    Core.PermissionSpecialAttack = true;
                }
                else
                {
                    Core.SpecialGage++;
                    Core.UpdateUI();
                }
                
            }
        }
    }

    public IEnumerator DashAttack(float dashMaxSpeed)
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
