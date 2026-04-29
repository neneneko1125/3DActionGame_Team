using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class Slime : EnemyBase, IDamaged
    {
        [Header("オリジナル設定")]
        [SerializeField] private GameObject _target;
        [Header("攻撃を行う最長距離")]
        [SerializeField] private float _attackRange;

        private Coroutine _currentCoroutine;

        private void Update()
        {
            if (_currentCoroutine != null) return;

            if ((_target.transform.position - transform.position).magnitude <= _attackRange)
            {
                _currentCoroutine = StartCoroutine(Attack());
            }
        }

        private IEnumerator Attack()
        {
            yield return null;
        }
        public void ChangeHP(float value)
        {
            Hp += value;

            if (Hp <= 0)
            {
                Destroy(gameObject);
            }
        }
        private void OnCollisionStay(Collision collision)
        {
            Debug.Log(collision.gameObject.name);
        }
    }
}
