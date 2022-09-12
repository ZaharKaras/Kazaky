using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kameraSkriptMovment : MonoBehaviour
{

    float speed = 0.06f;
    float zoomSpeed = 10.0f;
    float rotateSpeed;

    float maxHight = 40f;
    float minGight = 4f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 0.06f;
            zoomSpeed = 20.0f;
        }
        else
        {
            speed = 0.035f;
            zoomSpeed = 10.0f;
        }

        float hsp = transform.position.y * speed * Input.GetAxis("Horizontal"); // AD
        float vsp = transform.position.y * speed * Input.GetAxis("Vertical"); // WS
        float scrollsp = Mathf.Log(transform.position.y) * zoomSpeed * Input.GetAxis("Mouse ScrollWheel");

        if (transform.position.y >= maxHight && scrollsp > 0)
        {
            scrollsp = 0;
        }
        else if (transform.position.y <= minGight && scrollsp < 0)
        {
            scrollsp = 0;
        }

        if (transform.position.y + scrollsp > maxHight)
        {
            scrollsp = maxHight - transform.position.y;
        }
        else if (transform.position.y + scrollsp < minGight)
        {
            scrollsp = minGight - transform.position.y;
        }

        // работает на 3D векторе

        Vector3 verticalMove = new Vector3(0,scrollsp, 0);
        Vector3 lateralMove = hsp * transform.right;
        Vector3 forwardMove = transform.forward;

        forwardMove.y = 0;
        forwardMove.Normalize();
        forwardMove *= vsp;

        Vector3 move = verticalMove + lateralMove + forwardMove;

        transform.position += move;





    }
}
