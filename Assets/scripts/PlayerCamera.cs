using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public GameObject playerShip;

    private float height = 4.0f;
    private float distance = 5.0f;
    private float followDamping = 8f;
    private float rotationDamping = 100.0f;

    void Start()
    {
        transform.position = playerShip.transform.TransformPoint(0f,
        height, -distance);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,
            playerShip.transform.TransformPoint(0f, height, -distance),
            Time.deltaTime * followDamping);
        Quaternion rotation =
            Quaternion.LookRotation(playerShip.transform.position
            - transform.position + Vector3.up * 3);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,
            Time.deltaTime * rotationDamping);
    }
}
