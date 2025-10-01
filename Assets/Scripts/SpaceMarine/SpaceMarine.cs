using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceMarine : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 0.1f;
    [SerializeField] private float jumpForce = 3f;

    [Header("Inputs")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    
    private CharacterController characterController;
    private float verticalVelocity;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Obtenir des vecteurs de direction relatifs à la caméra.
        var camera = Camera.main!;
        var cameraTransform = camera.transform;
        var up = cameraTransform.up;
        var forward = cameraTransform.forward;
        var right = cameraTransform.right;
        
        // Retirer la rotation Y (garder le mouvement horizontal).
        forward.y = 0;
        right.y = 0;
        
        // Lire les entrées du joueur.
        var moveInput = moveAction.action.ReadValue<Vector2>();
        var jumpInput = jumpAction.action.triggered;

        var horizontalMovement = Vector3.zero;
        
        // Si le joueur ne veut pas bouger, ne pas faire bouger le joueur.
        if (moveInput != Vector2.zero)
        {
            // Y multiplie forward (avance/recule).
            // X multiplie right (gauche/droite).
            // Combinaison des deux fait le mouvement total.
            var moveDirection = forward * moveInput.y + right * moveInput.x;
            horizontalMovement = moveDirection * (speed * Time.deltaTime);
            
            // Rotate player using current direction.
            var lookRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
        
        // Partie sur le saut.
        var gravity = Physics.gravity;
        var isGrounded = characterController.isGrounded;

        // La vélocité est de zéro si on touche le sol.
        if (isGrounded)
        {
            verticalVelocity = 0;
        }

        // Calculer la vélocité lorsqu'on saute.
        //
        //      Vélocité² = 2 x Accélération (Gravité) x Déplacement (Hauteur voulue)
        if (isGrounded && jumpInput)
        {
            verticalVelocity = Mathf.Sqrt(2 * -gravity.y * jumpForce);
        }

        // Appliquer la gravité.
        verticalVelocity += gravity.y * Time.deltaTime;
        
        // Calculer le mouvement vertical.
        var verticalMovement = up * (verticalVelocity * Time.deltaTime);

        // Appliquer le mouvement.
        characterController.Move(horizontalMovement + verticalMovement);
    }
}
