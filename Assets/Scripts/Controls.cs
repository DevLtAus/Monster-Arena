using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    private PlayerControls controls;
    Weapon weapon;

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

        controls.Player.Shoot.performed += _ => weapon.Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = controls.Player.MousePosition.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        weapon.Aim(mouseWorldPosition);
    }
}
