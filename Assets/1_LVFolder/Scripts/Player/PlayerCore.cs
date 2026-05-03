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

        private void Start()
        {
            UpdateLevelUI();
        }

        private void Update()
        {
            DebugOfLevelUP();
        }

        private void DebugOfLevelUP()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log("デバッグ機能　プレイヤーのレベルを1増やしました: " + PlayerLevel);
                PlayerLevel++;
                PlayerHealth.ChangeHP(1000);    //全回復
                UpdateLevelUI();
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("デバッグ機能　プレイヤーのレベルを1減らしました: " + PlayerLevel);
                PlayerLevel--;
                PlayerHealth.ChangeHP(1);   //このメソッドで最大HPに抑え込んでくれる
                UpdateLevelUI();
            }
        }

        private void UpdateLevelUI()
        {
            if (UIManager.Instance != null && UIManager.Instance.PlayerLevelText != null)
            {
                UIManager.Instance.PlayerLevelText.text = PlayerLevel.ToString();
            }
        }
    }

}
