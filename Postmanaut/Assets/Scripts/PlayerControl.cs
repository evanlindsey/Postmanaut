using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float runSpeed = 10f;
    public float walkSpeed = 3f;
    public float turnSpeed = 200f;
    public float turnSmooth = 5f;
    public float gravity = 20f;

    public float forward = -90f;
    public float back = 90f;
    public float left = 180f;
    public float right = 0f;

    private Dictionary<string, float> directions;

    private Vector3 movement;
    private float rotation;
    private float speed;

    private Animator anim;
    private CharacterController controller;

    void Start()
    {
        directions = new Dictionary<string, float>
        {
            { "forward", forward },
            { "back", back },
            { "left", left },
            { "right", right }
        };

        movement = Vector3.zero;
        rotation = forward;
        speed = 0;

        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Movement
        movement = transform.forward * speed;
        controller.Move(movement * Time.deltaTime);
        movement.y -= gravity * Time.deltaTime;

        // Rotation
        Quaternion target = Quaternion.Euler(transform.eulerAngles.x, rotation, transform.eulerAngles.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * turnSmooth);
    }

    public void SetDirection(string direction)
    {
        rotation = directions[direction];
    }

    public void PerformAction(string action)
    {
        switch (action)
        {
            case "idle":
                anim.SetBool("walk", false);
                anim.SetBool("run", false);
                speed = 0;
                break;
            case "walk":
                anim.SetBool("run", false);
                anim.SetBool("walk", true);
                speed = walkSpeed;
                break;
            case "run":
                anim.SetBool("walk", false);
                anim.SetBool("run", true);
                speed = runSpeed;
                break;
            case "flip":
                anim.Play(action);
                break;
        }
    }

}
