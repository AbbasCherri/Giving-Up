using UnityEngine;

public class HandBob : MonoBehaviour
{
    public CharacterController controller; 

    public float bobSpeed = 8f;
    public float bobAmount = 0.04f;

    private Vector3 startPos;
    private float timer;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        Vector3 velocity = controller.velocity;
        float speed = new Vector3(velocity.x, 0, velocity.z).magnitude;

        if (speed > 0.1f)
        {
            timer += Time.deltaTime * bobSpeed;
            float x = Mathf.Cos(timer) * bobAmount;
            float y = Mathf.Sin(timer * 2f) * bobAmount;    

            transform.localPosition = startPos + new Vector3(x, y, 0);
        }
        else
        {
            timer = 0;

            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                startPos,
                Time.deltaTime * 6f
            );
        }
    }
}