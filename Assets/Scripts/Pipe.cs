using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    private bool held = false;
    private Vector3 charPos;
    private Player player;
    [SerializeField] GameObject Mirror;
    private float charPosX;
    private float charPosY;
    [SerializeField] float pipePosX = .51f;
    private float pipePosY = .5f;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        charPos = player.gameObject.transform.position;
        charPosX = charPos.x;
        charPosY = charPos.y;
        checkPlayerRadius();

        if (held)
        {
            transform.localScale = new Vector3(.4f, .4f, .4f);
            transform.position = new Vector3(charPos.x + .05f, charPos.y + .3f, 0);
        }
    }

    void checkPlayerRadius()
    {
        if (Mathf.Abs(Mathf.Abs(charPos.x) - Mathf.Abs(transform.position.x)) <= .5f && 
            Mathf.Abs(Mathf.Abs(charPos.y) - Mathf.Abs(transform.position.y)) <= .5f)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                held = true;
                Mirror.SetActive(false);
            }

            if (Input.GetKeyUp(KeyCode.Space) && held)
            {
                held = false;
                Mirror.SetActive(true);
                transform.localScale = new Vector3(1, 1, 1);

                if (charPosX < 0f)
                {
                    pipePosX = .49f;
                }

                transform.position = new Vector3(Mathf.Round(charPos.x + .1f) + pipePosX, Mathf.Round(charPos.y -.2f) + pipePosY, 0);
            }
        }
    }
}
