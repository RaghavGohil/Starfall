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
        shootEventInstance.SetShootInstances(player.GetComponentsInChildren<Shoot>());
        dashButton.onClick.AddListener(player.GetComponent<PlayerMovement>().Dash);
    }
}
