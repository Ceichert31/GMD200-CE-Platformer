using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [Header("Hand Settings")]
    [SerializeField] private Transform endPoint;
    private Vector2 startPoint;

    [SerializeField] private float 
        fallTime = 5, 
        riseTime = 3;

    private void Start()
    {
        startPoint = transform.position;
        StartCoroutine(HandFall(fallTime));
    }
    IEnumerator HandFall(float fallTime)
    {
        float duration = 0;
        duration += Time.deltaTime;
        while (duration < fallTime)
        {
            transform.position = Vector2.Lerp(startPoint, endPoint.position, fallTime / duration);
            yield return null;
        }
        transform.position = endPoint.position;
    }

}
