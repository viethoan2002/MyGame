using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerAnimationCtrl playerAnimationCtrl;
    private WeaponSlotManager weaponSlotManager;

    public List<GameObject> hitObjects = new List<GameObject>();
    public GameObject hitOnlyObjects;
    public AudioSource weaponAudio;
    public GameObject weaponhit;
    public GameObject enemyTarget;
    public LayerMask targetMask;

    public GameObject myCurros;

    public float timeReload;

    private void Start()
    {
        playerAnimationCtrl = GetComponentInChildren<PlayerAnimationCtrl>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
    }

    private void Update()
    {
        if (weaponSlotManager.weapon == null)
            return;
        HandleHold();
        HandleAttack();
    }

    private void HandleHold()
    {

        if (InputHandle.Instance.hold )
        {          
            Cursor.visible = false;
            myCurros.SetActive(true);

            playerAnimationCtrl.animator.SetBool("isHolding", true);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, targetMask))
            {
                Vector3 target = hit.point;
                target.y = transform.position.y;

                if (!playerAnimationCtrl.animator.GetBool("isInteracting"))
                    transform.LookAt(target);

                myCurros.transform.position = hit.point;
            }

            if (weaponSlotManager.weapon.isRange)
            {
                #region GunVision
                Ray ray2 = new Ray(weaponhit.transform.position, transform.forward);

                RaycastHit[] hits = Physics.RaycastAll(ray2, 40);
                System.Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));
                hitObjects.Clear();

                foreach (RaycastHit x in hits)
                {
                    if (x.transform.gameObject.tag == "Enemy")
                    {
                        hitObjects.Add(x.transform.gameObject);
                    }
                    else
                    {
                        break;
                    }
                }

                if (hitObjects.Count > 0)
                {
                    myCurros.GetComponent<CurrosDisplay>().OpenOutLine();
                }
                else
                {
                    myCurros.GetComponent<CurrosDisplay>().CloseOutLine();
                }

                #endregion
            }
            else
            {
                foreach (GameObject x in hitObjects)
                {
                    x.GetComponent<EnemyDisplay>().CloseOutLine();
                }

                hitObjects.Clear();
            }
        }
        else
        {
            Cursor.visible = true;
            myCurros.SetActive(false);

            foreach (GameObject x in hitObjects)
            {
                x.GetComponent<EnemyDisplay>().CloseOutLine();
            }

            hitObjects.Clear();

            playerAnimationCtrl.animator.SetBool("isHolding", false);
        }
        
    }

    private void HandleAttack()
    {
        if (!InputHandle.Instance.hold || weaponSlotManager.weapon==null || playerAnimationCtrl.animator.GetBool("isInteracting"))
            return;

        BulletManager bulletManager = transform.GetComponentInChildren<BulletManager>();
        if(InputHandle.Instance.attack)
        {
            if (bulletManager != null)
            {
                if (bulletManager.ShootBullet() && timeReload < 0)
                {
                    bulletManager.RemoveBullet();
                    bulletManager.particle.Play();
                    weaponAudio.PlayOneShot(weaponSlotManager.weapon.SoundAttack);
                    foreach (GameObject x in hitObjects)
                    {
                        x.GetComponent<EnemyStats>().TakeDamage(weaponSlotManager.weapon.atkBonus);
                    }

                    timeReload = bulletManager.weaponObject.speedFire;
                }
                else
                {
                  //  weaponAudio.PlayOneShot(weaponSlotManager.weapon.SoundCantAttack);
                }
            }
            else
            {
                playerAnimationCtrl.PlayerTargetAnimation("Weapon Attack", true);
            }
        }
        
        timeReload -= Time.deltaTime;
    }
}
