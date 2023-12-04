using UnityEngine;

public class PhysicButton : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public bool pressed = false;
    public float time = 0.1f;
    private Vector3 velocity;
    public bool stayOnPosition = true;

   

    private void FixedUpdate()
    {
        if(stayOnPosition)
        {
            if (pressed)
            {
                transform.position = Vector3.SmoothDamp(transform.position, end.position, ref velocity, time);
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, start.position, ref velocity, time);
            }
        }
        
    }

    public void SetPressed(bool pressed)
    {
        this.pressed = pressed;
    }
    public void SetOnPosition(bool stay)
    {
        stayOnPosition = stay;
    }

    public void Spawn(GameObject go)
    {
        Instantiate(go, transform.position, Quaternion.identity);
    }
}
