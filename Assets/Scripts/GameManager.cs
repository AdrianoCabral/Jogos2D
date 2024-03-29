using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> StateChanged;

    private GameObject player;

    private Scene _previousScene;

    public GameObject BarreiraVilaFolha;
    public GameObject BarreiraVilaPedra;
    public GameObject BarreiraArvoreSagrada;
    public GameObject BarreiraEntradaVilaPedra;
    public GameObject TriggerConversaChefes;

    public NpcDialogue npcDialogue;

    private GameObject chefeTriboPedra;
    private GameObject chefeTriboFolha;
    public CinemachineVirtualCamera _camera;

    public GameObject npcPedra1;
    public GameObject npcPedra2;
    public GameObject npcPedra3;

    public GameObject npcFolha1;
    public GameObject npcFolha2;
    public GameObject npcFolha3;

    private bool miniGameEnded = false;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        player = GameObject.FindWithTag("Player");
        chefeTriboPedra = GameObject.FindWithTag("ChefeTriboPedra");
        chefeTriboFolha = GameObject.FindWithTag("ChefeTriboFolha");
        //DontDestroyOnLoad(this.gameObject);
        UpdateGameState(GameState.InicioGame);
    }

    // private void Start()
    // {
    //     if (!miniGameEnded)
    //     {
    //     UpdateGameState(GameState.InicioGame);

    //     }else
    //     {
    //         UpdateGameState(GameState.posMiniGame);
    //     }
    // }


    // Update is called once per frame
    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch(newState)
        {
            case GameState.InicioGame:
                TriggerConversaChefes.GetComponent<Collider2D>().enabled = false;
                break;
            case GameState.FalouComChefeFolhaPraia:
                BarreiraVilaFolha.GetComponent<Collider2D>().enabled = false;
                break;
            case GameState.FalouComChefeFolhaVila:
                chefeTriboFolha.GetComponent<NpcDialogue>().SetconversationStartNode("repetindoConversaVila");
                BarreiraVilaPedra.GetComponent<Collider2D>().enabled = false;
                break;
            case GameState.ChegouVilaPedra:
                player.transform.position = new Vector3(-72, 40, 0);
                BarreiraEntradaVilaPedra.GetComponent<Collider2D>().enabled = false;
                break;
            case GameState.posMiniGame:
                chefeTriboPedra.GetComponent<NpcDialogue>().enabled = true;
                chefeTriboPedra.GetComponent<NpcDialogue>().SetconversationStartNode("posMiniGame"); 
                break;
            case GameState.conversaChefesNaPraia:
                player.transform.position = new Vector3(-87, 20, 0);
                chefeTriboPedra.transform.position = new Vector3(-13, 9, 0);
                chefeTriboFolha.transform.position = new Vector3(-2, 9, 0);
                chefeTriboPedra.GetComponent<NpcDialogue>().SetconversationStartNode("conversaEntreChefes");
                TriggerConversaChefes.GetComponent<Collider2D>().enabled = true;
                FindObjectOfType<Player2>().speed = 15f;
                // SceneManager.LoadScene(2);
                break;
            case GameState.posConversaChefes:
                TriggerConversaChefes.GetComponent<Collider2D>().enabled = false;
                chefeTriboPedra.transform.position = new Vector3(-86, 50, 0);
                npcPedra2.transform.position = new Vector3(-74, 52, 0);
                npcPedra3.transform.position = new Vector3(-92,52, 0);
                player.GetComponent<SpriteRenderer>().enabled = false;
                _camera.LookAt = chefeTriboPedra.transform;
                _camera.Follow = chefeTriboPedra.transform;
                StartCoroutine(WaiterDialogue("DeVoltaATriboDosPedras"));
                break;
            case GameState.reuniaoDasTribos:
                chefeTriboPedra.transform.position = new Vector3(-13, 9, 0);
                npcPedra1.transform.position = new Vector3(-16, -6, 0);
                npcPedra2.transform.position = new Vector3(-24, -2, 0);
                npcPedra3.transform.position = new Vector3(-24, 5.5f, 0);

                npcFolha1.transform.position = new Vector3(4.5f, -6, 0);
                npcFolha2.transform.position = new Vector3(9, -2, 0);
                npcFolha3.transform.position = new Vector3(2, 5.5f, 0);

                player.transform.position = new Vector3(-7.5f,0, 0);
                player.GetComponent<SpriteRenderer>().enabled = true;
                _camera.LookAt = player.transform;
                _camera.Follow = player.transform;
                StartCoroutine(WaiterDialogue("ReuniaoDasTribos"));
                break;
            case GameState.ultimaConversa:
                npcPedra1.transform.position = new Vector3(-86, 50, 0);
                npcPedra2.transform.position = new Vector3(-86, 50, 0);
                npcPedra3.transform.position = new Vector3(-86, 50, 0);

                npcFolha1.transform.position = new Vector3(-86, 50, 0);
                npcFolha2.transform.position = new Vector3(-86, 50, 0);
                npcFolha3.transform.position = new Vector3(-86, 50, 0);

                BarreiraVilaFolha.GetComponent<Collider2D>().enabled = true;
                BarreiraVilaPedra.GetComponent<Collider2D>().enabled = true;


                StartCoroutine(WaiterDialogue("UltimaConversa"));
                break;
            case GameState.creditos:
                StopAllCoroutines();
                BarreiraArvoreSagrada.GetComponent<Collider2D>().enabled = false;
                break;
            default: 
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        StateChanged?.Invoke(newState);
    }


    public void StartMinigame()
    {
        chefeTriboPedra.GetComponent<NpcDialogue>().enabled = false;
        chefeTriboPedra.GetComponent<NpcDialogue>().pararConversa();
        // Store the current scene
        _previousScene = SceneManager.GetActiveScene();

        // Load the minigame scene on top of the current scene
        SceneManager.LoadScene(3, LoadSceneMode.Additive);

        // Set the minigame scene as the active scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(3));
    }

    public void EndMinigame()
    {
        // Unload the minigame scene
        SceneManager.UnloadSceneAsync(3);

        // Set the previous scene as the active scene
        SceneManager.SetActiveScene(_previousScene);

        GameManager.Instance.UpdateGameState(GameManager.GameState.posMiniGame);
    }

    IEnumerator WaiterDialogue(String dialogue)
    {
        npcDialogue.SetconversationStartNode(dialogue);
        yield return new WaitForSeconds(1);
        npcDialogue.StartConversa();
    }


    public enum GameState
    {
        InicioGame,
        FalouComChefeFolhaPraia,
        FalouComChefeFolhaVila,
        ChegouVilaPedra,
        posMiniGame,
        conversaChefesNaPraia,
        posConversaChefes,
        reuniaoDasTribos,
        ultimaConversa,
        creditos
    }
}
