using UnityEngine;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public EnemyData EnemyData;

        /*[HideInInspector]*/ protected float Hp;

        private bool _registered;
        private void Awake()
        {
            if (EnemyManager.Instance)
            {
                EnemyManager.Instance.Register(this);
                _registered = true;
            }
        }
        private void Start()
        {
            if (!_registered)
                EnemyManager.Instance.Register(this);
        }

        private void OnDestroy()
        {
            EnemyManager.Instance.UnRegister(this);
        }

        //public enum EnemyState
        //{
        //    Idle, Move, Attack, Damaged, Dead
        //}
    }
}
