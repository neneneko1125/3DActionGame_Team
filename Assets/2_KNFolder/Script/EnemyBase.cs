using UnityEngine;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public EnemyData EnemyData;

        /*[HideInInspector]*/ protected float Hp;

        private void Awake()
        {
            if (EnemyManager.Instance)
                EnemyManager.Instance.Register(this);
            else
                Debug.Log("Awake‚©‚çStart‚É‚µ‚Ä‚­‚¾‚³‚¢...");
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
