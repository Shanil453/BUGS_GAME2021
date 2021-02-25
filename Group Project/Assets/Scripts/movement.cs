using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float thrust = 10.0f;
    public int maxjumptime = 400 ;
    public float jumppower = 4.0f;
    public float voiddepth = -20f;
    public float turnspeed = 1.0f;
    public float antigravity = 2.0f;
    Rigidbody rbody;
    Vector3 origin;

    bool antigravActive;
    //determines how long the button can be held
    int jumptime = 0;

    // raycast to check for ground
    RaycastHit hit;
    //points the raycast in downward direction
    Vector3 dir = new Vector3(0, -1);

    // Start is called before the first frame update
    void Start()
    {
        antigravActive = false;
        origin = transform.position;
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // lateral movement code
        if (Input.GetKey("w"))
        {
            rbody.AddForce(transform.forward * thrust);
        }
        if (Input.GetKey("s"))
        {
            rbody.AddForce(transform.forward * thrust * (-1));
        }
        if (Input.GetKey("d"))
        {
            rbody.AddForce(transform.right * thrust);
        }
        if (Input.GetKey("a"))
        {
            rbody.AddForce(transform.right * thrust * (-1));
        }

        // modify gravity
        if (Input.GetKeyDown("x"))
        {
            antigravActive = !antigravActive;
        }
        if (antigravActive)
        {
            rbody.AddForce(transform.up * antigravity);
        }

        // jumping code
        if (Input.GetKey("space") && (jumptime < maxjumptime))
        {
            rbody.AddForce(transform.up * jumppower);
            jumptime += 1;
        }

        if (Input.GetKeyUp("space") && (jumptime < maxjumptime))
        {
            jumptime = maxjumptime;
        }

        if (Physics.Raycast(transform.position, dir, out hit, 1f))
        {
            jumptime = 0;
        }



        // save you from falling

        if(transform.position.y < voiddepth)
        {
            transform.position = origin;
            rbody.velocity = new Vector3(0, 0, 0);
        }
        // turns the guy
        transform.Rotate(new Vector3(0,Input.GetAxis("Mouse X")*turnspeed,0) * Time.deltaTime);
    }
}
