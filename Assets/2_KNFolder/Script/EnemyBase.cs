using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public EnemyData EnemyData;

        //[Header("タ―ゲット")]
        [HideInInspector]
        public GameObject _target;


        [HideInInspector]
        public float Hp;
        [HideInInspector]
        public float Shield;

        public EnemyState _currentState;

        private bool _registered;
        private bool _generateEXP = true;

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

            _target = PlayerCore.Instance.gameObject;

            Hp = EnemyData.MaxHp;
            Shield = EnemyData.DefaultShield;
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
        }
        private void OnApplicationQuit()
        {
            _generateEXP = false;
        }
        private void OnDestroy()
        {
            if (!_generateEXP) return;
            foreach (var de in EnemyData.DropEXP)
            {
                int amount = Random.Range(de.minAmount, de.maxAmount + 1);
                for (int i = 0; i < amount; i++)
                {
                    Vector3 generatePos = transform.position;
                    generatePos.x += Random.Range(-0.1f, 0.1f);
                    generatePos.y += 0.5f;
                    generatePos.z += Random.Range(-0.1f, 0.1f);

                    Instantiate(de.exp, generatePos, Quaternion.identity);
                }
            }

            EnemyManager.Instance.UnRegister(this);
        }
    }

    public enum EnemyState
    {
        Idle, Move, Attack, Damaged, Dead
    }

}
