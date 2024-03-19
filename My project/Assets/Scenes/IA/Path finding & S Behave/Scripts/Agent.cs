using UnityEngine;

public class Agent : MonoBehaviour
{
    public float agentSpeed = 2f;
    public float arrivalRadius = 0.5f;

    private Vector3[] path;
    private int targetIndex;

    public void SetPath(Vector3[] newPath)
    {
        path = newPath;
        targetIndex = 0;
    }

    void Update()
    {
        if (path != null && targetIndex < path.Length)
        {
            Vector3 targetPosition = path[targetIndex];
            float distance = Vector3.Distance(transform.position, targetPosition);

            if (distance <= arrivalRadius)
            {
                targetIndex++;

                if (targetIndex >= path.Length)
                {
                    Debug.Log("El agente llegó al nodo final.");
                    return;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, agentSpeed * Time.deltaTime);
            }
        }
    }
}