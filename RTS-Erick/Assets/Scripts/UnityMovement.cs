using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityMovement : MonoBehaviour
{
    public List<Node> Path;
    Vector3[] waypoints;
    int current = 0;
    public float maxVelocity;
    bool mouseClick = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mouseClick = true;
            for (int i = 0; i < Path.Count; i++)
            {
                waypoints[i] = Path[1].Position;
            }
        }

        if (mouseClick)
        {
            if (transform.position.x != waypoints[current].x && transform.position.z != waypoints[current].z)
            {
                Vector3 dir = (waypoints[current] - transform.position).normalized;
                Vector3 velocity = dir * maxVelocity * Time.deltaTime;
                transform.position += velocity;
            }
            else current = (current + 1) % waypoints.Length;
            
            if (current == waypoints.Length)
            {
                mouseClick = false;
            }
        }
    }
}
