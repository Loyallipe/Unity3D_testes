using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Animator animator;

    private float moveSpeed = 5f; // Velocidade de movimento do jogador

    private Rigidbody rb;
    private Vector2 moveInput;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody não encontrado! Adicione um Rigidbody ao objeto.");
        }
    }

    // Este método é chamado pelo PlayerInput component quando a ação "Move" é acionada
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        // Calcula a direção do movimento baseada na entrada (x para esquerda/direita, y para frente/trás)
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

        Vector3 movement = transform.TransformDirection(moveDirection) * moveSpeed;

        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
        animator.SetBool("inWalking", moveInput.magnitude != 0);
    }
}
