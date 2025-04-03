using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    CharacterController controller;
    PlayerWeapon weapon;
    PlayerScore score;
    GameObject barrier;
    [SerializeField] float moveSpeed = 5f;
    float normalSpeed = 5f;
    float gravityValue = -9.81f;

    Vector2 moveDirection = Vector2.zero;
    Transform cameraTransform;
    Vector3 playerVelocity = Vector3.zero;
    bool isGrounded;
    bool isWaiting;
    bool nearBarrier;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawn;

    [SerializeField] GameObject reloadTimeBar;
    [SerializeField] Slider reloadSlider;
    [SerializeField] TextMeshProUGUI purchaseText;

    Inputs playerInputs;
    InputAction move;
    InputAction look;
    InputAction shoot;
    InputAction reload;
    InputAction sprint;
    InputAction interact;

    AudioSource source;
    public AudioClip pistolShootSound;
    public AudioClip pistolReloadSound;
    public AudioClip shotgunShootSound;
    public AudioClip shotgunReloadSound;
    public AudioClip smgShootSound;
    public AudioClip smgReloadSound;
    public AudioClip rifleShootSound;
    public AudioClip rifleReloadSound;

    public AudioClip doorBuySound;
    public AudioClip doorFailSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        weapon = GetComponent<PlayerWeapon>();
        score = GetComponent<PlayerScore>();
        normalSpeed = moveSpeed;
        cameraTransform = Camera.main.transform;
        purchaseText.text = "";
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

        interact = playerInputs.Player.Interact;
        interact.Enable();
        interact.performed += Interact;
    }

    private void OnDisable()
    {
        move.Disable();
        look.Disable();
        shoot.Disable();
        reload.Disable();
        sprint.Disable();
        interact.Disable();
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

            if (weapon.GetUsingPistol())
            {
                source.PlayOneShot(pistolShootSound);
            }
            else if (weapon.GetUsingShotgun())
            {
                source.PlayOneShot(shotgunShootSound);
            }
            else if (weapon.GetUsingSMG())
            {
                source.PlayOneShot(smgShootSound);
            }
            else
            {
                source.PlayOneShot(rifleShootSound);
            }
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

    private void Interact(InputAction.CallbackContext context)
    {
        if (nearBarrier && !UIManager.gamePaused)
        {
            if (barrier.GetComponent<BuyableBarrier>().GetBarrierCost() <= score.GetScore())
            {
                score.ChangeScore(-barrier.GetComponent<BuyableBarrier>().GetBarrierCost());
                Destroy(barrier);
                purchaseText.text = "";
                source.PlayOneShot(doorBuySound);
            }
            else
            {
                source.PlayOneShot(doorFailSound);
            }
        }
    }

    void ReloadWeapon()
    {
        weapon.SetCurrentAmmo(weapon.GetMaxAmmo());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Barrier"))
        {
            nearBarrier = true;
            barrier = other.gameObject;
            purchaseText.text = $"Press 'E' to open barrier ({barrier.GetComponent<BuyableBarrier>().GetBarrierCost()} score)";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Barrier"))
        {
            nearBarrier = false;
            purchaseText.text = "";
        }
    }

    IEnumerator ShotDelay()
    {
        isWaiting = true;
        yield return new WaitForSeconds(weapon.GetFireRate());
        isWaiting = false;
    }

    IEnumerator ReloadTime()
    {
        if (weapon.GetUsingPistol())
        {
            source.PlayOneShot(pistolReloadSound, 1f);
        }
        else if (weapon.GetUsingShotgun())
        {
            source.PlayOneShot(shotgunReloadSound, 1f);
        }
        else if (weapon.GetUsingSMG())
        {
            source.PlayOneShot(smgReloadSound, 1f);
        }
        else
        {
            source.PlayOneShot(rifleReloadSound, 1f);
        }
        StartCoroutine(ReloadSliderTimer());
        isWaiting = true;
        yield return new WaitForSeconds(weapon.GetReloadSpeed());
        ReloadWeapon();
        isWaiting = false;
        reloadTimeBar.SetActive(false);
    }

    IEnumerator ReloadSliderTimer()
    {
        float totalTime = 0f;
        reloadTimeBar.SetActive(true);
        reloadSlider.maxValue = weapon.GetReloadSpeed();
        reloadSlider.value = 0f;

        while (reloadSlider.value < weapon.GetReloadSpeed())
        {
            totalTime += Time.deltaTime;
            reloadSlider.value = totalTime;
            yield return null;
        }
    }
}
