using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance { get; private set; }

    public TextMeshProUGUI PhaseText;
    public event Action OnAllEnemiesCleared;
    public int CurrentPhase = 0;

    public static int HighScore = 0;

    private bool _isSpawning = false;

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
        if (EnemyManager.Instance.GetEnemyNum() <= 0 && !_isSpawning)
        {
            _isSpawning = true;
            StartCoroutine(EnemySpawn());
        }
    }

    private IEnumerator EnemySpawn()
    {
        OnAllEnemiesCleared?.Invoke();      // スポナーたちに一斉に生成命令を送る
        CurrentPhase++;
        PhaseText.text = CurrentPhase.ToString();
        yield return new WaitForSeconds(0.1f);  // 連続で生成したりPhaseをプラスしないように少しだけ待機
        _isSpawning = false;

        // もし新記録を出せば更新
        if(HighScore < CurrentPhase)
        {
            HighScore = CurrentPhase;
        }
    }
}
