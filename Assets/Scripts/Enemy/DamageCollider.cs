using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public Collider colliderDamage;
    public int damage;

    private void Awake()
    {
        colliderDamage = GetComponent<Collider>();
    }

    public void OpenDamageCollider()
    {
        colliderDamage.enabled = true;
    }

    public void CloseDamageCollider()
    {
        colliderDamage.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
}
