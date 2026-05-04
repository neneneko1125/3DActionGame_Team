using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class E_Move : EnemyModule
    {
        [Header("移動スピード")]
        [SerializeField] private float _moveSpeed = 1;
        [Header("最短距離")]
        [SerializeField] private float _minDistance;

        private GameObject _target;

        //private Rigidbody _rb;
        private NavMeshAgent _agent;

        private void Start()
        {
            _target = GetComponent<EnemyBase>()._target;

            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _moveSpeed;
            _agent.stoppingDistance = _minDistance;
        }

        public override void OnTick()
        {
            if (!_target)
            {
                _target = GetComponent<EnemyBase>()._target;
            }

            _agent.SetDestination(_target.transform.position);
        }
    }
}