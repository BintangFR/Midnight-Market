using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public static AIController Instance { get; private set; }

    [SerializeField] NavMeshAgent agent;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private EnemyState currentState = EnemyState.non_active;
    [SerializeField] private float losingPlayerTimer = 0f;
    [SerializeField] private AIVision aiVision;
    [SerializeField] private Transform firstActive;
    [SerializeField] private Transform secondActive;
    [SerializeField] private GameObject attackCollider;

    private Transform player;
    public LayerMask whatIsPlayer;
    private Animator anim;
    private NavMeshAgent navMeshAgent;
    private int currentWaypointIndex = 0;

    public enum EnemyState
    {
        non_active,
        activating,
        idle,
        seeking,
        chasing,
        attacking
    }

    //change enemy state for unity events 
    /*
    public void ChangeState(){
        currentState = EnemyState.seeking;
    }
    public void Testing(){
        Debug.Log("Test");
    }
    */

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = false;
        attackCollider.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        AudioManager.Instance.StopEnemy();
    }

    private void OnDisable()
    {
        AudioManager.Instance.StopEnemy();
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
            if (aiVision.GetCanSeePlayer())
            {
                ChangeEnemyState(EnemyState.chasing);
            }
        }
        else if (currentState == EnemyState.seeking)
        {
            agent.stoppingDistance = 0f;
            anim.SetFloat("Speed", agent.speed);
            agent.speed = normalSpeed;

            if (!agent.hasPath && waypoints.Length > 0)
            {
                anim.SetBool("Seek", true);
                MoveToWaypoint();
            }

            if (aiVision.GetCanSeePlayer())
            {
                ChangeEnemyState(EnemyState.chasing);
            }

            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(waypoints[currentWaypointIndex].position.x, 0, waypoints[currentWaypointIndex].position.z)) < 0.5f)
            {
                MoveToWaypoint();
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

        yield return new WaitForSeconds(0.4f);

        attackCollider.SetActive(true);

        yield return new WaitForSeconds(0.6f);

        attackCollider.SetActive(false);

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
        currentWaypointIndex = Random.Range(0, waypoints.Length); ;

        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    public void ActivateEnemy(bool first)
    {
        anim.enabled = false;
        navMeshAgent.enabled = false;

        if (first)
        {
            transform.position = firstActive.position;
            transform.rotation = firstActive.rotation;
        }
        else
        {
            transform.position = secondActive.position;
            transform.rotation = secondActive.rotation;
        }

        anim.enabled = true;
        navMeshAgent.enabled = true;

        ChangeEnemyState(EnemyState.idle);
    }

    public void ChangeEnemyState(EnemyState newState)
    {
        currentState = newState;

        if (newState == EnemyState.chasing || newState == EnemyState.seeking)
        {
            anim.SetBool("Seek", newState == EnemyState.chasing? true : false);
            AudioManager.Instance.PlayEnemy("Robot Footstep");
        }
        else
        {
            AudioManager.Instance.StopEnemy();
        }

        AudioManager.Instance.ChangeEnemyState(newState);
    }

    public AIVision GetAIVision()
    {
        return aiVision;
    }

    public Transform GetPlayer()
    {
        return player;
    }

    public void ApproachPlayerSound(Vector3 position)
    {
        if (currentState == EnemyState.seeking)
        {
            agent.SetDestination(position);
            Debug.Log("Cheery Can Hear You");
        }
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
