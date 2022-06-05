using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public GameObject GameOver;

    public AudioSource movementSound;
    public List<AudioClip> footsteps = new List<AudioClip>(2);

    public float speed = 12f;
    public float sprintSpeed = 20f;
    public float playerStamina = 2f;

    public bool sprinting = false;
    public bool stamRefil = true;

    private float stamMax;
    public bool paused;
    public GameObject pauseMenu;

    private void Start()
    {
        Time.timeScale = 1;
        stamMax = playerStamina;
        movementSound.clip = footsteps[0];
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        //Sprints when player hits shift unless no stamina, ends sprint when shift lifted
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerStamina > 0 && stamRefil)
        {
            movementSound.clip = footsteps[1];
            sprinting = true;
        } else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            movementSound.clip = footsteps[0];
            sprinting = false;
        }

        //Sends the stamina to negative if player runs until stamina runs out
        if (playerStamina <= 0 && sprinting)
        {
            movementSound.clip = footsteps[0];
            sprinting = false;
            playerStamina = -1f;
            stamRefil = false;
        }

        if (playerStamina == stamMax)
        {
            stamRefil = true;
        }

        if (move != Vector3.zero && !movementSound.isPlaying)
        {
            movementSound.Play(0);
        }
        else if (move == Vector3.zero)
        {
            movementSound.Stop();
        }

        //Moves faster when sprinting but consumes stamina, when not moves slower and restores stamina slower to a cap
        if (sprinting)
        {
            controller.Move(move * sprintSpeed * Time.deltaTime);
            playerStamina -= 1 * Time.deltaTime;
        } else
        {
            controller.Move(move * speed * Time.deltaTime);
            playerStamina += 0.5f * Time.deltaTime;
            playerStamina = Mathf.Clamp(playerStamina, -4f, stamMax);
        }


        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            Debug.Log("Paused the game");
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            paused = true;
            Cursor.lockState = CursorLockMode.None;

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            Debug.Log("Unpaused the game");
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            paused = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("Player Loses");
            Time.timeScale = 0;
            GameOver.SetActive(true);
        }
    }
}
