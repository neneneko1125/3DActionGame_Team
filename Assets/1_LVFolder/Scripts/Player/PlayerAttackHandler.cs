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
        float finalDamage = Mathf.RoundToInt(Core.PlayerData.AttackPower * (powerPercent / 100.0f));
        Collider[] hits = Physics.OverlapSphere(_attackPoint.position, _attackRange, _enemyLayer);

        foreach(var h in hits)
        {
            h.GetComponent<Slime>().ChangeHP(-finalDamage);
        }
    }

    public IEnumerator DashAttack(float dashMaxSpeed)
    {
        Vector3 dashDir = transform.forward;
        float timer = 0;
        float duration = Core.PlayerData.AttackDashDuration;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float normalizedTime = timer / duration;

            // 徐々に減速しながら進む
            float currentSpeed = dashMaxSpeed * (1 - normalizedTime);
            Rb.linearVelocity = new Vector3(dashDir.x * currentSpeed, Rb.linearVelocity.y, dashDir.z * currentSpeed);

            yield return null;
        }

        Rb.linearVelocity = new Vector3(0, Rb.linearVelocity.y, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

   
}
