using UnityEngine;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public EnemyData EnemyData;

        [Header("タ―ゲット")]
        public GameObject _target;

        public float Hp;

        private bool _registered;

        protected virtual void Awake()
        {
            if (EnemyManager.Instance)
            {
                EnemyManager.Instance.Register(this);
                _registered = true;
            }
        }
        protected virtual void Start()
        {
            if (!_registered)
                EnemyManager.Instance.Register(this);

            Hp = EnemyData.MaxHp;
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
        }
        private void OnDestroy()
        {
            EnemyManager.Instance.UnRegister(this);
        }
    }

    public enum EnemyState
    {
        Idle, Move, Attack, Damaged, Dead
    }

}
