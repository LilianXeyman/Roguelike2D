using System;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    // Este script servirá para controlar la generación automática por nivel del mapa
    public class CellData
    {
        public bool passable;
    }
    //Variables

    private Tilemap m_Tilemap; //Es una variable privada por eso el m_

    [Header("Variables")]

    [SerializeField]
    int width; //Columnas
    [SerializeField]
    int height; //Filas
    [Tooltip("Generación del mapa caminable")]
    [SerializeField]
    Tile[] groundTiles;
    [Tooltip("Generación del mapa no caminable")]
    [SerializeField]
    Tile[] wallTiles;

    //Información de cada tipo de celda
    private CellData[,] m_BoardData;
    private Grid m_Grid;

    [Header("Jugador")]
    public PlayerController player;

    void Start()
    {
        m_Tilemap = GetComponentInChildren<Tilemap>(); //Accede al hijo del archivo en el que está este script. Concretamente al Tilemap
        m_Grid = GetComponentInChildren<Grid>(); //Te dice la posición en la que "estas"
        m_BoardData = new CellData[width, height];

        //Para la creación del mapa (filas y columnas) se usa un bucle de tal forma que, por cada fila se creen x columnas que contengan untile aleatorio de la paleta de tiles
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                Tile tile;

                m_BoardData[x, y] = new CellData();

                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                {
                    tile = wallTiles[UnityEngine.Random.Range(0, wallTiles.Length)];//En esta línea no reconocía el Random.Range, al añadir delante el UnityEngine fuerzas a que lo use
                                                                                    //Ahora mismos se ha creado el tile pero falta posicionarlo en el espacio
                    m_BoardData[x, y].passable = false; //No puedes pasar por estas casillas
                }
                else//De esta forma estamos haciendo una diferenciación entre el suelo y las paredea 
                {
                    tile = groundTiles[UnityEngine.Random.Range(0, groundTiles.Length)];
                    m_BoardData[x, y].passable = true; //Puedes pasar por estas casillas
                }

                m_Tilemap.SetTile(new Vector3Int(x, y, 0), tile); //Colocas el tile aleatorio en la posición determinada por x e y
            }
        }
        player.Spawn(this, new Vector2Int(1, 1));
    }
    public UnityEngine.Vector3 CellToWorld(Vector2Int cellIndex)
    {
        return m_Grid.GetCellCenterWorld((Vector3Int)cellIndex);
    }
    public CellData GetCellData(Vector2Int cellIndex)
    {
        if (cellIndex.x < 0 || cellIndex.x >= width || cellIndex.y < 0 || cellIndex.y >= height)
        {
            return null;
        }
        
        return m_BoardData[cellIndex.x, cellIndex.y];
    }
}
