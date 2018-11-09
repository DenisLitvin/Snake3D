using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public GameObject world;
    public float speed = 1f;
    public float worldSpeed = 0.5f;

    private readonly Vector3 velocity = Vector3.forward;
    private Vector3 ray = Vector3.down;
    private float width;
    private float offset;
    private bool stabilizing;

    private Vector3 worldLeftToRotate;
    private float worldRotationStart;
    private float distanceToGround;

    void Start()
    {
        width = GetComponent<Renderer>().bounds.size.z + 0.49f;
        offset = -width / 2f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TurnLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TurnRight();
        }
        MoveIfGrounded();
        RotateWorld();
    }

    private void TurnRight()
    {
        transform.Rotate(new Vector3(0f, 90f, 0f));
        //velocity = Vector3.right;
    }

    private void TurnLeft()
    {
        transform.Rotate(new Vector3(0f, -90f, 0f));
        //velocity = Vector3.left;
    }

    private void Move()
    {
        transform.Translate(velocity * speed * Time.deltaTime);
    }

    private void MoveIfGrounded()
    {
        Vector3 point = transform.TransformPoint(new Vector3(0f, offset, offset));
        ray = transform.TransformDirection(Vector3.down);

        RaycastHit hit;
        if (Physics.Raycast(point, ray, out hit))
        {
            stabilizing = false;
            distanceToGround = hit.distance;
            Move();
        }
        else if (!stabilizing)
        {
            Stabilize();
        }
    }

    private void Stabilize() 
    {
        stabilizing = true;
        transform.Rotate(new Vector3(90f, 0f, 0f));
        transform.Translate(new Vector3(0f, 0f, distanceToGround + width / 2f));
        Vector3 directionDiff = transform.TransformDirection(new Vector3(90f, 0f, 0f));
        worldLeftToRotate += -directionDiff;
        worldRotationStart = Time.time;
        //world.transform.Rotate(-directionDiff);
    }

    private void RotateWorld()
    {
        //float rotated = (Time.time - worldRotationStart) * worldSpeed;
        //Vector3 rotationVel = Vector3.Lerp(Vector3.zero, worldLeftToRotate, worldSpeed);
        Vector3 rotationVel = worldLeftToRotate * Time.deltaTime * worldSpeed;
        print(rotationVel);
        worldLeftToRotate -= rotationVel;
        world.transform.Rotate(rotationVel, Space.World);
    }
}