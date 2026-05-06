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
        public int SpecialGage = 0;
        

        public bool IsStunned;
        public bool IsInvicible;
        public bool IsDead;
        public bool PermissionSpecialAttack = false;

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
            UpdateUI();
        }

        public void UpdateUI()
        {
            if (UIManager.Instance != null && UIManager.Instance.LevelText != null)
            {
                UIManager.Instance.LevelText.text = PlayerLevel.ToString();
                UIManager.Instance.EXPBarBlue.fillAmount = (float)PlayerEXP / (PlayerLevel * NeedEXP);
                UIManager.Instance.SpecalBar.fillAmount = (float)SpecialGage / PlayerData.MaxSpecialGage;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            // そのオブジェクトが経験値か確認
            if(collision.gameObject.TryGetComponent<EXP>(out var exp))
            {
                PlayerEXP += exp.Point;     //経験値増加
                Debug.Log("現在の経験値 : " + PlayerEXP + "  必要経験値 : " + PlayerLevel * NeedEXP);
                ChackLevelUp(); 
                Destroy(collision.gameObject);
            }
        }

        private void ChackLevelUp()
        {
            if(PlayerEXP >= PlayerLevel * NeedEXP)
            {
                SEManager.Instance.PlaySE_LevelUp();
                PlayerEXP -= PlayerLevel * NeedEXP;
                PlayerLevel++;
                PlayerHealth.ChangeHP(1000);    // 1000回復する　(全回復)
            }
            UpdateUI();
        }

    }
}
