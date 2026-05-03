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
        public int PlayerLevel = 1;
        public int PlayerEXP = 0;
        public int NeedEXP = 10;

        public bool IsStunned;
        public bool IsInvicible;
        public bool IsDead;

        public static PlayerCore Instance { get; private set; }
        public PlayerData PlayerData;

        protected override void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            base.Awake();
        }

        private void Start()
        {
            UpdateLevelUI();
        }

        private void UpdateLevelUI()
        {
            if (UIManager.Instance != null && UIManager.Instance.PlayerLevelText != null)
            {
                UIManager.Instance.PlayerLevelText.text = PlayerLevel.ToString();
                UIManager.Instance.EXPBarBlue.fillAmount = (float)PlayerEXP / (PlayerLevel * NeedEXP);
            }
        }


        private void OnCollisionEnter(Collision collision)
        {
            // そのオブジェクトが経験値か確認
            if(collision.gameObject.TryGetComponent<EXP>(out var exp))
            {
                PlayerEXP += exp.Point;     //経験値増加
                ChackLevelUp(); 
                Destroy(collision.gameObject);
            }
        }

        private void ChackLevelUp()
        {
            if(PlayerEXP >= PlayerLevel * NeedEXP)
            {
                PlayerLevel++;
                PlayerHealth.ChangeHP(1000);
                PlayerEXP -= PlayerLevel * NeedEXP;
            }
            UpdateLevelUI();
        }

    }
}
