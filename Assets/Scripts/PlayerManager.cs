using System.Collections.Generic;
using System;
using UnityEngine;


[Serializable]
public class Touch
{
    public int touchId = 0;
    public bool isMouse = false;
    public TouchState state = TouchState.ENDED;
    public float duration = 0f;
    public Vector3 position = Vector3.zero;
    public Vector3 delta = Vector3.zero;
}

public enum TouchState
{
    BEGIN,
    DOING,
    ENDED
}

public class PlayerManager : MonoBehaviour
{
    public float speedCircleRotation;

    public Player[] PlayersPrefab;
    List<Player> players = new List<Player>();

    public AnimationCurve curve;
    public int minPlayers = 1;
    public int maxPlayers = 5;

    public float endGameTime = 5f * 60f;
    private float gameStartTime;

    List<Touch> touches = new List<Touch>();
    List<Touch> touchgesgc = new List<Touch>();

    void Start()
    {
        gameStartTime = Time.unscaledTime;

        for (int i = 0; i < players.Count; ++i)
        {
            AddPlayer(i);
        }
    }

    void AddPlayer(int i)
    {
        int distanceAngleBetweenPlayers = 360 / PlayersPrefab.Length;
        float x = Mathf.Cos((transform.eulerAngles.z + (distanceAngleBetweenPlayers * i) + 90) * Mathf.Deg2Rad);
        float y = Mathf.Sin((transform.eulerAngles.z + (distanceAngleBetweenPlayers * i) + 90) * Mathf.Deg2Rad);
        players.Add(Instantiate(PlayersPrefab[i], new Vector3(x, y, 0), Quaternion.AngleAxis(distanceAngleBetweenPlayers * i, Vector3.forward)));
    }

    Touch GetTouch(int id)
    {
        foreach(Touch t in touches)
        {
            if (t.touchId == id)
                return t;
        }
        return null;
    }

    Touch GetMouseTouch()
    {
        foreach (Touch t in touches)
            if (t.isMouse)
                return t;

        return null;
    }

    private void Update()
    {
        //Update
        //Update touches
        foreach (Touch t in touches)
        {
            if (t.state == TouchState.BEGIN)
            {
                t.state = TouchState.DOING;
            }
            else if (t.state == TouchState.ENDED)
            {
                touchgesgc.Add(t);
            }
        }

        foreach (Touch t in touchgesgc)
        {
            touches.Remove(t);
        }
        touchgesgc.Clear();

        float elapsedTime = Time.unscaledTime - gameStartTime;
        float curvevalue = curve.Evaluate(elapsedTime / endGameTime);
        int playersRequired = Mathf.RoundToInt(Mathf.Lerp(minPlayers, maxPlayers, curvevalue));
        for(int i = players.Count; i < playersRequired; i++)
        {
            AddPlayer(i);
        }

        Touch currentMouseTouch = GetMouseTouch();

#if UNITY_STANDALONE || UNITY_EDITOR
        if(Input.GetMouseButtonDown(0))
        {
            if(currentMouseTouch == null)
            {
                Touch t = new Touch();
                t.isMouse = true;
                t.state = TouchState.BEGIN;
                t.position = Input.mousePosition;
                t.duration = 0f;
                touches.Add(t);
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(currentMouseTouch != null)
            {
                currentMouseTouch.state = TouchState.ENDED;
                currentMouseTouch.position = Input.mousePosition;
            }
        }
        else
        {
            if(currentMouseTouch != null)
            {
                currentMouseTouch.delta = Input.mousePosition - currentMouseTouch.position;
                currentMouseTouch.position = Input.mousePosition;
                currentMouseTouch.duration += Time.deltaTime;
            }
        }
#else
        //something
#endif

        UpdateInput();
    }

    bool dragging = false;
    float dragAngle = 0f;
    float dragAngleStart = 0f;
    Vector3 lastTouchRelativePost = Vector3.zero;

    void UpdateInput()
    {

        if (touches.Count < 1)
            return;

        if (Camera.main == null)
            return;

        Camera cam = Camera.main;
        float w = cam.pixelWidth;
        float h = cam.pixelHeight;
        Touch t = touches[0];

        Vector3 screenCenter = new Vector3(w*.5f,h*.5f,0f);
        Vector3 touchRelativePosition = t.position - screenCenter;

        float deg = Vector3.SignedAngle(Vector3.right,touchRelativePosition,Vector3.forward);

        //Debug.Log(deg);

        if (t.state == TouchState.ENDED)
        {
            dragging = false;
            return;
        }else if(t.state == TouchState.BEGIN || t.state == TouchState.DOING)
        {
            dragging = true;
        }

        if (!dragging)
        {
            dragAngleStart = deg;
            dragAngle = 0f;
        }
        else
        {
            float dadelta = Vector3.SignedAngle(touchRelativePosition, lastTouchRelativePost, Vector3.forward) * 60f;
            dragAngle += dadelta;

            RotatePlayers(dadelta * Mathf.Deg2Rad);
            /*
            
            if (t.delta.sqrMagnitude > 0.001f)
            {
                float angleDiff = dragAngle - dragAngleStart;
                float angleDelta = lastAngleDiff - angleDiff;
                RotatePlayers(angleDelta * Mathf.Deg2Rad);
            }*/


        }

        lastTouchRelativePost = touchRelativePosition;

    }

    public void RotatePlayers(float deltaDeg)
    {
        foreach (Player player in players)
        {
            player.transform.RotateAround(Vector3.zero, Vector3.back, deltaDeg);
        }
    }
}
