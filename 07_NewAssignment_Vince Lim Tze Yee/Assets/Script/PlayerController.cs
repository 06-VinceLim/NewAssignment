using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Declaration
    bool isOnGround;

    float jumpForce = 10.0f;
    float gravityModifer = 2.0f;
    float zLimit = 20.0f;
    float speed = 10.0f;

    Rigidbody playerRb;
    Renderer playerRdr;

    public Material[] playerMaterials; //For me to use array for material colour

    // Start is called before the first frame update
    void Start()
    {
        isOnGround = true; //Checking if player has touched the ground via Collision check

        Physics.gravity *= gravityModifer;

        playerRb = GetComponent<Rigidbody>(); //Make use of "Rigidbody"
        playerRdr = GetComponent<Renderer>(); //Make use of the pre-set colour in the libary
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        //move player (GameObject) according to user input
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);

        if (transform.position.z < -zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zLimit);

            playerRdr.material = playerMaterials[2];
        }
        else if (transform.position.z > zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zLimit);

            playerRdr.material = playerMaterials[3];
        }
        if (transform.position.x < -zLimit)
        {
            transform.position = new Vector3(-zLimit,transform.position.y, transform.position.z);

            playerRdr.material = playerMaterials[4];
        }
        else if (transform.position.x > zLimit)
        {
            transform.position = new Vector3(zLimit, transform.position.y, transform.position.z);

            playerRdr.material = playerMaterials[5];
        }

        PlayerJump(); //Allow player to make use of the jump code.
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GamePlane")) //Checking if the player has touched the plane with the tag "GamePlane"
        {
            isOnGround = true; //Checking if player has touched the ground via Collision check

            //playerRdr.material = playerMaterials[1]; //Change of Color according to array
        }
    }

    private void PlayerJump()
    {
       if( Input.GetKeyDown(KeyCode.Space)&& isOnGround) //If spacebar is pressed and player is ON the ground
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //Jumping the player upwards

            isOnGround = false; //Checking if player has touched the ground via Collision check

            playerRdr.material = playerMaterials[0]; //Change of Color according to array
        }
    }
}
