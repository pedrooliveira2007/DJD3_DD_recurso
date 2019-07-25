using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
[Serializable]
public class EnemyStatus : MonoBehaviour
{
    [SerializeField]
    internal int baseDamage = 5;
    [SerializeField]
    internal int baseShield = 0;
    internal int damageTaken = 0;
    [SerializeField]
    internal Collider damageSource;

    internal EnemyController enemyController;
    internal Animator animator;
    internal GameObject indicator;

    [SerializeField]
    internal HPBarFollow hpbar;
    private int IFrames = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
    }

    private void FixedUpdate()
    {
        IFrames -= 1;
    }


    public void LoseHealth(Collider weapon)
    {
        if (weapon.GetComponent<WeaponStatus>().heavyHit)
        {
           animator.SetInteger("condition", 4);
        }
        else
        {
            animator.SetInteger("condition", 2);
        }
        animator.SetTrigger("hurt");
        float playerDamage = weapon.GetComponentInParent<PlayerStatus>().
                baseDamage * weapon.GetComponent<WeaponStatus>().damageModifier;

        enemyController.HP -= (int)(playerDamage - baseShield);
        enemyController.hit = true;
        hpbar.ChangeHP((int)-(playerDamage - baseShield));
    }



    public void AttackStart()
    {
        damageSource.enabled = true;
    }

    public void AttackEnd()
    {
        damageSource.enabled = false;
    }

    public void Aiming()
    {
        enemyController.attacking = true;
        enemyController.rangedAttack = true;
    }

    public void CreateIndicator()
    {
        Vector3 pos = new Vector3(transform.position.x,
                                    transform.position.y + 1,
                                    transform.position.z);

        indicator = Instantiate(enemyController.indicator, pos, transform.rotation);
    }

    public void SendProjectile()
    {
        
        Vector3 pos = new Vector3(  transform.position.x,
                                    transform.position.y + 1,
                                    transform.position.z);

        Instantiate(enemyController.projectile, pos, transform.rotation);
    }

    public void ClearConditions()
    {
        animator.SetInteger("condition", 0);
        enemyController.hit = false;
        enemyController.attacking = false;
        enemyController.rangedAttack = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            if (IFrames > 0)
            {

            }
            else
            {
                IFrames = 5;
                Debug.Log(other.name);
                LoseHealth(other);
            }
        }
    }

}
