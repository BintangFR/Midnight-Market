using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class AIController : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    // public float sightRange;
    // private bool playerInSight = false;
    public LayerMask whatIsPlayer;
    public PlayerController playerController;
    public Vector3 range;
    public Transform[] waypoints;
    private Animator anim;

    [SerializeField] private EnemyState currentState;
    [SerializeField] private float losingPlayerTimer = 0f;
    [SerializeField] private AIVision aiVision;
    private int currentWaypointIndex = 0;

    enum EnemyState
    {
        non_active,
        activating,
        idle,
        seeking,
        chasing,
        attacking
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        currentState = EnemyState.seeking;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentState == EnemyState.non_active)
        {
            // non_active logic
        }
        else if (currentState == EnemyState.activating)
        {
            // activating logic
        }
        else if (currentState == EnemyState.idle)
        {
            // idle logic
        }
        else if (currentState == EnemyState.seeking)
        {
            anim.SetFloat("Speed", agent.speed);
            agent.speed = 5.0f;

            agent.stoppingDistance = 0f;

            if (!agent.hasPath && waypoints.Length > 0)
            {
                MoveToWaypoint();
            }

            if (aiVision.GetCanSeePlayer())
            {
                currentState = EnemyState.chasing;
            }
        }
        else if (currentState == EnemyState.chasing)
        {
            agent.stoppingDistance = 1.5f;
            agent.SetDestination(player.position);
            agent.speed = 10.0f;
            anim.SetFloat("Speed", agent.speed);

            if (aiVision.GetLastAwareTimer() >= losingPlayerTimer)
            {
                currentState = EnemyState.seeking;
            }

            if (Vector3.Distance(transform.position, player.position) <= 1.5f)
            {
                StartCoroutine(Attack());
            }
        }
        else if (currentState == EnemyState.attacking)
        {
            // attacking logic
        }
    }

    private IEnumerator Attack()
    {
        Debug.Log(Vector3.Distance(transform.position, player.position));

        currentState = EnemyState.attacking;
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(1f);

        if (aiVision.GetCanSeePlayer())
        {                
            currentState = EnemyState.chasing;
        }
        else
        {
            currentState = EnemyState.seeking;
        }
    }

    private void MoveToWaypoint()
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, waypoints.Length);
        }
        while (currentWaypointIndex == randomIndex);

        currentWaypointIndex = randomIndex;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //give damage to player
            playerController.TakeDamage();
        }
    }

    private void OnDrawGizmosSelected()
    {
       
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,sightRange);
    }
    */
}
