using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

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

    public Player PlayersPrefab;

    [HideInInspector]
    public List<Player> players = new List<Player>();

    public UnityEvent OnPlayersCreated;

    List<Touch> touches = new List<Touch>();
    List<Touch> touchgesgc = new List<Touch>();

    [HideInInspector]
    public float CircleDiv = 0f;

    void Start()
    {
        AddPlayers(1);
    }

    void AddPlayers(int nbPlayer)
    {
        foreach(Player player in players)
        {
            Destroy(player.gameObject);
        }
        players.Clear();

        CircleDiv = 360f / (float)nbPlayer;

        for (int i = 0; i < nbPlayer; ++i)
        {
            float x = Mathf.Cos((transform.eulerAngles.z + (CircleDiv * i) + 90) * Mathf.Deg2Rad);
            float y = Mathf.Sin((transform.eulerAngles.z + (CircleDiv * i) + 90) * Mathf.Deg2Rad);
            players.Add(Instantiate(PlayersPrefab, new Vector3(x, y, 0), Quaternion.AngleAxis(CircleDiv * i, Vector3.forward)));
        }

        if (OnPlayersCreated != null)
            OnPlayersCreated.Invoke();
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

        float curvevalue = Game.instance.playerProgCurve.Evaluate(Game.instance.gameTimeNormalized);
        int playersRequired = Mathf.RoundToInt(Mathf.Lerp(Game.instance.minPlayers, Game.instance.maxPlayers, curvevalue));
        if(playersRequired != players.Count)
            AddPlayers(playersRequired);

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

        if(currentMouseTouch == null)
        {
            //KEYBOARD CONTROL
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                keyBoardVelocity = -5f;

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                keyBoardVelocity = 5f;

            }
            else if(Input.GetKey(KeyCode.LeftArrow))
            {
                keyBoardVelocity -= keyBoardBaseSpeed * Time.deltaTime;
            }
            else if(Input.GetKey(KeyCode.RightArrow))
            {
                keyBoardVelocity += keyBoardBaseSpeed * Time.deltaTime;
            }
            else if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) {
                keyBoardVelocity = 0f;
            }

            if (keyBoardVelocity < -keyboardSpeedCap)
                keyBoardVelocity = -keyboardSpeedCap;

            if (keyBoardVelocity > keyboardSpeedCap)
                keyBoardVelocity = keyboardSpeedCap;
                
            RotatePlayers(keyBoardVelocity);
        }
        else //TOUCH/MOUSE CONTROL
        {
            keyBoardVelocity = 0f;
            UpdateInput();
        }

    }

    float keyBoardVelocity = 0f;
    float keyboardSpeedCap = 10f;
    float keyBoardBaseSpeed = 25f;

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
            float dadelta = Vector3.SignedAngle(touchRelativePosition, lastTouchRelativePost, Vector3.forward) * 120f;
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
