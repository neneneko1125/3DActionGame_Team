using Player;
using Enemy;
using UnityEngine;

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
