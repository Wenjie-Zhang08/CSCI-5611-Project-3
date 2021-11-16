using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    float maxX = 80f;
    float minX = -80f;
    float xSpeed = 40.0f;
    float ySpeed = 40.0f;
    bool xPos = false;
    bool xNeg = false;
    bool yPos = false;
    bool yNeg = false;

    bool forward = false;
    bool backward = false;
    bool left = false;
    bool right = false;

    public Vector3 localRot;
    public float camSpeed;
    void Start()
    {
        this.transform.localRotation = Quaternion.Euler(localRot);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            xNeg = true;
           
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            xPos = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            yNeg = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            yPos = true;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            xNeg = false;

        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            xPos = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            yNeg = false;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            yPos = false;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            forward = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            backward = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            left = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            right = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            forward = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            backward = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            left = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            right = false;
        }

        if (xPos)
        {
            localRot.x += (xSpeed * Time.deltaTime);
        }
        if (xNeg)
        {
            localRot.x -= (xSpeed * Time.deltaTime);
        }
        if (yPos)
        {
            localRot.y += (ySpeed * Time.deltaTime);
        }
        if (yNeg)
        {
            localRot.y -= (ySpeed * Time.deltaTime);
        }

        // clamp
        if (localRot.y >= 360.0f) localRot.y -= 360.0f;
        if (localRot.y < 0.0f) localRot.y += 360.0f;
        if (localRot.x > maxX) localRot.x = maxX;
        if (localRot.x < minX) localRot.x = minX;
        this.transform.rotation = Quaternion.Euler(localRot);


        Vector3 dir = Vector3.zero;
        if (forward)
        {
            dir += this.transform.forward;
        }
        if (backward)
        {
            dir -= this.transform.forward;
        }
        if (left)
        {
            dir -= this.transform.right;
        }
        if(right)
        {
            dir += this.transform.right;
        }
        //Debug.Log(dir);
        if(dir.magnitude > 0)
        {
            dir = dir.normalized;
            dir = dir * (camSpeed * Time.deltaTime);
            this.transform.position = this.transform.position + dir;
        }


    }
}
