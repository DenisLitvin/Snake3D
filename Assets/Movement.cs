using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public GameObject world;

    private Vector3 velocity = Vector3.forward;
    private float acceleration = 1f;
    private Vector3 rayV = Vector3.down;
    private float width;
    private float offset;
    private Vector3 worldToRotate;

    void Start()
    {
        width = GetComponent<Renderer>().bounds.size.z + 0.49f;
        offset = -width / 2f;
    }

    bool flag;
    float distance;

    void Update() {
        Vector3 origin = transform.TransformPoint(new Vector3(offset, offset, offset));
        RaycastHit hit;

        //print(worldToRotate);
        Vector3 rotationVel = worldToRotate * Time.deltaTime * 10;
        worldToRotate -= rotationVel;
        world.transform.Rotate(rotationVel);
        rayV = transform.TransformDirection(Vector3.down);

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            transform.Rotate(new Vector3(0f, -90f, 0f));
            //velocity = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            transform.Rotate(new Vector3(0f, 90f, 0f));
            //velocity = Vector3.right;
        }

        if (Physics.Raycast(origin, rayV, out hit)) {
            transform.Translate(velocity * acceleration * Time.deltaTime);
            flag = false;
            distance = hit.distance;
            //print(origin);
        }
        else if (!flag) {
            flag = true;

            Vector3 directionDiff = transform.TransformDirection(new Vector3(90f, 0f, 0f));
            print(directionDiff);
            worldToRotate += -directionDiff;
            //world.transform.Rotate(-directionDiff);

            transform.Rotate(new Vector3(90f, 0f, 0f));
            transform.Translate(new Vector3(0f, 0f, distance + width / 2f));
        }
    }
}
