using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int gridX;// posi��o X no Node Array
    public int gridY;// posi��o y no Node Array

    public bool IsWall;// verifica se o Node est� obstru�do
    public Vector3 Position;// a posi��o do Node no mundo

    public Node Parent;// Nodo pai, para o algor�timo poder tra�ar o caminho mais curto

    public int gCost;// o custo para se mover ao pr�ximo espa�o
    public int hCost;// a dist�ncia do objetivo at� este nodo

    public int FCost {  get { return gCost + hCost; } }//uma fun��o simples para setar o valor de F, j� q ele nunca � altero diretamente

    public Node(bool a_IsWall, Vector3 a_Pos, int a_gridX, int a_gridY)
    {
        IsWall = a_IsWall;// verifica se o Node est� obstru�do
        Position = a_Pos;// a posi��o do Node no mundo
        gridX = a_gridX;// posi��o x no Node Array
        gridY = a_gridY;// posi��o y no Node Array
    }
}
