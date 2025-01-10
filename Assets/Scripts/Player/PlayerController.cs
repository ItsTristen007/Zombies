using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    CharacterController controller;
    PlayerWeapon weapon;
    [SerializeField] float moveSpeed = 5f;
    float normalSpeed = 5f;
    float gravityValue = -9.81f;

    Vector2 moveDirection = Vector2.zero;
    Transform cameraTransform;
    Vector3 playerVelocity = Vector3.zero;
    bool isGrounded;
    [SerializeField] float fireRate = 0.2f;
    [SerializeField] float reloadSpeed = 0.75f;
    bool isWaiting;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawn;

    Inputs playerInputs;
    InputAction move;
    InputAction look;
    InputAction shoot;
    InputAction reload;
    InputAction sprint;

    AudioSource source;
    public AudioClip shootSound;
    public AudioClip reloadSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        weapon = GetComponent<PlayerWeapon>();
        normalSpeed = moveSpeed;
        cameraTransform = Camera.main.transform;
        playerInputs = new Inputs();
        Cursor.lockState = CursorLockMode.Locked;
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        move = playerInputs.Player.Move;
        move.Enable();        

        look = playerInputs.Player.Look;
        look.Enable();

        shoot = playerInputs.Player.Fire;
        shoot.Enable();
        shoot.performed += Shoot;

        reload = playerInputs.Player.Reload;
        reload.Enable();
        reload.performed += Reload;

        sprint = playerInputs.Player.Sprint;
        sprint.Enable();
        sprint.performed += Sprint;
    }

    private void OnDisable()
    {
        move.Disable();
        look.Disable();
        shoot.Disable();
        reload.Disable();
        sprint.Disable();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        moveDirection = move.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveDirection.x, 0, moveDirection.y);
        movement = cameraTransform.forward * movement.z + cameraTransform.right * movement.x;
        movement.y = 0f;
        controller.Move(movement * moveSpeed * Time.deltaTime);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (sprint.WasReleasedThisFrame())
        {
            moveSpeed = normalSpeed;
        }
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        if (weapon.GetCurrentAmmo() > 0 && !isWaiting && !UIManager.gamePaused)
        {
            Rigidbody bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity).GetComponent<Rigidbody>();
            bullet.velocity = cameraTransform.forward * weapon.GetBulletSpeed();
            weapon.SetCurrentAmmo(weapon.GetCurrentAmmo() - 1);

            StartCoroutine(ShotDelay());

            source.PlayOneShot(shootSound);
        }
        else if (weapon.GetCurrentAmmo() == 0 && !isWaiting && !UIManager.gamePaused)
        {
            StartCoroutine(ReloadTime());
        }
    }

    private void Reload(InputAction.CallbackContext context)
    {
        if (!isWaiting && !UIManager.gamePaused)
        {
            StartCoroutine(ReloadTime());
        }
    }

    private void Sprint(InputAction.CallbackContext context)
    {
        moveSpeed *= 1.5f;
    }

    void ReloadWeapon()
    {
        weapon.SetCurrentAmmo(weapon.GetMaxAmmo());
    }

    IEnumerator ShotDelay()
    {
        isWaiting = true;
        yield return new WaitForSeconds(fireRate);
        isWaiting = false;
    }

    IEnumerator ReloadTime()
    {
        source.PlayOneShot(reloadSound, 1f);
        isWaiting = true;
        yield return new WaitForSeconds(reloadSpeed);
        ReloadWeapon();
        isWaiting = false;
    }
}
