using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _delayTime = 0.0f;

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyPrefab2;
    [SerializeField] private GameObject _enemyPrefab3;
    [SerializeField] private GameObject _enemyPrefab4;
    [SerializeField] private GameObject _enemyPrefab5;
    [SerializeField] private GameObject _enemyPrefab6;
    [SerializeField] private GameObject _enemyPrefab7;
    [SerializeField] private GameObject _enemyPrefab8;
    [SerializeField] private GameObject _enemyPrefab9;

    [SerializeField] private int _phase1Point;
    [SerializeField] private int _phase2Point;
    [SerializeField] private int _phase3Point;
    [SerializeField] private int _phase4Point;
    [SerializeField] private int _phase5Point;
    [SerializeField] private int _phase6Point;
    [SerializeField] private int _phase7Point;
    [SerializeField] private int _phase8Point;


    private void Start()
    {
        if (EnemySpawnManager.Instance != null)
        {
            EnemySpawnManager.Instance.OnAllEnemiesCleared += SpawnEnemyByPhase;   // イベントに登録
        }
    }

    //EnemySpawnManagerから合図が来たら実行する
    private void SpawnEnemyByPhase()
    {
        if(EnemySpawnManager.Instance.CurrentPhase < _phase1Point)
        {
            StartCoroutine(SpawnEnemyWithDelay(_enemyPrefab));
        }
        else if(EnemySpawnManager.Instance.CurrentPhase < _phase2Point)
        {
            StartCoroutine(SpawnEnemyWithDelay(_enemyPrefab2));
        }
        else if(EnemySpawnManager.Instance.CurrentPhase < _phase3Point)
        {
            StartCoroutine(SpawnEnemyWithDelay(_enemyPrefab3));
        }
        else if (EnemySpawnManager.Instance.CurrentPhase < _phase4Point)
        {
            StartCoroutine(SpawnEnemyWithDelay(_enemyPrefab4));
        }
        else if (EnemySpawnManager.Instance.CurrentPhase < _phase5Point)
        {
            StartCoroutine(SpawnEnemyWithDelay(_enemyPrefab5));
        }
        else if (EnemySpawnManager.Instance.CurrentPhase < _phase6Point)
        {
            StartCoroutine(SpawnEnemyWithDelay(_enemyPrefab6));
        }
        else if (EnemySpawnManager.Instance.CurrentPhase < _phase7Point)
        {
            StartCoroutine(SpawnEnemyWithDelay(_enemyPrefab7));
        }
        else if (EnemySpawnManager.Instance.CurrentPhase < _phase8Point)
        {
            StartCoroutine(SpawnEnemyWithDelay(_enemyPrefab8));
        }
        else
        {
            StartCoroutine(SpawnEnemyWithDelay(_enemyPrefab9));
        }
    }

    private IEnumerator SpawnEnemyWithDelay(GameObject prefab)
    {
        yield return new WaitForSeconds(_delayTime);
        Instantiate(prefab, transform.position, Quaternion.identity);
    }

}
