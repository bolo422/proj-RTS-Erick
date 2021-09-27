using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int gridX;// posição X no Node Array
    public int gridY;// posição y no Node Array

    public bool IsWall;// verifica se o Node está obstruído
    public Vector3 Position;// a posição do Node no mundo

    public Node Parent;// Nodo pai, para o algorítimo poder traçar o caminho mais curto

    public int gCost;// o custo para se mover ao próximo espaço
    public int hCost;// a distância do objetivo até este nodo

    public int FCost {  get { return gCost + hCost; } }//uma função simples para setar o valor de F, já q ele nunca é altero diretamente

    public Node(bool a_IsWall, Vector3 a_Pos, int a_gridX, int a_gridY)
    {
        IsWall = a_IsWall;// verifica se o Node está obstruído
        Position = a_Pos;// a posição do Node no mundo
        gridX = a_gridX;// posição x no Node Array
        gridY = a_gridY;// posição y no Node Array
    }
}
