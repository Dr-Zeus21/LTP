using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] float gameStartDelay = 1.8f;
    [SerializeField] AudioClip gameStartSound;
    [SerializeField] [Range(0,1)] float gameStartSoundVolume = .7f;
    [SerializeField] [Range(0,1)] float musicDropLevel = .25f;
    [SerializeField] SceneTracker sceneTracker;
    public bool levelWon = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && levelWon)
        {
                LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(sceneTracker.sceneNumber);
        }
    }

    public void LoadNextLevel()
    {
        print("test");
        SceneManager.LoadScene(sceneTracker.sceneNumber + 1);
    }
        
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        StartCoroutine(DelayGameStart());
    }

    IEnumerator DelayGameStart()
    {
        FindObjectOfType<MusicPlayer>().GetComponent<AudioSource>().volume = musicDropLevel;
        AudioSource.PlayClipAtPoint(gameStartSound, Camera.main.transform.position, gameStartSoundVolume);
        yield return new WaitForSeconds(gameStartDelay);
        FindObjectOfType<MusicPlayer>().DestroyMusicPlayer();
        SceneManager.LoadScene(1);
    }

    public void setMusicVolume()
    {
        this.GetComponent<AudioSource>().volume = musicDropLevel;
    }
}
