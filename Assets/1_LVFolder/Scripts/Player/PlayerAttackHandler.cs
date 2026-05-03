using Player;
using Enemy;
using UnityEngine;
using System.Collections;

public class PlayerAttackHandler : PlayerBase
{
    [SerializeField] private float _attackRange = 1.0f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayer;

    protected override void Awake()
    {
        base.Awake();
    }

    // アニメーションのイベントから呼び出される
    // 引数によって、元々の攻撃力を基準として与えるダメージを変更することもできる
    public void OnHit(int powerPercent)
    {
        float baseDamage = Mathf.RoundToInt(Core.PlayerData.AttackPower * (powerPercent / 100.0f));     // 攻撃の種類によって攻撃力を変化させる
        float finalDamage = baseDamage + (Core.PlayerLevel - 1) * Core.PlayerData.AttackPowerBonusPerLevel;   // ゲーム中の強化を反映

        Collider[] hits = Physics.OverlapSphere(_attackPoint.position, _attackRange, _enemyLayer);
        foreach(var h in hits)
        {
            if (h.TryGetComponent<IDamaged>(out var target))
            {
                target.ChangeHP(-finalDamage);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

   
}
