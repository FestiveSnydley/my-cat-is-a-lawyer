//PATHING CODE MODIFIED FROM ALEXANDER ZOTOV'S UNITY 2D TUTORIAL: https://www.youtube.com/watch?v=KoFDDp5W5p0&t=50s
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPathing : MonoBehaviour {

    // Array of waypoints to walk from one to the next one
    [SerializeField]
    private Transform[] waypoints;

    // Walk speed that can be set in Inspector
    [SerializeField]
    private float moveSpeed = 4f;

    // Index of current waypoint from which Enemy walks
    // to the next one
    private int waypointIndex = 0;

	// Use this for initialization
	private void Start () {

        // Set position of Enemy as position of the first-> last waypoint
        transform.position = waypoints[0].transform.position;
	}
	
	// Update is called once per frame
	private void Update () {

        // Move Enemy
        Move();
	}

    // Method that actually make Enemy walk
    private void Move()
    {
        // loop thru waypoints (normal function is to stop at last waypt)
        if (waypointIndex <= waypoints.Length)
        {

            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector2.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
            if(waypointIndex == waypoints.Length) //reset walk cycle
            {
                waypointIndex = 0;
            }
        }
    }
}