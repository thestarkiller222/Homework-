using UnityEngine;
using System.Collections.Generic;

public class DFSWithAgent : MonoBehaviour
{
    // Clase para representar un nodo en el grafo
    public class Node
    {
        public int id;
        public float x;
        public float y;
        public List<Node> neighbors;

        public Node(int id, float x, float y)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            neighbors = new List<Node>();
        }
    }

    public GameObject nodePrefab; // Prefab para representar visualmente los nodos
    public GameObject agentPrefab; // Prefab para el agente
    public Node originNode;
    public Node targetNode;
    public float agentSpeed = 2f;
    public float nodeDetectionRadius = 1f;
    public float arrivalRadius = 0.5f;

    private List<GameObject> nodeObjects = new List<GameObject>();
    private List<Node> path = new List<Node>();
    private GameObject agent;
    private int pathIndex = 0;

    void Start()
    {
        // Crear nodos con coordenadas aleatorias limitadas a un rango pequeño
        CreateNodes();

        // Visualizar conexiones entre nodos (opcional)
        VisualizeNodeConnections();

        // Ejecutar DFS y obtener el camino desde Origin node hasta Target node
        DFS(originNode, targetNode, new HashSet<Node>(), new List<Node>());

        // Crear el agente en el nodo de origen
        agent = Instantiate(agentPrefab, new Vector3(originNode.x, 0, originNode.y), Quaternion.identity);
    }

    void Update()
    {
        // Mover el agente a lo largo del camino
        if (path.Count > 0)
        {
            MoveAgent();
        }
    }

    void CreateNodes()
    {
        // Crear nodos con coordenadas aleatorias
        for (int i = 0; i < 10; i++)
        {
            float randomX = Random.Range(-5f, 5f); // Rango limitado para visualizar los nodos
            float randomY = Random.Range(-5f, 5f); // Rango limitado para visualizar los nodos
            Node newNode = new Node(i, randomX, randomY);
            originNode = originNode == null ? newNode : originNode;
            nodeObjects.Add(Instantiate(nodePrefab, new Vector3(randomX, 0, randomY), Quaternion.identity));
        }
    }

    void VisualizeNodeConnections()
    {
        // Visualizar conexiones entre nodos (opcional)
        foreach (Node node in nodeObjects)
        {
            foreach (Node neighbor in node.neighbors)
            {
                Debug.DrawLine(new Vector3(node.x, 0, node.y), new Vector3(neighbor.x, 0, neighbor.y), Color.blue, Mathf.Infinity);
            }
        }
    }

    void DFS(Node currentNode, Node targetNode, HashSet<Node> visited, List<Node> currentPath)
    {
        // Implementación de DFS para encontrar el camino desde Origin node hasta Target node
        visited.Add(currentNode);
        currentPath.Add(currentNode);

        if (currentNode == targetNode)
        {
            path = new List<Node>(currentPath);
            return;
        }

        foreach (Node neighbor in currentNode.neighbors)
        {
            if (!visited.Contains(neighbor))
            {
                DFS(neighbor, targetNode, visited, currentPath);
            }
        }

        currentPath.Remove(currentNode);
    }

    void MoveAgent()
    {
        // Mover el agente a lo largo del camino
        Vector3 targetPosition = new Vector3(path[pathIndex].x, 0, path[pathIndex].y);
        float distance = Vector3.Distance(agent.transform.position, targetPosition);

        if (distance <= arrivalRadius)
        {
            // El agente ha llegado al nodo objetivo
            Debug.Log("El agente llegó al nodo " + path[pathIndex].id);
            pathIndex++;

            if (pathIndex >= path.Count)
            {
                // El agente ha llegado al nodo final
                Debug.Log("El agente llegó al nodo final.");
                return;
            }
        }
        else
        {
            // Mover el agente hacia el siguiente nodo en el camino
            Vector3 moveDirection = (targetPosition - agent.transform.position).normalized;
            agent.transform.position += moveDirection * agentSpeed * Time.deltaTime;
        }
    }
}
