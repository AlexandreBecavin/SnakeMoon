using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicController : MonoBehaviour
{
    public AudioSource goldenMelon;
    public AudioSource gameOverMusic;
    public AudioSource munch;
    public AudioSource deathPlayer;


    void Start()
    {
        if (goldenMelon == null || gameOverMusic == null || munch == null)
        {
            Debug.LogError("Assurez-vous que toutes les références AudioSource sont définies.");
            return;
        }
    }

    public void StartMusic(string musique)
    {
        if (musique == "eat")
        {
            munch.Play();
        }
        else if (musique == "gameOver")
        {
            gameOverMusic.Play();
        }
        else if (musique == "goldenMelon")
        {
            goldenMelon.Play();
        }
        else if (musique == "deathPlayer")
        {
            deathPlayer.Play();
        }
    }

    public void StopMusic(string musique)
    {
        if (musique == "eat")
        {
            munch.Stop();
        }
        else if (musique == "gameOver")
        {
            gameOverMusic.Stop();
        }
        else if (musique == "goldenMelon")
        {
            goldenMelon.Stop();
        }
        else if (musique == "deathPlayer")
        {
            deathPlayer.Stop();
        }
    }
}
