using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnPlayer : MonoBehaviour
{
    [Header("References")]
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
    EventTrigger fireButton;


    [Header("Player Prefabs")]
    [SerializeField]
    GameObject[] playerPrefabs;

    public GameObject player;

    private void Start()
    {
        player = Instantiate(playerPrefabs[PurchaseShipsManager.equippedShipId],transform);
        _camera.Follow = player.transform;
        player.GetComponent<PlayerMovement>().joystick = joystick;
        player.GetComponent<SpeedBlock>().speedLines = speedLines;
        player.GetComponent<SpeedBlock>().statusTextScript = statusTextScript;
        dashButton.onClick.AddListener(player.GetComponent<PlayerMovement>().Dash);
        //fireButton.triggers.Add();
    }
}
