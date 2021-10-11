using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public bool levelComplete;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        levelComplete = FindObjectOfType<ShootLaser>().levelComplete;
        if (levelComplete == true)
        {
            Debug.Log("YOOOOOOOO");
        }
    }


}
