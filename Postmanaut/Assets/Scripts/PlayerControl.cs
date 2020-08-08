using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float moveSpeed = 10f;
    public float turnSpeed = 200f;
    public float turnSmooth = 5f;
    public float gravity = 20f;
    public float rotation = -90f;

    private Vector3 direction = Vector3.zero;

    private Animator anim;
    private CharacterController controller;

    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Movement
        direction = transform.forward * moveSpeed * (anim.GetBool("Moving") ? 1 : 0);
        controller.Move(direction * Time.deltaTime);
        direction.y -= gravity * Time.deltaTime;

        // Rotation
        Quaternion target = Quaternion.Euler(transform.eulerAngles.x, rotation, transform.eulerAngles.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * turnSmooth);
    }

    public void PerformAction(string action)
    {
        if (action == "flip")
            anim.Play("Flip");
        else if (action == "move")
            anim.SetBool("Moving", true);
        else if (action == "stop")
            anim.SetBool("Moving", false);
    }

    public void TurnDirection(string direction)
    {
        if (direction == "forward")
            rotation = -90f;
        else if (direction == "back")
            rotation = 90f;
        else if (direction == "left")
            rotation = 180f;
        else if (direction == "right")
            rotation = 0f;
    }

}
