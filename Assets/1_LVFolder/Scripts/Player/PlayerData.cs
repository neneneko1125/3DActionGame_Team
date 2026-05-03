using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "ScriptableObject/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("最大HP")]
    public float MaxHp = 10.0f;
    [Header("HPの増加量")]
    public int HpBonusPerLevel = 5;
    [Header("攻撃力")]
    public float AttackPower = 1.0f;
    [Header("攻撃力の増加量")]
    public float AttackPowerBonusPerLevel = 1.0f;
    [Header("攻撃速度")]
    public float AttackSpeed = 3.0f;
    [Header("攻撃時の移動力")]
    public float AttackDashDistance = 3.0f;
    [Header("攻撃2速度")]
    public float Attack2Speed = 3.0f;
    [Header("攻撃2時の移動力")]
    public float Attack2DashDistance = 3.0f;
    [Header("攻撃3速度")]
    public float Attack3Speed = 10.0f;
    [Header("攻撃3時の移動力")]
    public float Attack3DashDistance = 3.0f;
    [Header("攻撃4速度")]
    public float Attack4Speed = 3.0f;
    [Header("攻撃4時の移動力")]
    public float Attack4DashDistance = 3.0f;
    [Header("攻撃時の移動時間(今のところ全攻撃共通)")]
    public float AttackDashDuration = 0.2f;
    [Header("次の攻撃入力をこの時間内にすればコンボ成立")]
    public float AttackComboTime = 0.5f;
    [Header("移動速度")]
    public float MoveSpeed = 3.0f;
    [Header("空中での重力")]
    public float FallGravity = 20.0f;
    [Header("地上での重力")]
    public float GroundGravity = 2.0f;
    [Header("振り向き速度")]
    public float TurningSpeed = 0.1f;
    [Header("スタン復帰速度")]
    public float StunRecoverySpeed = 1.0f; 
    [Header("無敵時間")]
    public float InvincibleDuration = 1.0f;
}
