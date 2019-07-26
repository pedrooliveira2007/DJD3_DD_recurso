using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animator : MonoBehaviour
{
    public float speed = 0;
    public bool attackMove = false;

    [SerializeField]
    internal PlayerStatus player;
    [SerializeField]
    private PlayerController controll;
    [SerializeField]
    internal TrailRenderer trail;

    private void Start()
    {
        controll = player.GetComponent<PlayerController>();
    }
    public void AttackStart()
    {
        trail.emitting = true;
        player.AttackStart();
    }

    public void AttackEnd()
    {
        trail.emitting = false;
        player.AttackEnd();
    }

    public void StompStart()
    {
        player.StompStart();
    }


    public void ThrustStart()
    {
        player.ThrustStart();
    }

    public void ThrustEnd()
    {
        player.ThrustEnd();
    }

    public void BlockStart()
    {
        player.blocking = true;
    }

    public void BlockEnd()
    {
        player.blocking = false;
    }

    public void UnlockPlayer()
    {
        GetComponent<Animator>().SetInteger("Ability", 0);
        controll.attacking = false;
    }

    public void StartMovement()
    {
        controll.attackMove = true;
    }

    public void EndMovement()
    {
        controll.attackMove = false;
    }

    public void Heal()
    {
        controll.Heal();
    }
}
