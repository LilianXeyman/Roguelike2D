using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    // Este script servirá para controlar la generación automática por nivel del mapa
    //Variables

    private Tilemap m_Tilemap; //Es una variable privada por eso el m_

    [Header("Variables")]

    [SerializeField]
    int width; //Columnas
    [SerializeField]
    int height; //Filas
    [Tooltip("Generación del mapa")]
    [SerializeField]
    Tile[] groundTiles;
    [SerializeField]
    Tile[] wallTiles;
    void Start()
    {
        m_Tilemap = GetComponentInChildren<Tilemap>(); //Accede al hijo del archivo en el que está este script. Concretamente al Tilemap

        //Para la creación del mapa (filas y columnas) se usa un bucle de tal forma que, por cada fila se creen x columnas que contengan untile aleatorio de la paleta de tiles
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                Tile tile;

                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                {
                    tile = wallTiles[UnityEngine.Random.Range(0, wallTiles.Length)];//En esta línea no reconocía el Random.Range, al añadir delante el UnityEngine fuerzas a que lo use
                                                                                    //Ahora mismos se ha creado el tile pero falta posicionarlo en el espacio
                }
                else//De esta forma estamos haciendo una diferenciación entre el suelo y las paredea 
                {
                    tile = groundTiles[UnityEngine.Random.Range(0, groundTiles.Length)];
                }

                m_Tilemap.SetTile(new Vector3Int(x, y, 0), tile); //Colocas el tile aleatorio en la posición determinada por x e y
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
