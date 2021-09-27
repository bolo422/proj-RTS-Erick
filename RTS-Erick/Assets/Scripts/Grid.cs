using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform StartPosition; //Posição inicial do pathfinding
    public LayerMask WallMask; //layer mask das paredes
    public Vector2 gridWorldSize; //dimensoes do grid em "metros"
    public float nodeRadius; //raio de cada nodo no grid
    public float Distance; //distancia entre os nodos

    Node[,] grid; //array de nodos
    public List<Node> FinalPath; //caminho final

    float nodeDiameter; //diametro do nodo, dobro do raio colocado no inspector
    int gridSizeX, gridSizeY; //dimensões do grid em int

    private void Start()
    {
        nodeDiameter = nodeRadius * 2; // calcula o diametro do nodo
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);// divide o valor do grid em "metros" pelo tamanho do nodo para obter o tamanho do grid em int
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);// divide o valor do grid em "metros" pelo tamanho do nodo para obter o tamanho do grid em int
        CreateGrid(); //criação do grid
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY]; //declaração do array de nodos
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward* gridWorldSize.y / 2; // método para encontrar a posição no mundo do canto inferiro esquerdo do grid
        for (int x = 0; x < gridSizeX; x++) //loop do array de nodos
        {
            for (int y = 0; y < gridSizeY; y++) // loop do array de nodos
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool Wall = true; //declaração de variável e e seta o nodo como parede

                if (Physics.CheckSphere(worldPoint, nodeRadius, WallMask)) // verifica se o nodo não colide com a parede
                {
                    Wall = false; //se não colide, não é uma parede
                }

                grid[x, y] = new Node(Wall, worldPoint, x, y); // crio um novo nodo no array
            }
        }
    }

    public Node NodeFromWorldPosition(Vector3 a_WorldPosition) //função que calcula e retorna o nodo mais próximo da posição informada
    {
        float xpoint = ((a_WorldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float ypoint = ((a_WorldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y);

        xpoint = Mathf.Clamp01(xpoint);
        ypoint = Mathf.Clamp01(ypoint);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xpoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * ypoint);

        return grid[x, y];
    }

    public List<Node> GetNeighboringNodes(Node a_Node) //função que descobre os nodos vizinhos do nodo informado
    {
        List<Node> Neighbor = new List<Node>(); //lista dos vizinhos

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                //if we are on the node tha was passed in, skip this iteration.
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = a_Node.gridX + x;
                int checkY = a_Node.gridY + y;

                //Make sure the node is within the grid.
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    Neighbor.Add(grid[checkX, checkY]); //Adds to the neighbours list.
                }

            }
        }

        return Neighbor; //retorna a lista de vizinhos
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null)
        {
            foreach(Node node in grid)
            {
                if (node.IsWall)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }

                if(FinalPath != null)
                {
                    if (FinalPath.Contains(node))
                    {
                        //Debug.Log("Caminho encontrado");
                        Gizmos.color = Color.red;

                    }
                }

                Gizmos.DrawCube(node.Position, Vector3.one * (nodeDiameter - Distance));
            }
                
        }
    }


}
