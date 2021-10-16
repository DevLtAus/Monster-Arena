using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    private PlayerControls controls;
    Weapon weapon;
    MovePlayer player;
    CameraMovement cam;
    private bool useController = true;
    private Vector2 prevMouseCoor;
    private Vector2 prevCoor;

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
        prevMouseCoor = new Vector2(0, 0);
        prevCoor = new Vector2(0, 0);

        // Shooting
        controls.Player.Shoot.performed += _ => weapon.Shoot();
    }

    // Update is called once per frame
    void Update()
    {   
        // (Elliot) Check if player is using mouse or controller
        Vector2 mousePosition = controls.Player.MousePosition.ReadValue<Vector2>();
        Vector2 stickPosition = controls.Player.StickPosition.ReadValue<Vector2>();
        if (stickPosition != new Vector2(0, 0) && stickPosition != prevCoor) {
            useController = true;
        }
        if (mousePosition != prevMouseCoor) {
            useController = false;
        }

        if (!useController) {
            // Weapon rotation
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            weapon.Aim(mouseWorldPosition);

            // Camera movement
            cam.SetMouseInput(new Vector3(mousePosition.x, mousePosition.y, 0));
            
            prevMouseCoor = mousePosition;

        } else {
            // Weapon Controller rotation
            if (stickPosition == new Vector2(0, 0)) {
                stickPosition = prevCoor;
            }
            Vector3 stickWorldPosition = weapon.transform.position + new Vector3(stickPosition.x, stickPosition.y, 0);
            weapon.Aim(stickWorldPosition);
            
            // Camera Controller movement 
            Vector3 stickScreenPosition = Camera.main.WorldToScreenPoint(stickWorldPosition);
            cam.SetMouseInput(stickScreenPosition);

            prevCoor = stickPosition;
        }

        // Player Movement
        Vector3 movement = controls.Player.Movement.ReadValue<Vector2>();
        player.SetHorizontalInput(movement.x);

        // Jumping
        controls.Player.Jump.performed += _ => player.JumpButtonDown();
        controls.Player.Jump.performed += _ => player.SetJumpInput(true);
        controls.Player.Jump.canceled += _ => player.SetJumpInput(false);
    }
}
