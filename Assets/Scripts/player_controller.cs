using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;

    //  fuerza externa (empuje)
    private Vector3 externalForce;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // GRAVEDAD
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        // MOVIMIENTO FINAL 
        Vector3 finalMove =
            move * speed +
            velocity +
            externalForce;

        controller.Move(finalMove * Time.deltaTime);

        // reducir empuje 
        externalForce = Vector3.Lerp(externalForce, Vector3.zero, 5f * Time.deltaTime);
    }

    // MÉTODO PARA EMPUJAR
    public void AddForce(Vector3 force)
    {
        externalForce += force;
    }
}
