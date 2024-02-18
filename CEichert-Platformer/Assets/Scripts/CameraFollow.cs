using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    private Camera cam;

    [SerializeField] private AnimationCurve zoomCurve;

    private bool followYAxis;

    public delegate void CameraZoom(bool zoom);
    public static CameraZoom zoom;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!followYAxis)
            transform.position = new Vector3(GameManager.Instance.player.position.x, 3, -10);
        else
        {
            float playerYPos = GameManager.Instance.player.position.y;
            playerYPos = Mathf.Clamp(playerYPos, 2, 4);
            transform.position = new Vector3(GameManager.Instance.player.position.x, playerYPos, -10);
        }
           
    }

    void ZoomCamera(bool zoom)
    {
        followYAxis = zoom;

        if (zoom)
            StartCoroutine(ZoomCameraIn(1));
        else 
            StartCoroutine(ZoomCameraOut(0));
    }
    IEnumerator ZoomCameraIn(float duration)
    {
        while (duration >= 0)
        {
            duration -= Time.unscaledDeltaTime;
            cam.orthographicSize = zoomCurve.Evaluate(duration);
            yield return null;
        }
        cam.orthographicSize = 4;
    }
    IEnumerator ZoomCameraOut(float duration)
    {
        while (duration <= 1)
        {
            duration += Time.unscaledDeltaTime;
            cam.orthographicSize = zoomCurve.Evaluate(duration);
            yield return null;
        }
        cam.orthographicSize = 5;
    }
    private void OnEnable()
    {
        zoom += ZoomCamera;
    }
    private void OnDisable()
    {
        zoom -= ZoomCamera;
    }
}
