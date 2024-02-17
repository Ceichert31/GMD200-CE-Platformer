using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private Transform[] movePoints;

    private Transform currentPoint;

    private int index = 0;

    private float waitTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = movePoints[index];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.01f)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                waitTime = 1f;
                index++;
                index %= movePoints.Length;
                currentPoint = movePoints[index];
            }
            
        }
    }
}
