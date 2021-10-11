using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip laserSound;
    [SerializeField] float laserSoundVolume = 1f;
    public Material material;
    public LaserBeam beam;
    private int interval = 60;
    public bool levelComplete = false;
    public bool shooting = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !shooting)
        {
            shooting = true;
            AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserSoundVolume);
        }
        
        if(Time.frameCount % interval != 0)
        {
            levelComplete = beam.win;
        }
        
        if (Time.frameCount % interval == 0 && shooting)
        {
            if (beam != null)
            {
                Destroy(beam.laserObj);
            }
            beam = new LaserBeam(gameObject.transform.position, gameObject.transform.right, material);

        }
    }
    
}
