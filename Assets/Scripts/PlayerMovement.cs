using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public GameObject GameOver;

    public float speed = 12f;
    public float sprintSpeed = 20f;
    public float playerStamina = 2f;

    public bool sprinting = false;
    public bool stamRefil = true;

    private float stamMax;

    private void Start()
    {
        Time.timeScale = 1;
        stamMax = playerStamina;
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
            sprinting = true;
        } else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprinting = false;
        }

        //Sends the stamina to negative if player runs until stamina runs out
        if (playerStamina <= 0 && sprinting)
        {
            sprinting = false;
            playerStamina = -1f;
            stamRefil = false;
        }

        if (playerStamina == stamMax)
        {
            stamRefil = true;
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
