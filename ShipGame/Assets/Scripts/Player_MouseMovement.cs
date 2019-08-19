using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MouseMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // pierwsze dotknięcie myszką
        {
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        if (Input.GetMouseButton(0)) // wciśnięta myszka
        {
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        else // jesli nie naciskamy już
        {
            touchStart = false;
        }
        //moveCharacter(new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"))); // poruszanie się strzałkami
    }

    private void FixedUpdate()
    {
        if (touchStart)
        {
            Vector2 offset = pointB - pointA; // różnica pomiędzy odległościami
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            moveCharacter(direction * -1);
        }
    }

    void moveCharacter (Vector2 direction)
    {
        player.Translate(direction * speed * Time.deltaTime);
    }
}
