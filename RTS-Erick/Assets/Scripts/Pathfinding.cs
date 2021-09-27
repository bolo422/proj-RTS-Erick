using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Grid grid; //referencia do script como grid
    public UnityMovement Unit;
    public Transform StartPosition; //posi��o inicial do pathfinding no mundo
    public Transform TargetPosition; //objetivo do pathfinding no mundo


    // Start is called before the first frame update
    void Awake()
    {
       // grid = GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            FindPath(StartPosition.position, TargetPosition.position);
        }
    }

    void FindPath(Vector3 a_StartPos, Vector3 a_TargetPos)
    {
        Node StartNode = grid.NodeFromWorldPosition(a_StartPos); //referencia o nodo mais pr�ximo do transform colocado no inspector
        Node TargetNode = grid.NodeFromWorldPosition(a_TargetPos); //referencia o nodo mais pr�ximo do transform colocado no inspector

        List<Node> OpenList = new List<Node>();
        HashSet<Node> ClosedList = new HashSet<Node>();

        OpenList.Add(StartNode);

        while(OpenList.Count > 0) // enquanto tiver algum nodo na openlist
        {
            Node CurrentNode = OpenList[0]; // cria um nodo e coloca ele como primeiro item na openlist
            for(int i = 1; i < OpenList.Count; i++) //faz loop atrav�s da open list, come�ando no 2� elemento
            {
                if (OpenList[i].FCost < CurrentNode.FCost || OpenList[i].FCost == CurrentNode.FCost && OpenList[i].hCost < CurrentNode.hCost) //se o Fcost do objeto � menor ou igual ao do nodo atual
                {
                    CurrentNode = OpenList[i];
                }
            }
            OpenList.Remove(CurrentNode);
            ClosedList.Add(CurrentNode);

            if(CurrentNode == TargetNode) //se o nodo atual � o objetivo, calcula o caminho final
            {
                GetFinalPath(StartNode, TargetNode);
                
            }

            foreach (Node NeighbordNode in grid.GetNeighboringNodes(CurrentNode)) //faz loop atrav�s dos vizinhos do nod oatual
            {
                if (!NeighbordNode.IsWall || ClosedList.Contains(NeighbordNode)) //se o vizinho � uma parede ou j� foi verificado, pula para o pr�ximo
                { continue; }

                int MoveCost = CurrentNode.gCost + GetManhattenDistance(CurrentNode, NeighbordNode); // calcular o Fcost do nodo

                if (MoveCost < NeighbordNode.FCost || !OpenList.Contains(NeighbordNode)) //se o FCost � maior que o gcost ou n�o est� na openList
                {
                    NeighbordNode.gCost = MoveCost; // colocar o valor do fCost no gCost
                    NeighbordNode.hCost = GetManhattenDistance(NeighbordNode, TargetNode); //setar o hCost
                    NeighbordNode.Parent = CurrentNode; //seta o nodo pai para calcular o caminho final

                    if (!OpenList.Contains(NeighbordNode)) //se o vizinho n�o est� na openlist
                    {
                        OpenList.Add(NeighbordNode); //adiciona o vizinho na open list
                    }
                }
            }


        }

        void GetFinalPath(Node a_StartingNode, Node a_EndNode)
        {
            List<Node> FinalPath = new List<Node>(); //list para guardar o caminho final
            Node CurrentNode = a_EndNode;

            while(CurrentNode != a_StartingNode) //j� que a lista � verificada de tr�s para frente, o loop continua enquanto o nodo atual for diferente do nodo inicial
            {
                //Debug.Log("X: " + CurrentNode.Position.x + "\nZ: " + CurrentNode.Position.z);
                FinalPath.Add(CurrentNode);
                CurrentNode = CurrentNode.Parent;
            }

            FinalPath.Reverse(); //reverter a lista

            grid.FinalPath = FinalPath; //enviar o resultado para o grid desenhar
           // Unit.Path = FinalPath;
        }

    }

    int GetManhattenDistance(Node a_nodeA, Node a_nodeB)
    {
        int ix = Mathf.Abs(a_nodeA.gridX - a_nodeB.gridX);
        int iy = Mathf.Abs(a_nodeA.gridY - a_nodeB.gridY);

        return ix + iy;
    }
}
