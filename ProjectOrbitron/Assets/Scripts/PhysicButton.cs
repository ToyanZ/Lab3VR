using UnityEngine;

public class PhysicButton : MonoBehaviour
{
    public enum MovementMode { SmoothDamp, Lerp}
    public Transform start;
    public Transform end;
    public bool pressed = false;
    public float time = 0.1f;
    private Vector3 velocity;
    public bool stayOnPosition = true;
    public MovementMode movementMode = MovementMode.SmoothDamp;

    private float sign = 1;
    private float counter = 0;

    private void FixedUpdate()
    {
        if(stayOnPosition)
        {
            if(movementMode == MovementMode.SmoothDamp )
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
            else
            {
                counter += Time.deltaTime * sign;
                if(counter >= time) counter = time;

                if (pressed)
                {
                    transform.position = Vector3.Lerp(start.position, end.position, time);
                }
                else
                {
                    transform.position = Vector3.Lerp(start.position, end.position, time);
                }
            }
        }
    }

    public void SetPressed(bool pressed)
    {
        this.pressed = pressed;
        sign = pressed ? 1 : -1;
    }
    public void SetOnPosition(bool stay)
    {
        stayOnPosition = stay;
    }

    public void Spawn(GameObject go)
    {
        Instantiate(go, transform.position, Quaternion.identity);
    }

    public void SetPosition(GameObject gameObject)
    {
        transform.position = gameObject.transform.position;
    }
    public void ResetCounter()
    {
        counter = 0;
    }
}
