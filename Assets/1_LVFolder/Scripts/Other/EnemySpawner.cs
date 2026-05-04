using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
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
            EnemySpawnManager.Instance.OnAllEnemiesCleared += SpawnEnemy;   // イベントに登録
        }
    }

    //EnemySpawnManagerから合図が来たら実行する
    private void SpawnEnemy()
    {
        if(EnemySpawnManager.Instance.CurrentPhase < _phase1Point)
        {
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        }
        else if(EnemySpawnManager.Instance.CurrentPhase < _phase2Point)
        {
            Instantiate(_enemyPrefab2, transform.position, Quaternion.identity);
        }
        else if(EnemySpawnManager.Instance.CurrentPhase < _phase3Point)
        {
            Instantiate(_enemyPrefab3, transform.position, Quaternion.identity);
        }
    }

}
