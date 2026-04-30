using UnityEngine;

namespace Player
{
    /// <summary>
    /// PlayerDataを所有している
    /// PlayerMovementやPlayerAttackHandler等に、SOデータをはじめとするデータたちを分配する
    /// </summary>
    public class PlayerCore : PlayerBase
    {
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
