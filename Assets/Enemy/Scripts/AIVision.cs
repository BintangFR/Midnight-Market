using UnityEngine;

public class AIVision : MonoBehaviour
{
    [SerializeField] private float viewRange;
    [SerializeField][Range(0, 360)] private float viewAngle;
    [SerializeField] private float hearingRange;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private AIController aiController;

    private bool canSeePlayer;
    private float lastAwareTimer = 0f;
    private Transform player;

    private void Start()
    {
        player = aiController.GetPlayer();
    }

    private void Update()
    {
        FindPlayer();

        lastAwareTimer += Time.deltaTime;
    }

    private void FindPlayer()
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float dstToPlayer = Vector3.Distance(transform.position, player.position);
        bool noObstacleBlockingVision = Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask);

        if (dstToPlayer < hearingRange)
        {
            lastAwareTimer = 0f;

            canSeePlayer = true;

            return;
        }
        else if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
        {
            if (!noObstacleBlockingVision)
            {
                lastAwareTimer = 0f;

                canSeePlayer = true;

                return;
            }
        }

        canSeePlayer = false;
    }

    public Vector3 DirFromAngle(float angleInDegree, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegree += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegree * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegree * Mathf.Deg2Rad));
    }

    public float GetViewRange()
    {
        return viewRange;
    }

    public float GetViewAngle()
    {
        return viewAngle;
    }

    public Transform GetPlayer()
    {
        return player;
    }

    public bool GetCanSeePlayer()
    {
        return canSeePlayer;
    }

    public float GetLastAwareTimer()
    {
        return lastAwareTimer;
    }

    public float GetHearingRange()
    {
        return hearingRange;
    }
}
