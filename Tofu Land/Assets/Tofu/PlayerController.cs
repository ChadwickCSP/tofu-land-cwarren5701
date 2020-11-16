using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Determines the speed that the Tofu moves 
    public float speed;
    // Determines how high the Tofu jumps
    public float jumpPower;
    //global variable that enables all inputs 
    public static bool IsInputEnabled = true;
    //determines the tofu's health hitpoints
    public float health;
    //update is called at the begining of the game 
    void Start()
    {
        //health value starts at 5 
        health = 5; 
    }
    // Update is called once per frame
    void Update()
    {
        //if all inputs are enabled 
        if (IsInputEnabled)
        {
            //when there's a horizontalInput move object on the horizontal axis
            float horizontalInput = Input.GetAxis("Horizontal");
            //move the object right or left depending on its speed 
            transform.Translate(Vector2.right * speed * Time.deltaTime * horizontalInput);

            //Assigning the space bar to the jumping movement 
            bool isJumping = Input.GetKeyDown(KeyCode.Space);

            //when ibject isjumping then...
            if (isJumping)
            {
                //move object with the force that's multiplied by the public variable "jumpPower" up
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower);
            }

            // if the player presses the right or left arrow keys(or a or d) and the object moves right or left, it will also flip so it is facing the correct direction
            if (horizontalInput < 0)
            {
                //flip the object on its x axis
                GetComponent<SpriteRenderer>().flipX = true;
            }
            //if the player presses the other of the keys for horizontal input then..
            else
            {
                //flip the object on its x axis
                GetComponent<SpriteRenderer>().flipX = false;
            }
            // if the player presses the right or left arrow keys(or a or d) the object will move accordingly
            if (horizontalInput != 0)
            {
                //set the variable "isMoving" to true in the Animator 
                GetComponent<Animator>().SetBool("isMoving", true);
            }

            // if the player doesn't press the right or left arrow keys(or a or d) the object will move accordingly
            else
            {
                //set the variable "isMoving" to true in the Animator 
                GetComponent<Animator>().SetBool("isMoving", false);
            }
        }
        //if health is less than 1 then...
        if(health < 1)
        {
            //move tofu back to starting position
            transform.position = new Vector3(-11, 1, 0);
            //set health to 5 
            health = 5;
            //set speed to 3 
            speed = 3;
        }
    }
    // method will run when a collision occurs 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // print message when collison happens 
        print(collision.gameObject);

        //assigning "enemy" to the EnemyController script so that during a collison, we can check if that script is on a object
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        //assigning "tofuFollower" to the TofuFollowerEnemyController script so that during a collison, we can check if that script is on a object
        TofuFollowerEnemyController tofuFollower = collision.gameObject.GetComponent<TofuFollowerEnemyController>();
        //assigning "tofuStunner" to the TofuStunnerEnemyController script so that during a collison, we can check if that script is on a object
        TofuStunnerEnemyController tofuStunner = collision.gameObject.GetComponent<TofuStunnerEnemyController>();
        // if EnemyController script is attached to this object, then...
        if (enemy != null)
        {
            //subtract 1 from the health value
            health += -1;
            //determining that when we refer to tofuX it means that objects position on the x axis 
            //float tofuX = this.transform.position.x;
            //determining that when we refer to enemyX it means that objects position on the x axis 
            //float enemyX = enemy.transform.position.x;
            //determining the when "isLeftofTofu" is true it really means that the tofuX is less than than enemyX
            //bool isLeftofEnemy = tofuX < enemyX;

            // if the enemy is left of the enemy then...
            //if (isLeftofEnemy)
            //{
            // have the tofu kickback on contact to the left based on the enemy strength 
            //this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * enemy.strength);



            //}
            // if the enemy is not facing left(so facing right)
            //else
            //{
            //have the tofu kickback on contact to the right based on the enemy strength
            //this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * enemy.strength);


            // }
        }
        // if TofuFollowerEnemyController script is attached to this object, then...
        if (tofuFollower != null)
        {
            //subtract 1 from the health value
            health += -1;
        }
        // if TofuStunnerEnemyController script is attached to this object, then...
        if (tofuStunner != null)
        {
            //subtract 1 from the health value
            health += -1;

        }
        //assigning "stunner" to the PlayerController script so that during a collison, we can check if that script is on a object
        TofuStunnerEnemyController stunner = collision.gameObject.GetComponent<TofuStunnerEnemyController>();
        // if stunner is attached to this object, then...
        if (stunner != null)
        {
            //if all inputs are enabled, when this collision occurs..
            if(IsInputEnabled)
            {
                //disable all inputs
                IsInputEnabled = false;
            }
        }
        //assigning "spike" to the SpikeController script so that during a collison, we can check if that script is on a object
        SpikeController spike = collision.gameObject.GetComponent<SpikeController>();
        // if SpikeController script is attached to this object, then...
        if (spike != null)
        {
            //subtract 1 from the health value
            health += -1;
        }
        //assigning "orbs" to the SpeedOrbs script so that during a collison, we can check if that script is on a object
        SpeedOrbs orbs = collision.gameObject.GetComponent<SpeedOrbs>();
        if(orbs != null)
        {
            //add 1 to the speed value 
            speed += 1;
            //destroy object that SpeedOrbs scripts is a component on
            UnityEngine.Object.Destroy(orbs.gameObject);
        }
            
    }
    

}
