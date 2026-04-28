using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class EnemyManager : MonoBehaviour
{
    static public EnemyManager Instance;

    private List<EnemyBase> _enemies = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void Register(EnemyBase enemy)
    {
        if (enemy && !_enemies.Contains(enemy))
            _enemies.Add(enemy);
    }

    public void UnRegister(EnemyBase enemy)
    {
        if (enemy && _enemies.Contains(enemy))
            _enemies.Remove(enemy);
    }
}
