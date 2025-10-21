using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceMarine : MonoBehaviour, IHurtable
{
    [Header("Movement")]
    [SerializeField] private float speed = 25f;
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private float jumpForce = 10f;
    [Header("Infos")]
    [SerializeField] private int maxHealthPoints = 100;
    [SerializeField] private float invulnerabilityTime = 1.5f;
    [Header("Inputs")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference cameraAction;
    [SerializeField] private InputActionReference projectileAction;
    [SerializeField] private InputActionReference missileAction;
    
    [Header("Firing")]
    [SerializeField] private GameObject projectileSpawnPoint;
    
    private int healthPoints = 50;
    private float remainingInvulnerabilityTime;
    private CharacterController characterController;
    private float verticalVelocity;
    private bool followCamera = false;

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

        if (cameraAction.action.triggered)
        {
            followCamera = !followCamera;
        }
        
        // Retirer la rotation Y (garder le mouvement horizontal).
        forward.y = 0;
        right.y = 0;
        
        // Lire les entrées du joueur.
        var moveInput = moveAction.action.ReadValue<Vector2>();
        var jumpInput = jumpAction.action.triggered;
        var projectileInput = projectileAction.action.triggered;
        var missileInput = missileAction.action.triggered;

        var horizontalMovement = Vector3.zero;
        
        // Si le joueur ne veut pas bouger, ne pas faire bouger le joueur.
        if (moveInput != Vector2.zero)
        {
            // Y multiplie forward (avance/recule).
            // X multiplie right (gauche/droite).
            // Combinaison des deux fait le mouvement total.
            var moveDirection = forward * moveInput.y + right * moveInput.x;
            horizontalMovement = moveDirection * (speed * Time.deltaTime);
            
            if (!followCamera)
            {
                // Rotate player using current direction.
                var lookRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            }
        }
        
        // ALTERNATIVE POUR CAMERA FOLLOW
        #region Camera rotation follow

        if (followCamera)
        {
        
            var cameraForward = cameraTransform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();
    
            if (cameraForward != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(cameraForward);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }    
        }
                
        #endregion
        
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
        
        // Gérer les inputs de tir.
        if (projectileInput)
        {
            FireProjectile();
        }

        if (missileInput)
        {
            FireMissile();
        }

        if (healthPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void FireProjectile()
    {
        Finder.ObjectPools.Projectile.Place(projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);
    }

    private void FireMissile()
    {
        Finder.ObjectPools.Missile.Place(projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);
    }

    public void Hurt(int damage)
    {
        healthPoints -= damage;
    }
}
