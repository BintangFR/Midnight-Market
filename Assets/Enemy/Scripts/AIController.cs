using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class AIController : MonoBehaviour
{
    public static AIController Instance { get; private set; }

    public Transform player;
    public NavMeshAgent agent;
    // public float sightRange;
    // private bool playerInSight = false;
    public LayerMask whatIsPlayer;
    public PlayerController playerController;
    public Vector3 range;

    [SerializeField] private float normalSpeed;
    [SerializeField] private float sprintSpeed;
    
    public Transform[] waypoints;
    private Animator anim;

    [SerializeField] private EnemyState currentState;
    [SerializeField] private float losingPlayerTimer = 0f;
    [SerializeField] private AIVision aiVision;
    private int currentWaypointIndex = 0;

    public enum EnemyState
    {
        non_active,
        activating,
        idle ,
        seeking ,
        chasing ,
        attacking 
    }

    //change enemy state for unity events 
    public void ChangeState(){
        currentState = EnemyState.seeking;
    }
    public void Testing(){
        Debug.Log("Test");
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        ChangeEnemyState(EnemyState.seeking);
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
            agent.speed = normalSpeed;
            agent.stoppingDistance = 0f;

            if (!agent.hasPath && waypoints.Length > 0)
            {
                anim.SetBool("Seek", true);
                MoveToWaypoint();
            }

            if (aiVision.GetCanSeePlayer())
            {
                ChangeEnemyState(EnemyState.chasing);
            }
        }
        else if (currentState == EnemyState.chasing)
        {

            agent.stoppingDistance = 1.5f;
            agent.SetDestination(player.position);
            agent.speed = sprintSpeed;
            anim.SetFloat("Speed", agent.speed);
            anim.SetBool("Seek", false);

            if (aiVision.GetLastAwareTimer() >= losingPlayerTimer)
            {
                ChangeEnemyState(EnemyState.seeking);

            }

            if (Vector3.Distance(transform.position, player.position) <= 1.9f)
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
        ChangeEnemyState(EnemyState.attacking);
        anim.SetTrigger("Attack");

        AudioManager.Instance.PlaySFX("Attack Before", transform.position);

        yield return new WaitForSeconds(1f);

        AudioManager.Instance.PlaySFX("Attack After", transform.position);

        if (aiVision.GetLastAwareTimer() >= losingPlayerTimer)
        {
            ChangeEnemyState(EnemyState.seeking);
        }
        else
        {
            ChangeEnemyState(EnemyState.chasing);
        }
    }

    private void MoveToWaypoint()
    {
        int randomIndex;
        do
        {
            randomIndex = UnityEngine.Random.Range(0, waypoints.Length);
        }
        while (currentWaypointIndex == randomIndex);
        currentWaypointIndex = randomIndex;
        
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    public void ChangeEnemyState(EnemyState newState)
    {
        currentState = newState;

        AudioManager.Instance.StopEnemy();
        AudioManager.Instance.ChangeEnemyState(newState);

        if (newState == EnemyState.chasing || newState == EnemyState.seeking)
        {
            AudioManager.Instance.PlayEnemy("Robot Footstep");
        }
    }

    public AIVision GetAIVision()
    {
        return aiVision;
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
