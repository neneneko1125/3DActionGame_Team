using UnityEngine;

namespace Player
{
    /// <summary>
    /// PlayerDataを所有している
    /// PlayerMovementやPlayerAttackHandler等に、SOデータをはじめとするデータたちを分配する
    /// プレイヤーの状態を管理する　無敵状態、スタン状態など
    /// </summary>
    public class PlayerCore : PlayerBase
    {
        public bool IsStunned;
        public bool IsInvicible;
        public bool IsDead;

        public static PlayerCore Instance { get; private set; }
        public PlayerData PlayerData;

        protected override void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            base.Awake();
        }
    }

}
