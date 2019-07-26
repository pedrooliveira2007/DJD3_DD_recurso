using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
[RequireComponent(typeof(PlayerController))]
public class PlayerStatus : MonoBehaviour
{
    internal bool isAlive = true;
    internal int baseDamage = 10;
    internal int baseHeal = 30;
    [SerializeField]
    internal Collider weapon;
    [SerializeField]
    internal GameObject stomp;
    [SerializeField]
    internal Collider thrust;
    [SerializeField]
    internal GameObject thrustEffects;
    private UIManager UI;
    internal int IFrames;
    internal bool blocking = false;

    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Collider>();
    }
    private void FixedUpdate()
    {
        if(IFrames > 0 && !blocking)
        {
            IFrames--;
        }
    }

    void Update()
    {
        if (UI.hp <= 0 && !GetComponentInParent<PlayerController>().
                            pAnim.GetComponent<Animator>().GetBool("isDead"))
        {
            PlayerController p = GetComponentInParent<PlayerController>();
            p.pAnim.GetComponent<Animator>().SetTrigger("DeadTrigger");
            p.pAnim.GetComponent<Animator>().SetBool("isDead",true);
        }
    }

    public void AttackStart()
    {
        
        weapon.enabled = true;
    }

    public void AttackEnd()
    {
        weapon.enabled = false;
    }

    public void StompStart()
    {
        stomp.SetActive(true);
    }

    public void ThrustStart()
    {
        thrust.enabled = true;
        thrustEffects.SetActive(true);
    }
    public void ThrustEnd()
    {
        thrust.enabled = false;
        thrustEffects.SetActive(false);
    }

    



    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Damage Source")
        {
            if (blocking)
            {
                UI.AdjustNode(1);
                IFrames = 45;
            }
            else if(IFrames > 0)
            {

            }
            else
            {
                int damageTaken = -other.GetComponentInParent<EnemyStatus>().baseDamage;
                isAlive = UI.AdjustHP(damageTaken);
                IFrames = 15;
            }
        }

        if (other.tag == "Enemy Projectile")
        {
            if (blocking)
            {
                UI.AdjustNode(1);
                IFrames = 45;
                Destroy(other.gameObject);
            }
            else if (IFrames > 0)
            {
                Destroy(other.gameObject);
            }
            else
            {
                int damageTaken = -other.GetComponentInParent<Projectile>().baseDamage;
                isAlive = UI.AdjustHP(damageTaken);
                Destroy(other.gameObject);
            }

        }

    }

    public void ChangeHeal(int value)
    {
        baseHeal += value;
    }
}