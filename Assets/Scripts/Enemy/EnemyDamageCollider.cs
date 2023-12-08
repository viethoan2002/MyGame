using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCollider : MonoBehaviour
{
    public EnemyManager enemyManager;
    public DamageCollider damageCollider;
    public GameObject _arrow;
    public Transform Pos;

    private void Awake()
    {
        damageCollider = GetComponentInChildren<DamageCollider>();
    }

    public void OpenDamage()
    {
        damageCollider.OpenDamageCollider();
    }

    public void CloseDamage()
    {
        damageCollider.CloseDamageCollider();
    }

    public void ShootArrow()
    {
        GameObject arrow=Instantiate(_arrow) as GameObject;
        Vector3 target = enemyManager.currentTarget.transform.position;
        target.y = Pos.transform.position.y;
        arrow.transform.position = Pos.position;
        arrow.transform.LookAt(target);
    }
}
