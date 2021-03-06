using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{
    Vector2 pos, dir;

    public GameObject laserObj;
    LineRenderer laser;
    List<Vector2> laserIndices = new List<Vector2>();
    public bool win;
    public LaserBeam(Vector2 pos, Vector3 dir, Material material)
    {
        this.laser = new LineRenderer();
        this.laserObj = new GameObject();
        this.laserObj.name = "Laser Beam";
        this.pos = pos;
        this.dir = dir;
        this.laserObj.tag = "laser";
        this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = 0.1f;
        this.laser.endWidth = 0.1f;
        this.laser.material = material;
        this.laser.startColor = Color.red;
        this.laser.endColor = Color.red;

        CastRay(pos, dir, laser);
    }

    void CastRay(Vector2 pos, Vector2 dir, LineRenderer laser)
    {
        laserIndices.Add(pos);

        Ray2D ray = new Ray2D(pos, dir);
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, 30);
        if(Physics2D.Raycast(pos, dir, 30))
        {
            CheckHit(hit, dir, laser );
            
        }
        else
        {
            laserIndices.Add(ray.GetPoint(30));
            UpdateLaser();
        }    
    }

    void UpdateLaser()
    {
        int count = 0;
        laser.positionCount = laserIndices.Count;

        foreach (Vector2 idx in laserIndices)
        {
            laser.SetPosition(count, idx);
            count++;
        }
    }

    void CheckHit(RaycastHit2D hitInfo, Vector2 direction, LineRenderer laser)
    {
        if(hitInfo.collider.gameObject.tag == "Mirror")
        {
            Vector2 pos = hitInfo.point;
            Vector2 dir = Vector2.Reflect(direction, hitInfo.normal);

            CastRay(pos, dir, laser);
        }
        else if(hitInfo.collider.gameObject.tag != "laser")
        {
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
        else if(hitInfo.collider.gameObject.tag != "Player")
        {
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
        else
        {

        }

        if (hitInfo.collider.gameObject.tag == "Goal")
        {
            win = true;
        }
    }
}
//USE LEVEL COMPLETE TAG ON FINAL GOAL OBJECT TO CHECK FOR LEVEL COMPLETION