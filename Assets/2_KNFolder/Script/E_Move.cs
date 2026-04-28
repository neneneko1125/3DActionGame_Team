using UnityEngine;

namespace Enemy
{
    public class E_Move : MonoBehaviour
    {
        [Header("プレイヤーをターゲットとするか")]
        [SerializeField] private bool _targetPlayer;
        [Header("なにをターゲットとするか(プレイヤー以外の場合)")]
        [SerializeField] private GameObject _target;
        [Header("移動スピード")]
        [SerializeField] private float _moveSpeed = 1;
        [Header("最短距離以下なら離れるか")]
        [SerializeField] private bool _makeDistance;
        [Header("最短距離")]
        [SerializeField] private float _minDistance;

        private Rigidbody _rb;

        private void Start()
        {
            if (_targetPlayer)
            {
                _target = GameObject.Find("Player");
            }

            _rb = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            if (!_target) return;

            Vector3 vec = _target.transform.position - transform.position;
            if (vec.magnitude > _minDistance)
            {
                _rb.linearVelocity = vec.normalized * _moveSpeed;
            }
            else
            {
                if (_makeDistance)
                {
                    _rb.linearVelocity = vec.normalized * -_moveSpeed;
                }
                else
                {
                    _rb.linearVelocity = Vector3.zero;
                }
            }

        }
    }
}