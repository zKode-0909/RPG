using UnityEngine;

public class GameBootStrapper : MonoBehaviour
{
    QuestLogFactory questLogFactory;
    QuestLogRegistry questLogRegistry;

    PlayerFactory playerFactory;
    PlayerRegistry playerRegistry;
    PlayerSpawner playerSpawner;


    QuestBootstrapper questBootstrapper;
    PlayerBootStrapper playerBootStrapper;

    [SerializeField] PlayerInputReader input;

    [SerializeField] QuestGiverDB questGiverDB;
    [SerializeField] QuestDB questDB;

    [SerializeField] SimulationManager simulationManager;

    [SerializeField] HumanPlayerMB HumanPlayer;
    [SerializeField] PlayerMB playerTemplate; 

    SelectionManager selectionManager;
    PlayerLocationManager playerLocationManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  

        questLogRegistry = new QuestLogRegistry();
        questLogFactory = new QuestLogFactory(questLogRegistry);

        playerRegistry = new PlayerRegistry();
        playerLocationManager = new PlayerLocationManager();
        playerLocationManager.Initialize();
        playerSpawner = new PlayerSpawner(playerLocationManager);
        playerFactory = new PlayerFactory(playerRegistry, questLogFactory,playerTemplate,playerSpawner);
        


        questBootstrapper = new QuestBootstrapper();    
        playerBootStrapper = new PlayerBootStrapper();

        questBootstrapper.BootStrap(questLogFactory,questLogRegistry,questDB,questGiverDB);
        playerBootStrapper.Bootstrap(playerFactory,playerRegistry);

        selectionManager = new SelectionManager();

        selectionManager.Initialize();

        simulationManager.Initialize(playerFactory,playerRegistry,playerSpawner,HumanPlayer,playerLocationManager);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
