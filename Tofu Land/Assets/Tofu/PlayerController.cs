using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Determines the speed that the Tofu moves 
    public float speed;
    // Determines how high the Tofu jumps
    public float jumpPower;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * speed * Time.deltaTime * horizontalInput);

        //Assigning the space bar to the jumping movement 
        bool isJumping = Input.GetKeyDown(KeyCode.Space);

        // Add code to make it so that when you press space the Tofu jumps
        if(isJumping)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower);
        }

        // if the player presses the right or left arrow keys(or a or d) and the object moves forward or backwards, it will also flip so it is facing the correct direction
        if (horizontalInput < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        } else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        // if the player presses the right or left arrow keys(or a or d) the object will move accordingly
        if(horizontalInput !=0)
        {
            GetComponent<Animator>().SetBool("isMoving", true);
        } else
        {
            GetComponent<Animator>().SetBool("isMoving", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject);

        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        if(enemy != null)
        {
            // if the enemy is facing left then...
            if(GetComponent<SpriteRenderer>().flipX == false)
            {
                // have the tofu kickback on contact to the left based on the enemy strength 
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * enemy.strength);
            }
            // if the enemy is not facing left(so facing right)
            else
            {
                //have the tofu kickback on contact to the right based on the enemy strength
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * enemy.strength);

            }
        }
    }


}
