using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public Transform body;
    public float time = 0.2f;
    public bool open = false;
    private Vector3 velocity;

    private void FixedUpdate()
    {
        if (open)
        {
            body.position = Vector3.SmoothDamp(body.position, end.position, ref velocity, time);
        }
        else
        {
            body.position = Vector3.SmoothDamp(body.position, start.position, ref velocity, time);
        }
    }


    public void Switch()
    {
        open = !open;
    }

}
