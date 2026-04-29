using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class E_Move : MonoBehaviour
    {
        [Header("プレイヤーをターゲットとするか")]
        [SerializeField] private bool _targetPlayer;
        [Header("なにをターゲットとするか(プレイヤー以外の場合)")]
        public GameObject _target;
        [Header("移動スピード")]
        [SerializeField] private float _moveSpeed = 1;
        //[Header("最短距離以下なら離れるか")]
        //[SerializeField] private bool _makeDistance;
        [Header("最短距離")]
        [SerializeField] private float _minDistance;

        private Rigidbody _rb;
        private Animator _animator;

        private NavMeshAgent _agent;

        private void Start()
        {
            if (_targetPlayer)
            {
                _target = GameObject.Find("Player");
            }

            _rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();

            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _moveSpeed;
            _agent.stoppingDistance = _minDistance;

            transform.LookAt(_agent.destination);
        }

        private void Update()
        {
            if (_agent.enabled == false) return;

            _agent.SetDestination(_target.transform.position);

            //十分に近づいている時
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                _animator.SetBool("isWalking", false);
            }
            else
            {
                _animator.SetBool("isWalking", true);
            }
        }
    }
}