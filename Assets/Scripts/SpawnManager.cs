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
    private bool _stopSpowning = false;
    [SerializeField]
    private GameObject _powerUpPrefab;
    [SerializeField]
    private GameObject [] _PowerUps;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator SpawnEnemyRoutine()
    {
        while (!_stopSpowning)
        {
            Vector3 posToSpown = new Vector3(Random.Range(-10, 10), 9, 0);

            GameObject newEnemy=Instantiate(_enemyPrefab,posToSpown,Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            
            yield return new WaitForSeconds(5.0f);
        }
    }
    IEnumerator SpawnPowerUpRoutine()
    {
        while (!_stopSpowning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-10, 10), 9, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(_PowerUps[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpowning = true;
    }
   
}

