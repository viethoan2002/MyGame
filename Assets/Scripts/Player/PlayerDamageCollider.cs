using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageCollider : MonoBehaviour
{
    public Collider colliderDamage;
    public int damage;

    private void Awake()
    {
        colliderDamage = GetComponent<Collider>();
    }

    public void OpenDamageCollider(int _damage)
    {
        damage = _damage;
        colliderDamage.enabled = true;
    }

    public void CloseDamageCollider()
    {
        colliderDamage.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(other.GetComponent<EnemyStats>().currentHealth>0)
                other.GetComponent<EnemyStats>().TakeDamage(damage);
        }
    }
}
