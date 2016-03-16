using UnityEngine;
using System.Collections;

public class Kulkan : MonoBehaviour
{
    Animator anim;
    Rigidbody2D body;

    private bool on_the_floor;
    public float run_speed;
    public float walk_speed;
    public State state;
    private float joystickSensitivity;
    private int layerFloor;
    public bool direction;

    // Use this for initialization
    void Start()
    {
        on_the_floor = false;
        direction = true;
        joystickSensitivity = 0.2f;
        layerFloor = LayerMask.NameToLayer("Floor");
    }

    // Update is called once per frame
    void Update()
    {
        if (direction)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }


    public void run(bool direction)
    {
        if (on_the_floor)
        {
            if (direction)
            {
                setHorizontalSpeed(run_speed);
            }
            else
            {
                setHorizontalSpeed(-run_speed);
            }
            this.direction = direction;
        }
        this.state = State.RUNNING;
    }

    public void walk(bool direction)
    {
        setHorizontalSpeed(direction,walk_speed);       
        this.direction = direction;
        this.state = State.WALKING;
    }

    public void jump()
    {
        if (on_the_floor)
        {
            switch (state)
            {
                case State.RUNNING:
                    setVerticalSpeed(40);
                    setHorizontalSpeed(direction, run_speed);
                    break;
                case State.WALKING:
                    setVerticalSpeed(40);
                    setHorizontalSpeed(direction, walk_speed);
                    walk(direction);
                    break;
                case State.IDLE:
                default:
                    setVerticalSpeed(40);
                    break;
            }
        }
    }

    public void stop()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        this.state = State.IDLE;
    }

    private float getHorizontalSpeed()
    {
        return GetComponent<Rigidbody2D>().velocity.x;
    }

    private float getVerticalSpeed()
    {
        return GetComponent<Rigidbody2D>().velocity.y;
    }

    private void setHorizontalSpeed(bool direction, float speed)
    {
        if (direction)
        {
            setHorizontalSpeed(speed);
        }
        else
        {
            setHorizontalSpeed(-speed);
        }
    }

    private void setHorizontalSpeed(float speed)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
    }

    private void setVerticalSpeed(float speed)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, speed);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.layer.Equals(layerFloor))
        {
            on_the_floor = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer.Equals(layerFloor))
        {
            on_the_floor = false;
        }
    }

    public enum State
    {
        IDLE,
        WALKING,
        RUNNING,
        AIR_UP,
        AIR_DOWN,
        HURT,
        CROUCHING
    }
}
