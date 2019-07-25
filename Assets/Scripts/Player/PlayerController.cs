using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStatus))]
public class PlayerController : MonoBehaviour
{
    //mask where the mouse can access the world position
    [SerializeField]
    private LayerMask moveMask;

    [SerializeField]
    private AbilityManager abiltyManager;

    private PlayerStatus stat;

    private UIManager UI;

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private CharacterController _controller;
    [SerializeField]
    private Vector3 _velocity;

    internal bool attackMove = false;
    [SerializeField]
    internal Player_Animator pAnim;
    internal bool attacking = false;


    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        cam = Camera.main;
        anim = GetComponentInChildren<Animator>();
        _controller = GetComponent<CharacterController>();
        _velocity = Vector3.zero;
        anim.SetBool("Idle", true);
        stat = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateVelocity();
        UpdatePosition();
    }
    private void Update()
    {
        UpdateAttack();
    }


    private void UpdateVelocity()
    {


        if (Input.GetMouseButton(0))
        {
            if (!attacking && !anim.GetBool("isDead"))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000, moveMask))
                {
                    transform.LookAt(new Vector3(hit.point.x, 0, hit.point.z));
                    transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
                    _velocity.z = 20f;

                    anim.SetBool("isWalking", true);
                    anim.SetBool("Idle", false);
                }
            }

            else
            {
                _velocity.z = 0f;
                anim.SetBool("Idle", true);
                anim.SetBool("isWalking", false);
            }
        }



        else
        {
            _velocity.z = 0f;
            anim.SetBool("Idle", true);
            anim.SetBool("isWalking", false);
        }

        if (pAnim.attackMove)
        {
            _velocity.z = pAnim.speed;
        }
    }

    private void UpdatePosition()
    {
        Vector3 motion = _velocity * Time.deltaTime;
        _controller.Move(transform.TransformVector(motion));
    }


    private bool MousePoint()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, moveMask))
        {
            transform.LookAt(new Vector3(hit.point.x, 0, hit.point.z));
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
            return true;
        }
        return false;
    }


    private void UpdateAttack()
    {
            if (!attacking)
            {
                // Horizontal Breaker.
                if (Input.GetButtonDown("1st Ability"))
                {
                    if (abiltyManager.FirstTrigger())
                    {

                    stat.baseDamage = 10;
                    // If the player points outside of the map it will not
                    // only consume the ability's required resources but
                    // also goes into cooldown. Done like this for performance.
                    if (MousePoint())
                        {
                        attacking = true;
                            anim.SetInteger("Ability", 1);
                        }
                    }
                }

                // Duelist Advance.
                else if (Input.GetButtonDown("2nd Ability"))
                {
                    if (abiltyManager.SecondTrigger())
                    {
                    stat.baseDamage = 15;
                    // If the player points outside of the map it will not
                    // only consume the ability's required resources but
                    // also goes into cooldown. Done like this for performance.
                    if (MousePoint())
                        {
                            attacking = true;
                            anim.SetInteger("Ability", 2);
                        }
                    }
                }

                // Guardian's Will.
                else if (Input.GetButtonDown("3rd Ability"))
                {
                    if (abiltyManager.ThirdTrigger())
                    {
                        // If the player points outside of the map it will not
                        // only consume the ability's required resources but
                        // also goes into cooldown. Done like this for performance.
                        if (MousePoint())
                        {
                            attacking = true;
                            anim.SetInteger("Ability", 3);
                        }
                    }
                }

                // Healing Faith.
                else if (Input.GetButtonDown("4th Ability"))
                {
                    if (abiltyManager.FourthTrigger())
                    {
                        // If the player points outside of the map it will not
                        // only consume the ability's required resources but
                        // also goes into cooldown. Done like this for performance.
                        if (MousePoint())
                        {
                            attacking = true;
                            anim.SetInteger("Ability", 4);
                        }
                    }
                }

                // Leapard's Dash.
                else if (Input.GetButtonDown("5th Ability"))
                {
                    if (abiltyManager.FifthTrigger())
                    {
                    stat.baseDamage = 18;
                    // If the player points outside of the map it will not
                    // only consume the ability's required resources but
                    // also goes into cooldown. Done like this for performance.
                    if (MousePoint())
                        {
                            attacking = true;
                            anim.SetInteger("Ability", 5);
                        }
                    }
                }

                // Sky's Wrath.
                else if (Input.GetButtonDown("6th Ability"))
                {
                    if (abiltyManager.SixthTrigger())
                    {

                    stat.baseDamage = 20;
                    // If the player points outside of the map it will not
                    // only consume the ability's required resources but
                    // also goes into cooldown. Done like this for performance.
                    if (MousePoint())
                        {
                            attacking = true;
                            anim.SetInteger("Ability", 6);
                        }
                    }
                }

            }

            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("Idle", false);
            }
    }


    internal void Heal()
    {
        UI.AdjustHP(stat.baseHeal);
    }


}
