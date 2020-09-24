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

        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Movement
        float moveSpeed = (anim.GetBool("run") ? runSpeed : (anim.GetBool("walk") ? walkSpeed : 0));
        movement = transform.forward * moveSpeed;
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
                break;
            case "walk":
                anim.SetBool("run", false);
                anim.SetBool("walk", true);
                break;
            case "run":
                anim.SetBool("walk", false);
                anim.SetBool("run", true);
                break;
            case "flip":
                anim.Play(action);
                break;
        }
    }

}
