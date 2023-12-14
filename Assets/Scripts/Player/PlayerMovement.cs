using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class PlayerMovement : MonoBehaviour
{

    public Joystick joystick;
    public Transform ship;
    public ParticleSystem fire;

    Rigidbody2D player_rb;

    bool can_move = false;
    bool can_dash = false;
    public bool is_dashing { get; private set; } = false;

    public float move_speed;
    public float rot_speed;
    public float dash_speed;
    public float dash_cooldown;
    public float dash_time;

    Vector2 last_dir = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        //initialize
        player_rb = GetComponent<Rigidbody2D>();
        fire.Stop();

        //wait on start
        StartCoroutine(StartMovement());
    }

    void Move() 
    {
        if(!is_dashing)
            player_rb.velocity = ((Vector2)ship.transform.up)* Time.fixedDeltaTime * move_speed;
        else
            player_rb.velocity = ((Vector2)ship.transform.up) * Time.fixedDeltaTime * dash_speed;

        Debug.DrawRay(transform.position,transform.up,Color.red);
    }

    void Rotate() 
    {
        ship.rotation = Quaternion.Slerp(ship.rotation, Quaternion.FromToRotation(Vector3.up, last_dir), Time.fixedDeltaTime * rot_speed);
        if (joystick.Direction != Vector2.zero)
            last_dir = joystick.Direction.normalized;
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
