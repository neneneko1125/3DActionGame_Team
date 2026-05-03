using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyPrefab2;
    [SerializeField] private GameObject _enemyPrefab3;

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
        if(EnemySpawnManager.Instance.CurrentPhase <= 2)
        {
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        }
        else if(EnemySpawnManager.Instance.CurrentPhase <= 4)
        {
            Instantiate(_enemyPrefab2, transform.position, Quaternion.identity);
        }
        else if(EnemySpawnManager.Instance.CurrentPhase <= 6)
        {
            Instantiate(_enemyPrefab3, transform.position, Quaternion.identity);
        }
    }

}
