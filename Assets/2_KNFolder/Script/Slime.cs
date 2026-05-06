using System.Collections;
using Unity.Collections;
using UnityEngine;

namespace Enemy
{
    public class Slime : EnemyBase, IDamaged
    {
        [Header("çUåÇÇçsÇ§ç≈í∑ãóó£")]
        [SerializeField] private float _attackRange;
        [SerializeField] private AttackArea attackArea;

        private Animator _anim;

        private E_Move _moveModule;
        private E_Look _lookModule;

        private Coroutine _currentCoroutine;

        private float knockbackCounter;

        private static readonly int _isWalking = Animator.StringToHash("isWalking");
        private static readonly int _isIdling = Animator.StringToHash("isIdling");
        private static readonly int _isAttacking = Animator.StringToHash("isAttacking");
        private static readonly int _isDamaged = Animator.StringToHash("isDamaged");
        private static readonly int _isDead = Animator.StringToHash("isDead");

        protected override void Start()
        {
            base.Start();

            _anim = GetComponent<Animator>();

            _moveModule = GetComponent<E_Move>();
            _lookModule = GetComponent<E_Look>();
        }
        private void Update()
        {
            switch (_currentState)
            {
                case EnemyState.Idle:
                    _currentState = EnemyState.Move; // úpújó\íË
                    break;
                case EnemyState.Move:
                    _moveModule.OnTick();

                    if (Vector3.Distance(_target.transform.position, transform.position) <= _attackRange)
                        _currentState = EnemyState.Attack;
                    break;
                case EnemyState.Attack:
                    if (_currentCoroutine == null)
                        _currentCoroutine = StartCoroutine(AttackSequence());
                    break;

                case EnemyState.Damaged:
                    if (_currentCoroutine == null)
                        _currentCoroutine = StartCoroutine(DamagedSequence());
                    Knockback();
                    break;
                case EnemyState.Dead:
                    break;
            }
        }

        private IEnumerator AttackSequence()
        {
            SetAnimation(_isIdling);
            yield return StartCoroutine(WaitForAnimation("IdleBattle"));

            //çUåÇèàóù
            attackArea.Attack(0.25f);

            _lookModule.OnTick();
            SetAnimation(_isAttacking);
            yield return StartCoroutine(WaitForAnimation("Attack01"));

            SetAnimation(_isIdling);
            yield return StartCoroutine(WaitForAnimation("IdleBattle"));

            yield return new WaitForSeconds(0.1f);

            _currentCoroutine = null;
            _currentState = EnemyState.Idle;
        }

        private IEnumerator DamagedSequence()
        {
            if (Hp <= 0)
            {
                _currentState = EnemyState.Dead;
                GetComponent<Collider>().enabled = false;
                SetAnimation(_isDead);
                yield return StartCoroutine(WaitForAnimation("Die"));
                yield return new WaitForSeconds(1.5f);
  
                Destroy(gameObject);
            }
            else
            {
                SetAnimation(_isDamaged);
                yield return StartCoroutine(WaitForAnimation("GetHit"));
                _currentCoroutine = null;
                SetAnimation(0);
                _currentState = EnemyState.Idle;
            }
        }

        private void Knockback()
        {
            transform.position = transform.position - 3f * knockbackCounter * (_target.transform.position - transform.position).normalized * Time.deltaTime;
            knockbackCounter -= Time.deltaTime;
        }

        private IEnumerator WaitForAnimation(string stateName)
        {
            yield return null;

            while (!_anim.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            {
                yield return null;
            }

            AnimatorStateInfo state = _anim.GetCurrentAnimatorStateInfo(0);
            while (state.IsName(stateName) && state.normalizedTime < 0.95f)
            {
                state = _anim.GetCurrentAnimatorStateInfo(0);
                yield return null;
            }
        }
        private void SetAnimation(int activeParam)
        {
            _anim.SetBool(_isWalking, activeParam == _isWalking);
            _anim.SetBool(_isIdling, activeParam == _isIdling);
            _anim.SetBool(_isAttacking, activeParam == _isAttacking);
            if (activeParam == _isDamaged)
                _anim.SetTrigger("isDamaged");
            if (activeParam == _isDead)
                _anim.SetTrigger("dead");
            //_anim.SetBool(_isDamaged, activeParam == _isDamaged);
            //_anim.SetBool(_dead, activeParam == _dead);
        }
        public void ChangeHP(float value)
        {
            Shield += value;
            if (Shield <= 0)
            {
                Hp += Shield;
                Shield = 0;
                knockbackCounter = 1;
            }
            else
            {
                return;
            }

            if (value < 0)
            {
                if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
                _currentState = EnemyState.Damaged;
            }
        }
    }
}
