using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TofuFollowerEnemyController : MonoBehaviour
{
    //if this value is true, the object is moving left 
    public bool movingLeft;

    //The amount of force to apply to Tofu on contact
    public float strength;

    //public variable to determine that whenever "tofu" is referenced it applies to the object with "PlayerController" attached
    public PlayerController tofu;

    // Update is called once per frame
    void Update()
    {
        //value that can have decimals, and refers to the speed of the object
        float speed = 0;
        //determining that when we refer to tofuX it means that objects position on the x axis 
        float tofuX = tofu.transform.position.x;
        //determining that when we refer to enemyX it means that objects position on the x axis 
        float enemyX = this.transform.position.x;
        //determining the when "isLeftofTofu" is true it really means that the tofuX is greater than enemyX
        bool isLeftofTofu = tofuX > enemyX;


        //if "isLeftofTofu" is not true
        if (!isLeftofTofu)

        {
            //speed is -1 so the object is moving left
            speed = -1;
            //when the object starts moving right, flip the object on its x object so its facing the direction its walking
            GetComponent<SpriteRenderer>().flipX = false;

        }
        //if enemy IS left of tofu
        else
        {
            //speed is 1 so the object moves right 
            speed = 1;
            //when the object starts moving right, flip the object on its x axis so its facing the direction its walking
            GetComponent<SpriteRenderer>().flipX = true;
        }
        //move the object right or left depending on its speed 
        transform.Translate(Vector2.right * speed * Time.deltaTime);                
}
    // method will run when a collision occurs 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // print message when collison happens 
        print(collision.gameObject);
        // checking to see if the object that we collide into has an edge checker 

        EdgeChecker checker = collision.gameObject.GetComponent<EdgeChecker>();
        // if the object that we collide with is not equal to null(null means that something is not a component on this object) so basically saying that if checker is a present component on this object then do...
        if (checker != null)
        {
            // so if it is a checker on this object, then make it so the enemy is not travelling left(if its not going left its going right) so basically making the enemy turn around and walk the other way
            // the "!" means the opposite of the following value

            this.movingLeft = !checker.isLeftBound;
            //when it collides with the edgechecker, then flip it on its x axis 
            GetComponent<SpriteRenderer>().flipX = false;

        }
        //assigning "playerController" to the PlayerController script so that during a collison, we can check if that script is on a object
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

        // if playercontroller is attached to this object, then...
        if (playerController != null)

        {
            //destroy this object
            UnityEngine.Object.Destroy(this.gameObject);

            // refering to the object that playerController is assigned to; and when the collison occurs, move the Tofu character(object with playercontroller) up in the air with gravity applied times 600
            playerController.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 600);
        }
    }
    // method will run when a collision occurs 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // print message when collison happens 
        print(collision.gameObject);
        //assigning "ground" to the BouncyGround script so that during a collison, we can check if that script is on a object
        BouncyGround ground = collision.gameObject.GetComponent<BouncyGround>();
        // if ground script is attached to this object, then...
        if (ground != null)
        {
            //move object with the force that's multiplied by the public variable "ground strength"
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * ground.strength);
        }
        
    
    }

}
