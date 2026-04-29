using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "ScriptableObject/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float MaxHp = 10;
    public float AttackPower = 1;
}
