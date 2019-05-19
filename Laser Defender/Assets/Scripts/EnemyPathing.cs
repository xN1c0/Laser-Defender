using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    /* Wave Configuration */
    [SerializeField] WaveConfig waveConfiguration;

    /* Enemy Path WayPoint List */
    List<Transform> waypoints;
    int currentWayPoint;

    /* Enemy Movement */
    Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentWayPoint = 0;
        waypoints = waveConfiguration.GetWayPoints();
        transform.position = waypoints[currentWayPoint].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToNextWayPoint();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        waveConfiguration = waveConfig;
    }

    private void MoveToNextWayPoint()
    {
        if (currentWayPoint <= waypoints.Count - 1)
        {
            newPosition = waypoints[currentWayPoint].transform.position;
            float step = waveConfiguration.GetMovementSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, newPosition, step);
            if (transform.position == newPosition)
            {
                currentWayPoint++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
