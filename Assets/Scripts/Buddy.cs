using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buddy : MonoBehaviour
{
    List<Transform> waypoints;
    int waypointIndex = 0;
    [SerializeField] GameObject path;
    [SerializeField] GameObject buddyWin;
    [SerializeField] GameObject canvas;
    public GameSession gameSession;
    public Player player;
    [SerializeField] GameObject playerWin;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject exitPath;
    public bool levelComplete;
    [SerializeField] AudioClip winSound;
    [SerializeField] float winSoundVolume = 1f;
    [SerializeField] AudioClip buttonPressSound;
    [SerializeField] float buttonPressSoundVolume = 1f;
    private bool buttonPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = GetWaypoints();
        player = FindObjectOfType<Player>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        levelComplete = FindObjectOfType<ShootLaser>().levelComplete;
        Move();
    }


    private void Move()
    {
        if (levelComplete == true)
        {
            if (!buttonPlayed)
            {
                AudioSource.PlayClipAtPoint(buttonPressSound, Camera.main.transform.position, buttonPressSoundVolume);
                buttonPlayed = true;
            }
            exitPath.SetActive(true);

            if (waypointIndex <= waypoints.Count - 1)
                {
                var targetPosition = waypoints[waypointIndex].transform.position;
                var movementThisFrame = moveSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

                if (transform.position == targetPosition)
                {
                    waypointIndex++;
                }
        }

        else
        {
            AudioSource.PlayClipAtPoint(winSound, Camera.main.transform.position, winSoundVolume); // sound
            buddyWin.transform.position = transform.position; // move buddy win
            canvas.SetActive(true); // show canvas
            gameSession.levelWon = true; // set levelWon
            playerWin.transform.position = player.transform.position; // move player win
            player.DestroyPlayer(); // destroy player
            Destroy(gameObject); // destroy buddy
        }
        }
    }
    

    public List<Transform> GetWaypoints() 
    { 
        var pathWaypoints = new List<Transform>();
        foreach (Transform child in path.transform)
        {
            pathWaypoints.Add(child);
        }

        return pathWaypoints; 
    }
}
