using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicController : MonoBehaviour
{
    public AudioSource menuMusic; // Référence à l'Audio Source de la musique du menu
    public AudioSource mainMusic;
    public AudioSource discoMusic;
    public AudioSource gameOverMusic;
    public AudioSource munch;

    void Start()
    {
        menuMusic.Play(); // Démarre la lecture de la musique du menu au démarrage du script
    }

    // Vous pouvez appeler cette fonction lorsque le menu principal est affiché
    public void StartMenuMusic()
    {
        menuMusic.Play(); // Commence à jouer la musique du menu
    }

    // Vous pouvez appeler cette fonction lorsque vous quittez le menu
    public void StopMenuMusic()
    {
        menuMusic.Stop(); // Arrête la musique du menu
    }


    public void StartMainMusic()
    {
        mainMusic.Play(); // Commence à jouer la musique du menu
    }

    // Vous pouvez appeler cette fonction lorsque vous quittez le menu
    public void StopMainMusic()
    {
        mainMusic.Stop(); // Arrête la musique du menu
    }

    public void StartDiscoMusic()
    {
        discoMusic.Play(); // Commence à jouer la musique du menu
    }

    // Vous pouvez appeler cette fonction lorsque vous quittez le menu
    public void StopDiscoMusic()
    {
        discoMusic.Stop(); // Arrête la musique du menu
    }
    public void StartGameOverMusic()
    {
        gameOverMusic.Play(); // Commence à jouer la musique du menu
    }

    // Vous pouvez appeler cette fonction lorsque vous quittez le menu
    public void StopGameOverMusic()
    {
        gameOverMusic.Stop(); // Arrête la musique du menu
    }

    public void PlayMunch()
    {
        munch.Play(); // Commence à jouer la musique du menu
    }

}