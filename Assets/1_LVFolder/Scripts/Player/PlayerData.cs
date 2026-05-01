using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "ScriptableObject/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("最大HP")]
    public float MaxHp = 10.0f;
    [Header("攻撃力")]
    public float AttackPower = 1.0f;
    [Header("攻撃速度")]
    public float AttackSpeed = 3.0f;
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
