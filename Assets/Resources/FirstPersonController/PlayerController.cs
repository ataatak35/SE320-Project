using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    [SerializeField]
    private string name;
    float GetSpeed()//Returns the players intended speed
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //Debug.Log("Sprint");
            return SprintSpeed/5;
        }
        //Debug.Log("Walk");
        return WalkSpeed/5;
    }

    //Player Body Variables
    [Header("Public Adjustments:")] [Tooltip("'Show Cursor' toggles cursor sprite")] public bool ShowCursor; public float JumpHeight = 5f;
    [Header("Player Movement Speeds:")]
    [Tooltip("'Look Speed' is equal to your mouse sensitivity")] [Range(0,10)] public float LookSpeed = 5f;
    [Range(0f, 1f)]
    public float WalkSpeed = 0.50f, SprintSpeed = 0.85f;
    private Rigidbody rb;//Player rigidbody

    //Camera Variables
    private Camera Camera;

    void Start()
    {
        Camera = gameObject.GetComponentInChildren<Camera>();//Find our "child" camera
        rb = GetComponent<Rigidbody>();//Find Rigidbody Component
    }
    private void Update()
    {
        Cursor.visible = ShowCursor;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * LookSpeed, 0))); //Rotation
        rb.MovePosition(transform.position +(transform.forward*Input.GetAxis("Vertical") * GetSpeed() + transform.right * Input.GetAxis("Horizontal") * GetSpeed())); //Position

        //Camera Movement:
        float Velocity = LookSpeed * -Input.GetAxis("Mouse Y"); //Velocity
        Camera.transform.Rotate(Velocity, 0f, 0f);//Rotate on the camera's X axis.
        float CamRotationX = Camera.transform.localRotation.x;//Get's the camera's X axis rotation

        //Cancel out any rotational velocity if the player tries to rotate camera above 0.4 or below -0.5.
        if (CamRotationX > 0.5f || CamRotationX < -0.5f)
        {
            Camera.transform.Rotate(-Velocity, 0, 0);
        }
        //Jump:
        RaycastHit RayHit;//We use a raycast to check the players distance from the ground to stop double/infinite jumps.
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RayHit, 1.25f*transform.localScale.y) && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
        }
    }
}