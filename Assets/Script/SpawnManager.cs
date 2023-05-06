using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _speedPrefab;
    [SerializeField]
    private GameObject _container;
    [SerializeField]
    private bool _stopSpawning = false;

    private const float SpawnTime = 5.0f;

    private Transform _containerTransform;

    void Start()
    {

        _containerTransform = _container.transform;
        StartCoroutine(SpawnRoutine());
        StartCoroutine(TripleRoutine());
        StartCoroutine(SpeedRoutine());
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
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity, _containerTransform);
            yield return new WaitForSeconds(SpawnTime);
            }
        }
    }

    IEnumerator TripleRoutine()
    {
        while (!_stopSpawning)
        {
            if (_tripleShotPrefab == null)
            {
                Debug.LogError("Power prefab is not assigned!");
                yield break;
            }

            while (!_stopSpawning)
            {
                Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                GameObject newPower = Instantiate(_tripleShotPrefab, posToSpawn, Quaternion.identity, _containerTransform);
                yield return new WaitForSeconds(Random.Range(30f, 50f));
            }
        }
    }

    IEnumerator SpeedRoutine()
    {
        while (!_stopSpawning)
        {
            if (_speedPrefab == null)
            {
                Debug.LogError("Power prefab is not assigned!");
                yield break;
            }

            while (!_stopSpawning)
            {
                Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                GameObject newPower = Instantiate(_speedPrefab, posToSpawn, Quaternion.identity, _containerTransform);
                yield return new WaitForSeconds(Random.Range(10f, 30f));
            }
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;

        foreach (Transform child in _containerTransform)
        {
            Destroy(child.gameObject);
        }
    }
}
