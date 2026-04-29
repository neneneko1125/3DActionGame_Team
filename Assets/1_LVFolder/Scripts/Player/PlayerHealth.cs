using UnityEngine;
using Enemy;
using Player;

public class PlayerHealth : PlayerBase, IDamaged
{
    private float _currentHp;

    private void Awake()
    {
        if(PlayerData == null)
        {
            Debug.LogError("SOデータがないです！");
            return;
        }

        _currentHp = PlayerData.MaxHp;
    }

    public void Damaged(float value)
    {
        _currentHp += value;

        if (_currentHp <= 0)
        {
            Debug.Log("PlayerのHPが0になりました");
           // Destroy(gameObject);
        }
    }
}
