using UnityEngine;
using System;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance { get; private set; }

    public event Action OnAllEnemiesCleared;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(EnemyManager.Instance.GetEnemyNum() <= 0)
        {
            OnAllEnemiesCleared?.Invoke();      // スポナーたちに一斉に生成命令を送る
        }
    }
}
