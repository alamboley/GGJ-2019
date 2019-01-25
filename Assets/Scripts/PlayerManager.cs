using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float speedCircleRotation;

    public Player[] players;

     public Slider mainSlider;
     float oldSliderValue;

    void Start()
    {
    }

    public void OnSliderChanged()
    {
        Debug.Log(mainSlider.value);

        foreach (Player player in players)
        {
            if (mainSlider.value > oldSliderValue)
                player.transform.RotateAround(Vector3.zero, Vector3.forward, speedCircleRotation);
            else
                player.transform.RotateAround(Vector3.zero, Vector3.back, speedCircleRotation);
        }

        oldSliderValue = mainSlider.value;
    }

    public void AddPlayer()
    {
        
    }
}
