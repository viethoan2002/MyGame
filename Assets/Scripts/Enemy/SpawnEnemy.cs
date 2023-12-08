using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public QuestStep questStep;
    public Transform spawnPos;
    public Transform poolPos;
    public PoolObject poolObject;
    public List<Vector3> posList = new List<Vector3>();
    public bool isSpawn;

    public int enemyAmount;
    public int currentEnemy;
    public int timeSpawn;

    private void OnEnable()
    {
        QuestStep.OnStart += StartSpawn;
    }

    private void OnDisable()
    {
        QuestStep.OnStart -= StartSpawn;
    }

    public void StartSpawn(QuestStep questStep)
    {
        if (questStep != this.questStep)
            return;

        if (!isSpawn)
        {
            isSpawn = true;
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        while (currentEnemy<enemyAmount)
        {
            for (int i = 0; i < posList.Count; i++)
            {
                GameObject _enemy =poolObject.GetPoolObject();
                _enemy.transform.parent = spawnPos;
                _enemy.transform.GetComponent<EnemyManager>().spawnEnemy = this;
                _enemy.transform.GetComponent<EnemyManager>().enemyStats.currentHealth = 100;
                _enemy.transform.GetComponent<EnemyManager>().GetComponent<Collider>().isTrigger = false;
                _enemy.transform.GetComponent<EnemyManager>().currentState = _enemy.transform.GetComponent<EnemyManager>().idleState;
                _enemy.transform.GetComponent<EnemyManager>().navMeshAgent.Warp(posList[i]);
                _enemy.transform.position = posList[i];

                float randomRotationAngle = Random.Range(0f, 360f);
                _enemy.transform.Rotate(new Vector3(0, randomRotationAngle, 0));
                currentEnemy += 1;
            }

            yield return new WaitForSeconds(timeSpawn);
        }
    }
    public void AddPool(GameObject _enemy)
    {
        poolObject.AddPool(_enemy);
    }
}
