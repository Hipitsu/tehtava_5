using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControls : MonoBehaviour 
{
    public float thrustSpeed;
    public float turnSpeed;
    public float hoverPower;
    public float hoverHeight;

    private float thrustInput;
    private float turnInput;
    private float lateralInput;
    private Rigidbody shipRigidBody;

    // Use this for initialization
    void Start()
    {
        shipRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        thrustInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space))
        {
            lateralInput = 1f; // Ylös
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            lateralInput = -1f; // Alas
        }
        else
        {
            lateralInput = 0f; // Ei liikettä
        }

    }

    void FixedUpdate()
    {
        // Turning the ship 
        shipRigidBody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);

        shipRigidBody.AddRelativeForce(0f, 0f, thrustInput * thrustSpeed);

        //Moving the ship
        shipRigidBody.AddRelativeForce(0f, lateralInput * thrustSpeed, 0f);

        if (transform.position.y < hoverHeight)
        {
            shipRigidBody.AddForce(0f, hoverPower, 0f);
        }

        //Hovering
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverPower;
            shipRigidBody.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            hoverPower = 700f;
            Debug.LogFormat("Lisää pomppua!");
        }
        else
        {
            hoverPower = 30f;
        }
    }
}
