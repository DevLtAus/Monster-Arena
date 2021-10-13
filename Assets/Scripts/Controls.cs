using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    private PlayerControls controls;
    Weapon weapon;
    MovePlayer player;
    CameraMovement cam;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
        player = this.GetComponent<MovePlayer>();
        cam = GetComponentInChildren<CameraMovement>();

        //Shooting
        controls.Player.Shoot.performed += _ => weapon.Shoot();
    }

    // Update is called once per frame
    void Update()
    {   
        //Weapon rotation
        Vector2 mousePosition = controls.Player.MousePosition.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        weapon.Aim(mouseWorldPosition);
        //Camera movement
        cam.SetMouseInput(mouseWorldPosition);

        //Player Movement
        Vector3 movement = controls.Player.Movement.ReadValue<Vector2>();
        player.SetHorizontalInput(movement.x);

        //Jumping
        controls.Player.Jump.performed += _ => player.JumpButtonDown();
        controls.Player.Jump.performed += _ => player.SetJumpInput(true);
        controls.Player.Jump.canceled += _ => player.SetJumpInput(false);
    }
}
