using System.Collections.Generic;
using UnityEngine;
using Enemy;
using Unity.VisualScripting;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (var enemy in _enemies)
            {
                enemy.GetComponent<IDamaged>().ChangeHP(-1000);
            }
        }
    }

    public int GetEnemyNum(bool refresh = false)
    {
        if (refresh)
        {
            List<EnemyBase> list = new();
            foreach(EnemyBase enemy in _enemies)
            {
                if (enemy)
                    list.Add(enemy);
            }
            return list.Count;
        }

        return _enemies.Count;
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
