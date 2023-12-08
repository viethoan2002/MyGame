using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public GameObject Enemy;
    public List<GameObject> poolObject = new List<GameObject>();
    public GameObject GetPoolObject()
    {
        if (poolObject.Count > 0)
        {
            GameObject _enemy = poolObject[0];
            poolObject.Remove(_enemy);
            _enemy.SetActive(true);
            return _enemy;
        }
        else
        {
            return Instantiate(Enemy);
        }
    }

    public void AddPool(GameObject _enemy)
    {
        _enemy.SetActive(false);
        poolObject.Add(_enemy);
        _enemy.transform.parent = this.transform;
    }
}
