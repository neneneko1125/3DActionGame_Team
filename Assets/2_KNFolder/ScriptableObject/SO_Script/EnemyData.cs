using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "ScriptableObject/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("最大HP")]
    public float MaxHp = 1;
    [Header("攻撃力")]
    public float AttackPower = 1;
    [Header("ドロップ経験値")]
    public DropEXP[] DropEXP;
}

[System.Serializable]
public class DropEXP
{
    public EXP exp;
    public int minAmount;
    public int maxAmount;
}
