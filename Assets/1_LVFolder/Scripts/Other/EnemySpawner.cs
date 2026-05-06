using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _delayTime = 0.0f;

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyPrefab2;
    [SerializeField] private GameObject _enemyPrefab3;

    [SerializeField] private int _phase1Point;
    [SerializeField] private int _phase2Point;
    [SerializeField] private int _phase3Point;


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
    }

    private IEnumerator SpawnEnemyWithDelay(GameObject prefab)
    {
        yield return new WaitForSeconds(_delayTime);
        Instantiate(prefab, transform.position, Quaternion.identity);
    }

}
