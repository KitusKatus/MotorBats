using Unity.VisualScripting;
using UnityEngine;

public class MotorbikeMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D frontWheelRb; //kinda silly to try to move the player using actual wheel physics. remove this later
    [SerializeField] Rigidbody2D backWheelRb;

    Rigidbody2D playerRb;

    public float wheelRotationForce;
    public float maxSpeed; //per simulation step

    float frontOldRotate;
    float backOldRotate;

    Vector2 oldPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Controls();
    }

    void Controls()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            Drive(wheelRotationForce);
        }else
        if(Input.GetKey(KeyCode.DownArrow))
        {
            Drive(-wheelRotationForce);
        }
    }

    void Drive(float speed)
    {
        speed = LimitSpeed(speed);

        //speed *= Time.deltaTime;
        frontWheelRb.AddTorque(speed, ForceMode2D.Impulse);
        backWheelRb.AddTorque(speed, ForceMode2D.Impulse);



        //frontWheelRb.totalTorque = Mathf.Clamp(frontWheelRb.totalTorque, -maxWheelSpeed, maxWheelSpeed);
        //backWheelRb.totalTorque = Mathf.Clamp(backWheelRb.totalTorque, -maxWheelSpeed, maxWheelSpeed);


        //playerRb.AddForce()
    }

    float LimitSpeed(float speed)
    {
        //float frontRotateDelta = frontWheelRb.rotation - frontOldRotate;
        //float backRotateDelta = backWheelRb.rotation - backOldRotate;
        //float averageDelta = (frontRotateDelta + backRotateDelta) / 2; // a bit silly

        float positionDelta = playerRb.position.x - oldPosition.x;
        Debug.Log(positionDelta);   

        //frontOldRotate = frontWheelRb.rotation;
        //backOldRotate = backWheelRb.rotation;
        oldPosition = playerRb.position;

        if (positionDelta > maxSpeed) // if going too fast in positive direction
        {
            if(speed > 0)
            {
                return 0;
            }
        }

        if(positionDelta < -maxSpeed) // if going too fast in negative direction
        {
            if(speed < 0)
            {
                return 0;
            }
        }
        
        return speed;
    }
}
