using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; } //public static GameManager instance;
    [SerializeField]
    public BoardManager boardManager;
    [SerializeField]
    public PlayerController playerControllerer;
    [SerializeField]
    public TurnManager turnManager;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        //Hacer que comience el TurnManager
        turnManager = new TurnManager();
        //Hacer que se cree el mapa
        boardManager.Init();
        //Instanciar el personaje
        playerControllerer.Spawn(boardManager, new Vector2Int(1,1));
    }
}
