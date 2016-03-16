using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public float joystickSensitivity;
    public Kulkan kulkan;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float input_x = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            HandleInputJump();
        }
        
        if (input_x > joystickSensitivity)
        {
            HandleInputX(true, input_x);
        }
        else if (input_x < -joystickSensitivity)
        {
            input_x = Mathf.Abs(input_x);
            HandleInputX(false, input_x);
        }
        else {
            HandleNoInputX();
        }
    }


    void HandleInputX(bool direction, float value)
    {
        if (value < 0.7)
        {
            kulkan.walk(direction);
        }
        else
        {
            kulkan.run(direction);
        }
    }

    void HandleNoInputX()
    {
        kulkan.stop();
    }

    void HandleInputJump()
    {
        Debug.Log("Jump");
        kulkan.jump();
    }

}
