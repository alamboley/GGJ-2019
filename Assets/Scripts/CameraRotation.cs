using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public AnimationCurve zoomCurve;
    public Camera cam;

    private float size;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 10f * Time.deltaTime);
    }

    void Awake()
    {
        size = cam.orthographicSize;
    }

    public void Zoom()
    {
        StartCoroutine(ZoomCoroutine());
    }

    IEnumerator ZoomCoroutine()
    {
        for (float f = 0f; f < 0.5f; f += Time.deltaTime)
        {
            float curvevalue = zoomCurve.Evaluate(f / 0.5f);
            cam.orthographicSize = Mathf.Lerp(size, size + 1, curvevalue);
            yield return null;
        }
    }
}
