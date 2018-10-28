using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public GameObject world;

    private Vector3 velocity = new Vector3(0f, 0f, 20f);
    private Vector3 rayV = Vector3.down;
    private float width;
    private Transform worldTargetTransform;

    void Start () {
        width = GetComponent<Renderer>().bounds.size.z;
        worldTargetTransform = world.transform;
        print(width);
	}

    bool flag;
    float distance;

    void Update()
    {
        Vector3 origin = transform.TransformPoint(new Vector3(0f, 0f, -width / 2));
        RaycastHit hit;
        //world.transform.rotation = Quaternion.Slerp(world.transform.rotation, worldTargetTransform.rotation, 0.1f);
        Input.GetKeyDown("ArrowDown");
        if (Physics.Raycast(origin ,rayV, out hit)) {
            transform.Translate(velocity * Time.deltaTime);
            flag = false;
            distance = hit.distance;
        }
        else if (!flag){
            flag = true;
            worldTargetTransform.rotation = Quaternion.AngleAxis(90, new Vector3(1f, 0f, 0f));
            transform.Rotate(new Vector3(90f, 0f, 0f));
            //worldTargetTransform.Rotate(new Vector3(-90f, 0f, 0f));
            transform.Translate(new Vector3(0f, 0f, width + distance));
            rayV = transform.TransformDirection(Vector3.down);
            Debug.Log(rayV);
        }
    }
}
