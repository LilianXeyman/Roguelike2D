using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // En este script se especificará todo aquello relacionado con el movimiento del jugador
    private BoardManager m_Board;//Accede al otro script
    private Vector2Int m_CellPosition;//Guarda la información de la celda en la que estas

    void Update()
    {
        Vector2Int newCellTarget = m_CellPosition;
        bool hasMoved = false;

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            newCellTarget.y += 1;
            hasMoved = true;
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            newCellTarget.y -= 1;
            hasMoved = true;
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            newCellTarget.x += 1;
            hasMoved = true;
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            newCellTarget.x -= 1;
            hasMoved = true;
        }
        if (hasMoved) //Comprueba si la casilla a la que te puedes mover te deja pasar
        {
            BoardManager.CellData cellData = m_Board.GetCellData(newCellTarget);
            if (cellData != null && cellData.passable)
            {
                GameManager.instance.turnManager.Tick();
                MoveTo(newCellTarget); //Cambia la posicion del personaje
            }
        }

    }
    public void Spawn(BoardManager boardManager, Vector2Int cell)
    {
        m_Board = boardManager;

        MoveTo(cell);
    }
    public void MoveTo(Vector2Int cell)
    {
        m_CellPosition = cell;
        transform.position = m_Board.CellToWorld(m_CellPosition);
    }
}

