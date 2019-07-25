
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStatus))]
[RequireComponent(typeof(Rigidbody))]

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    internal string mobName = "Dustsnare";
    [SerializeField]
    internal int maxHP = 100;
    [SerializeField]
    internal int HP = 100;
    [SerializeField]
    internal float meleeDistance = 5f;
    [SerializeField]
    internal float rangedDistance = 20f;


    internal bool hit = false;
    [SerializeField]
    internal bool isRanged = false;
    internal bool attacking = false;
    internal bool rangedAttack = false;


    [SerializeField]
    internal GameObject projectile;
    [SerializeField]
    internal GameObject indicator;
    private Animator anim;
    private Transform target;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        Physics.IgnoreCollision(this.GetComponent<BoxCollider>(),
            target.GetComponent<CharacterController>().GetComponent<Collider>());
        if (isRanged)
            agent.stoppingDistance = rangedDistance;
        else
            agent.stoppingDistance = meleeDistance;
    }

    // Update is called once per frame
    void Update()
    {

        if (HP <= 0 && !anim.GetBool("dead"))
        {
            HP = 0;
            anim.SetInteger("condition", 0);
            anim.SetTrigger("dead trigger");
            anim.SetBool("dead",true);
            GetComponentInChildren<HPBarFollow>().Kill();
            Destroy(this.gameObject, 2f);
        }

        else if (anim.GetBool("dead") == false && hit == false && !attacking)
        {
            float distance = Vector3.Distance(target.position, transform.position);

            agent.SetDestination(target.position);
            anim.SetInteger("condition", 1);//walk animation



            if (distance <= agent.stoppingDistance && isRanged)
            {

                LookAt(); //look at the target
                AttackRanged(); //atack the target

            }


            if (distance <= meleeDistance)
            {

                LookAt(); //look at the target
                Attack(); //atack the target

            }

            if (attacking || rangedAttack)
            {
                agent.isStopped = true;
            }

            else
            {
                agent.isStopped = false;
            }
        }

    }

    void LookAt()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    private void Attack()
    {
        attacking = true;
        anim.SetInteger("condition", 3);

    }

    private void AttackRanged()
    {
        anim.SetInteger("condition", 5);
    }
}
