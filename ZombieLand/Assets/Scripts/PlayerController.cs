using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject arrow;
    public Transform shootPoint;
    public float health;

    private float playerSpeed = 2.0f;
    private float rotationSpeed = 10f;
    private bool aiming = false;
    private float maxHealth = 100f;

    private CharacterController controller;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction shootAction;
    private InputAction aimAction;
    private Transform cameraTransform;

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
        if (shootAction.WasPerformedThisFrame())
            Shoot();
        controller.Move(move * Time.deltaTime * playerSpeed);
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed*Time.deltaTime);
    }

    private void Shoot()
    {
        if (!aiming)
            playerSpeed = playerSpeed / 2;
        var shot = Instantiate(arrow, shootPoint.position, transform.rotation);
        var physics = shot.GetComponent<Rigidbody>();
        physics.AddForce(shot.transform.forward * 10f, ForceMode.Impulse);
        if (!aiming)
            playerSpeed = playerSpeed * 2;
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
