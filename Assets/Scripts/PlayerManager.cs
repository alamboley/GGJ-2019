using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float speedCircleRotation;

    public List<Player> players;

    public Transform playerDownDirection;
    public Transform playerUpDirection;
    public Transform playerLeftDirection;
    public Transform playerRightDirection;

     public Slider mainSlider;
     float oldSliderValue;

    void Start()
    {
    }

    public void OnSliderChanged()
    {
        foreach (Player player in players)
        {
            if (mainSlider.value > oldSliderValue)
            {
                player.transform.RotateAround(Vector3.zero, Vector3.forward, speedCircleRotation);
            }
            else
            {
                player.transform.RotateAround(Vector3.zero, Vector3.back, speedCircleRotation);
            }
        }

        oldSliderValue = mainSlider.value;
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
