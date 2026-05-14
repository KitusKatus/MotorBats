using Unity.VisualScripting;
using UnityEngine;

public class MotorbikeMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D frontWheelRb; //kinda silly to try to move the player using actual wheel physics. remove this later
    [SerializeField] Rigidbody2D backWheelRb;

    Rigidbody2D playerRb;

    public float wheelRotationForce;
    public float maxVelocity; //per simulation step

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

        //speed *= Time.deltaTime;

        frontWheelRb.AddTorque(speed, ForceMode2D.Impulse);
        backWheelRb.AddTorque(speed, ForceMode2D.Impulse);

        LimitSpeed();



        //frontWheelRb.totalTorque = Mathf.Clamp(frontWheelRb.totalTorque, -maxWheelSpeed, maxWheelSpeed);
        //backWheelRb.totalTorque = Mathf.Clamp(backWheelRb.totalTorque, -maxWheelSpeed, maxWheelSpeed);


        //playerRb.AddForce()
    }

    void LimitSpeed()
    {
        Debug.Log(GroundCheck());
        if(GroundCheck())
        {
            if(playerRb.linearVelocity.magnitude > maxVelocity)
            {
                playerRb.linearVelocity = playerRb.linearVelocity.normalized * maxVelocity;
            }
        }


        /*
        float angle = Vector2.Angle(playerRb.linearVelocity, transform.forward);
        //Debug.Log(angle);


        //frontOldRotate = frontWheelRb.rotation;
        //backOldRotate = backWheelRb.rotation;
        oldPosition = playerRb.position;

        if(angle > -90 && angle < 90) //going forward
        {
            if (positionDelta.magnitude > maxSpeed) // if going too fast in positive direction
            {
                if (speed > 0)
                {
                    return 0;
                }
            }
        }

        if(angle > 90 || angle < -90)
        {
            if (positionDelta.magnitude > maxSpeed) // if going too fast in negative direction
            {
                if (speed < 0)
                {
                    return 0;
                }
            }
        }
        
        return speed;
        */
    }

    bool GroundCheck()
    {
        Vector2 downVector = transform.up * -1;
        return Physics2D.Raycast(transform.position, downVector, 5f, LayerMask.GetMask("Ground"));
    }
}
