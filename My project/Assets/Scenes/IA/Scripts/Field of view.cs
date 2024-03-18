using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentController : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    private NavMeshAgent agent;
    private Transform target;
    private bool canSeePlayer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldofviewCheck();
        }
    }

    private void FieldofviewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Player"));

        if (rangeChecks.Length != 0)
        {
            target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, LayerMask.GetMask("Obstacles")))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    void Update()
    {
        if (canSeePlayer)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            agent.ResetPath();
        }
    }
}
