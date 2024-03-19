using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int id;
    public float x;
    public float y;
    public List<Node> neighbors;

    // Constructor para inicializar la lista de vecinos
    public Node()
    {
        neighbors = new List<Node>();
    }

    void Start()
    {
        // No es necesario inicializar la lista aquí si ya se inicializó en el constructor  
    }
}
