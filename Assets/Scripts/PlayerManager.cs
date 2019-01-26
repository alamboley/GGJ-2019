using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


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

    public List<Player> players;

    public Transform playerDownDirection;
    public Transform playerUpDirection;
    public Transform playerLeftDirection;
    public Transform playerRightDirection;

     public Slider mainSlider;


    List<Touch> touches = new List<Touch>();
    List<Touch> touchgesgc = new List<Touch>();

    void Start()
    {
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

        Debug.Log(deg);

        if(t.delta.sqrMagnitude > 0.001f)
        {
            RotatePlayers(t.delta.x * 0.32f);
        }
    }
    

    public void RotatePlayers(float deltaDeg)
    {
        foreach (Player player in players)
        {
            player.transform.RotateAround(Vector3.zero, Vector3.back, deltaDeg);
        }
    }


    /// <summary>
    /// J'ai essayé de vérifier le nombre de joueur et d'en instancier grâce à des prefabs
    /// Mais ça ne fonctionne pas
    /// </summary>
    public void AddPlayer()
    {
        if(players.Count < 4)
        {
            /*foreach(Player player in players)
            {
                Destroy(player);
            }
            players.Clear();*/

            switch (players.Count)
            {
                case 1:
                    Instantiate(playerUpDirection);
                    Instantiate(playerDownDirection);
                    break;
                case 2:
                    Instantiate(playerUpDirection);
                    Instantiate(playerLeftDirection);
                    Instantiate(playerRightDirection);
                    break;
                case 3:
                    Instantiate(playerUpDirection);
                    Instantiate(playerDownDirection);
                    Instantiate(playerLeftDirection);
                    Instantiate(playerRightDirection);
                    break;
                default:
                    break;
            }
        }
    }
}
