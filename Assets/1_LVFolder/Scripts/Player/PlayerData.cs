using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "ScriptableObject/PlayerData")]
public class PlayerData : ScriptableObject
{
    // インスペクターで表示するために [System.Serializable] が必須
    [System.Serializable]
    public struct AttackParam
    {
        public float Speed;
        public float Range;
        public float DashDistance;
    }

    [Header("=== 基本ステータス ===")]
    public float MaxHp = 10.0f;
    public int HpBonusPerLevel = 5;
    [Space(5)]
    public float AttackPower = 1.0f;
    public float AttackPowerBonusPerLevel = 1.0f;

    [Header("=== 攻撃パラメータ ===")]
    public AttackParam Attack1 = new AttackParam { Speed = 3.0f, Range = 1.0f, DashDistance = 3.0f };
    public AttackParam Attack2 = new AttackParam { Speed = 3.0f, Range = 1.0f, DashDistance = 3.0f };
    public AttackParam Attack3 = new AttackParam { Speed = 10.0f, Range = 2.0f, DashDistance = 3.0f };
    public AttackParam Attack4 = new AttackParam { Speed = 3.0f, Range = 1.5f, DashDistance = 3.0f };
    public AttackParam AttackSpecial = new AttackParam { Speed = 3.0f, Range = 3.0f, DashDistance = 5.0f };
    [Header("SP攻撃に必要なHIT数")]
    public int MaxSpecialGage = 100;
    [Tooltip("OnHitの引数がこの値以上ならSP攻撃として扱う")]
    public int SpecialAttackThreshold = 1000;

    [Header("=== コンボ・硬直設定 ===")]
    [Tooltip("ダッシュ移動にかかる時間")]
    public float AttackDashDuration = 0.2f;
    [Tooltip("コンボを受け付ける猶予時間")]
    public float AttackComboTime = 0.5f;
    public float StunRecoverySpeed = 1.0f;
    public float InvincibleDuration = 1.0f;

    [Header("=== 移動・物理設定 ===")]
    public float MoveSpeed = 3.0f;
    public float TurningSpeed = 0.1f;
    [Space(10)]
    public float FallGravity = 20.0f;
    public float GroundGravity = 2.0f;
}



