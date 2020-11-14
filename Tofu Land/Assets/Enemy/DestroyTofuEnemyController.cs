﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTofuEnemyController : MonoBehaviour
{
    public bool movingLeft;

    //The amount of force to apply to Tofu on contact
    public float strength;

    // Update is called once per frame
    void Update()
    {
        float speed = 0;

        if (movingLeft)
        {
            speed = -1;
        }
        else
        {
            speed = 1;
            GetComponent<SpriteRenderer>().flipX = true;
        }

        transform.Translate(Vector2.right * speed * Time.deltaTime);
        
    }

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
            GetComponent<SpriteRenderer>().flipX = false;

        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject);
        BouncyGround ground = collision.gameObject.GetComponent<BouncyGround>();
        if (ground != null)
        {
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * ground.strength);
            
        }
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null)

        {
                float tofuX = playerController.transform.position.x;
                float enemyX = this.transform.position.x;
                bool isLeftofEnemy = tofuX < enemyX;

                // if the enemy is left of the enemy then...
                if (isLeftofEnemy)
                {
                    // have the tofu kickback on contact to the left based on the enemy strength 
                    UnityEngine.Object.Destroy(playerController.gameObject);



                }
                // if the enemy is not facing left(so facing right)
                else
                {
                //have the tofu kickback on contact to the right based on the enemy strength
                UnityEngine.Object.Destroy(playerController.gameObject);


            }

            }
        }
    }