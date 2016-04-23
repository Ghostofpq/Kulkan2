using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{

    public Kulkan kulkan;

    public float adaptationSpeed;
    public float maxFollowOffset;
    private Mode mode;

    // Use this for initialization
    void Start()
    {
        mode = Mode.FOLLOWING;
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case Mode.FOLLOWING:
                UpdateFollow(Time.deltaTime);
                break;
            case Mode.STATIC:
                break;
        }
    }

    void UpdateFollow(float deltaTime)
    {
        float offset = Camera.main.transform.position.x - kulkan.transform.position.x;
        if (kulkan.facingRight)
        {
            if (Mathf.Abs(offset) < maxFollowOffset || offset < 0)
            {
                if (offset > 0 && maxFollowOffset - offset < 0.5)
                {
                    Camera.main.transform.position = new Vector3(
                   kulkan.transform.position.x + maxFollowOffset,
                   Camera.main.transform.position.y,
                   Camera.main.transform.position.z);
                }
                else {
                    Camera.main.transform.position = new Vector3(
                    Camera.main.transform.position.x + adaptationSpeed * deltaTime,
                    Camera.main.transform.position.y,
                    Camera.main.transform.position.z);
                }
            }
        }
        else
        {
            if (Mathf.Abs(offset) < maxFollowOffset || offset > 0)
            {
                if (offset < 0 && maxFollowOffset + offset < 0.5)
                {
                    Camera.main.transform.position = new Vector3(
                        kulkan.transform.position.x - maxFollowOffset,
                        Camera.main.transform.position.y,
                        Camera.main.transform.position.z);
                }
                else {
                    Camera.main.transform.position = new Vector3(
                        Camera.main.transform.position.x - adaptationSpeed * deltaTime,
                        Camera.main.transform.position.y,
                        Camera.main.transform.position.z);
                }
            }
        }
    }


    private enum Mode
    {
        FOLLOWING,
        STATIC
    }
}
