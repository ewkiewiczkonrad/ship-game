using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TouchMovement : MonoBehaviour
{
    //player
    public Transform player;
    public float speed = 15f;

    //joystick
    //public Transform circle;
    //public Transform outterCircle;

    //dotyk
    private Vector2 startingPoint; // początkowa pozycja przy dotyku
    private int leftTouch = 99; 

    
    void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i); // sprawdzamy w której fazie dotknięcia jesteśmy 

            Vector2 touchPos = getTouchPosition(t.position)  * -1; // pozycja w worldspace
            if(t.phase == TouchPhase.Began)
            {
                if(t.position.x > Screen.width / 2) // jeśli dotkniemy po prawej częsci ekranu to strzelamy
                {
                    shootbullet(); 
                }
                else
                {
                    leftTouch = t.fingerId;
                    startingPoint = touchPos; // nie mozemy dac tu t.position poniewaz jest to screenspace 

                }
            }
            else if(t.phase == TouchPhase.Moved && leftTouch == t.fingerId) 
            {
                Vector2 offset = touchPos - startingPoint;
                Vector2 direction = Vector2.ClampMagnitude(offset, 0.1f);

                moveCharacter(direction * -1);

                //circle.transform.position = new Vector2(outterCircle.transform.position.x + direction.x, outterCircle.transform.position.y + direction.y);
            }
            else if(t.phase == TouchPhase.Ended && leftTouch == t.fingerId)
            {
                leftTouch = 99;
            }
        }
    }
    

    void moveCharacter(Vector2 direction)
    {
        player.Translate(direction * speed * Time.deltaTime);
    }

    void shootbullet() // funkcja strzelania
    {
        Debug.Log("fire");
    }

    Vector2 getTouchPosition(Vector2 touchPosition) // przekonwertuje nam miejsce dotknięcia palca (screen space) w pozycje w grze (world space) 
    {
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    }

}
