using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class shadowManController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private NavMeshAgent navMeshAgent;
    private Animator animator;
    public static shadowManController Instance { get; private set; }
    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = false;    
    }
    void Start()
    {
        navMeshAgent.enabled = true;
        animator = GetComponent<Animator>();
    }
    

    private void Update() {
        SetAnimatorSpeed();
    }
    // Update is called once per frame
    public void SetAnimatorSpeed(){
        animator.SetFloat("Speed",navMeshAgent.speed);
        Debug.Log(navMeshAgent.speed);
    }
}
