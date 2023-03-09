using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private bool _stopSpawning = false;

    private const float SpawnTime = 5.0f;

    private Transform _enemyContainerTransform;

    void Start()
    {
        _enemyContainerTransform = _enemyContainer.transform;
        StartCoroutine(SpawnRoutine());
    }

    //IEnumerator SpawnRoutine()
    //{
    //    while (!_stopSpawning)
    //    {
    //        Vector3 posToSpawn = new Vector3(Random.Range(-8f,8f), 7, 0);
    //        GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity, _enemyContainerTransform);
    //        yield return new WaitForSeconds(SpawnTime);
    //    }
    //}
    IEnumerator SpawnRoutine()
    {
        while (!_stopSpawning)
        {
            if (_enemyPrefab == null)
            {
                Debug.LogError("Enemy prefab is not assigned!");
                yield break;
            }

            while (!_stopSpawning)
            { 
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity, _enemyContainerTransform);
            yield return new WaitForSeconds(SpawnTime);
            }
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;

        foreach (Transform child in _enemyContainerTransform)
        {
            Destroy(child.gameObject);
        }
    }
}
