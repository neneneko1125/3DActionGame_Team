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
    public AttackParam AttackSP = new AttackParam { Speed = 3.0f, Range = 3.0f, DashDistance = 5.0f };
    [Header("SP攻撃に必要なHIT数")]
    public int MaxSpecialGage = 100;

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






//using UnityEngine;

//[CreateAssetMenu(fileName = "NewPlayerData", menuName = "ScriptableObject/PlayerData")]
//public class PlayerData : ScriptableObject
//{
//    [Header("最大HP")]
//    public float MaxHp = 10.0f;
//    [Header("HPの増加量")]
//    public int HpBonusPerLevel = 5;
//    [Header("攻撃力")]
//    public float AttackPower = 1.0f;
//    [Header("攻撃力の増加量")]
//    public float AttackPowerBonusPerLevel = 1.0f;
//    [Header("攻撃速度")]
//    public float AttackSpeed = 3.0f;
//    [Header("攻撃の移動力")]
//    public float AttackDashDistance = 3.0f;
//    [Header("攻撃2速度")]
//    public float Attack2Speed = 3.0f;
//    [Header("攻撃2の移動力")]
//    public float Attack2DashDistance = 3.0f;
//    [Header("攻撃3速度")]
//    public float Attack3Speed = 10.0f;
//    [Header("攻撃3の移動力")]
//    public float Attack3DashDistance = 3.0f;
//    [Header("攻撃4速度")]
//    public float Attack4Speed = 3.0f;
//    [Header("攻撃4の移動力")]
//    public float Attack4DashDistance = 3.0f;
//    [Header("攻撃SP速度")]
//    public float AttackSPSpeed = 3.0f;
//    [Header("攻撃SPの移動力")]
//    public float AttackSPDashDistance = 3.0f;
//    [Header("攻撃時の移動時間(今のところ全攻撃共通)")]
//    public float AttackDashDuration = 0.2f;
//    [Header("次の攻撃入力をこの時間内にすればコンボ成立")]
//    public float AttackComboTime = 0.5f;
//    [Header("移動速度")]
//    public float MoveSpeed = 3.0f;
//    [Header("空中での重力")]
//    public float FallGravity = 20.0f;
//    [Header("地上での重力")]
//    public float GroundGravity = 2.0f;
//    [Header("振り向き速度")]
//    public float TurningSpeed = 0.1f;
//    [Header("スタン復帰速度")]
//    public float StunRecoverySpeed = 1.0f; 
//    [Header("無敵時間")]
//    public float InvincibleDuration = 1.0f;
//}
