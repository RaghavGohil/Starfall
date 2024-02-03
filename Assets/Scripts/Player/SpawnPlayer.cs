using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnPlayer : MonoBehaviour
{


    [Header("References")]
    [SerializeField]
    EventSystem eventSystem;
    
    [SerializeField]
    CinemachineVirtualCamera _camera;

    [SerializeField]
    Joystick joystick;
    [SerializeField]
    StatusText statusTextScript;
    [SerializeField]
    ParticleSystem speedLines;
    [SerializeField]
    Button dashButton;
    [SerializeField]
    ShootEvent shootEventInstance;
    [SerializeField]
    GameObject loseScreen;
    [SerializeField]
    GameObject winScreen;
    [SerializeField] GameManager gameManagerInstance;

    [Header("Player Prefabs")]
    [SerializeField]
    GameObject[] playerPrefabs;

    [HideInInspector]
    public static GameObject player;

    private void Start()
    {
        player = Instantiate(playerPrefabs[PurchaseShipsManager.equippedShipId],transform);
        _camera.Follow = player.transform;
        player.GetComponent<PlayerMovement>().joystick = joystick;
        player.GetComponent<SpeedBlock>().speedLines = speedLines;
        player.GetComponent<SpeedBlock>().statusTextScript = statusTextScript;
        player.GetComponent<FirePowerBlock>().statusTextScript = statusTextScript;
        player.GetComponent<HeartBlock>().statusTextScript = statusTextScript;
        player.GetComponent<DiePlayer>().loseScreen = loseScreen;
        winScreen.GetComponent<WinPanel>().player = player; 
        shootEventInstance.SetShootInstances(player.GetComponentsInChildren<Shoot>());
        dashButton.onClick.AddListener(player.GetComponent<PlayerMovement>().Dash);
        gameManagerInstance.playerMovementInstance = player.GetComponent<PlayerMovement>();
        gameManagerInstance.StartGame();
    }
}
