using UnityEngine;
using Enemy;
using Player;

public class PlayerHealth : PlayerBase, IDamaged
{
    private float _currentHp;

    protected override void Awake()
    {
        base.Awake();

        if (Core == null)
        {
            Debug.LogError("コアがないです！");
            return;
        }
        if (Core.PlayerData == null)
        {
            Debug.LogError("SOデータがないです！");
            return;
        }

        _currentHp = Core.PlayerData.MaxHp;
    }

    public void ChangeHP(float value)
    {
        _currentHp += value;

        if (_currentHp <= 0)
        {
            Debug.Log("PlayerのHPが0になりました");
           // Destroy(gameObject);
        }
    }
}
