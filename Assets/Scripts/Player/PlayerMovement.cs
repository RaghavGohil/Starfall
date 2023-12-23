using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
internal sealed class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance { get; private set; }

    [SerializeField]
    Joystick joystick;
    [SerializeField]
    Transform ship;
    [SerializeField]
    ParticleSystem fire;

    Rigidbody2D player_rb;

    bool can_move = false;
    bool can_dash = false;
    bool speedBoost;
    public bool is_dashing { get; private set; } = false;

    [SerializeField]
    float max_move_speed;
    [SerializeField]
    float min_move_speed;
    [SerializeField]
    float speedBoostAmount;

    [SerializeField]
    float rot_speed;
    [SerializeField]
    float dash_speed;
    [SerializeField]
    float dash_cooldown;
    [SerializeField]
    float dash_time;

    Vector2 last_dir = Vector2.zero;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);
    }

    // Start is called before the first frame update
    void Start()
    {
        //initialize
        player_rb = GetComponent<Rigidbody2D>();
        speedBoost = false;
        fire.Stop();

        //wait on start
        StartCoroutine(StartMovement());
    }

    void Move() 
    {
        if (!speedBoost)
        {
            if (!is_dashing)
                player_rb.velocity = ((Vector2)ship.transform.up) * Mathf.Lerp(min_move_speed, max_move_speed, joystick.Direction.magnitude) * Time.fixedDeltaTime;
            else
                player_rb.velocity = ((Vector2)ship.transform.up) * Time.fixedDeltaTime * dash_speed;
        }
        else 
        {
            player_rb.velocity = ((Vector2)ship.transform.up) * max_move_speed * Time.fixedDeltaTime;
        }
        
    }

    public void BoostSpeed() 
    {
        speedBoost = true;
        max_move_speed += speedBoostAmount;
        dash_speed += speedBoostAmount;
        
    }

    public void UnBoostSpeed()
    {
        speedBoost = false;
        max_move_speed -= speedBoostAmount;
        dash_speed -= speedBoostAmount;
    }

    void Rotate() 
    {
        ship.rotation = Quaternion.Slerp(ship.rotation, Quaternion.FromToRotation(Vector3.up, last_dir), Time.fixedDeltaTime * rot_speed);
        if (joystick.Direction != Vector2.zero)
            last_dir = joystick.Direction;
    }

    public void Dash() // ON UI
    {
        if (can_dash) 
        {
            is_dashing = true;
            can_dash = false;
            StartCoroutine(ReInitializeDash());
        }
    }


    void FixedUpdate()
    {
        if (can_move)
        { 
            Move();
            Rotate();
        }
    }

    IEnumerator StartMovement() 
    {
        yield return new WaitForSeconds(1f);
        fire.Play();
        can_move = true;
        can_dash = true;
    }

    IEnumerator ReInitializeDash()
    {
        yield return new WaitForSeconds(dash_time);
        is_dashing = false;
        yield return new WaitForSeconds(dash_cooldown);
        if (can_move)
            can_dash = true;
    }

}
