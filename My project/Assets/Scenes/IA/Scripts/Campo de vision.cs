using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FieldOfVision : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target; // Referencia al otro agente que se desea perseguir
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en el campo de visi�n es el otro agente
        if (other.transform == target)
        {
            // Inicia la persecuci�n del otro agente
            isChasing = true;
            agent.SetDestination(target.position);
            Invoke("StopChasing", 3f); // Invoca la funci�n StopChasing despu�s de 3 segundos
        }
    }

    void StopChasing()
    {
        // Detiene la persecuci�n y mantiene al agente en su posici�n actual
        isChasing = false;
        agent.ResetPath(); // Detiene el movimiento del agente
    }

    void Update()
    {
        // Si est� persiguiendo al otro agente y el agente objetivo se ha movido, actualiza la posici�n de destino
        if (isChasing && target != null && agent.isActiveAndEnabled)
        {
            agent.SetDestination(target.position);
        }
    }
}
