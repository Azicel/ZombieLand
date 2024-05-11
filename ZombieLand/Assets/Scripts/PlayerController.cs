using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public GameObject arrow;
    public Transform shootPoint;
    public float health;

    [SerializeField]private float playerSpeed = 2.0f;
    private float rotationSpeed = 10f;
    private bool aiming = false;
    private float maxHealth = 100f;
    private float force = 1f;
    private float deltaTime;
    private bool readyToShoot;


    private CharacterController controller;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction shootAction;
    private InputAction aimAction;
    private Transform cameraTransform;

    private bool once;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        shootAction = playerInput.actions["Shoot"];
        aimAction = playerInput.actions["Aim"];
        cameraTransform = Camera.main.transform;
        health = maxHealth;      
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > 0 )
        {
            controller.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        if (health <= 0)
            Destroy(gameObject);
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        if (aimAction.inProgress || aimAction.WasPerformedThisFrame())
            startAim();
        else if (aiming)
            stopAim();
        if (shootAction.WasPerformedThisFrame() || shootAction.inProgress)
        {
            AddShootingForce();
            readyToShoot = true;
        }
        else if (readyToShoot)
        {
            Shoot();
            readyToShoot = false;
        }
        controller.Move(move * Time.deltaTime * playerSpeed);
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        shootPoint.transform.rotation = Quaternion.Euler(cameraTransform.eulerAngles);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed*Time.deltaTime);    
    }

    private void AddShootingForce()
    {

        if (!aiming && !once)
        {
            playerSpeed = playerSpeed / 2;
            once = true;
        }
        if (force < 100f && deltaTime > 0.1f)
        {
            force += 10f;
            deltaTime = 0;
        }
        deltaTime += Time.deltaTime;
    }

    private void Shoot()
    {
        var shot = Instantiate(arrow, shootPoint.position, shootPoint.transform.rotation);
        shot.GetComponent<ArrowController>().force = force;
        var physics = shot.GetComponent<Rigidbody>();
        physics.AddForce(shot.transform.forward * force, ForceMode.Impulse);
        if (!aiming)
        {
            playerSpeed = playerSpeed * 2;
            once = false;
        }
        force = 1f;
    }

    private void startAim()
    {
        if(!aiming)
            playerSpeed = playerSpeed / 2;
        aiming = true;
    }
    private void stopAim()
    {
        playerSpeed = playerSpeed * 2;
        aiming = false;
    }

    public void RecieveDamage()
    {
        health -= 10;
    }

    
}
