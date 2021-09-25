/**
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 * 
 *  http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableEnemyController : StaticEnemyController
{
    public enum FacingDirection
    {
        RIGHT,
        LEFT
    };
    public FacingDirection initialDirection; // Initial direction of the movement animation

    [Range(0.0f, 10.0f)]
    public float throwForce;        // The force of throwing the player

    private Vector2 moveDirection;          // Direction of the current movement
    private Vector2 reversingPosition;      // The position of the reversing

    // Start is called before the first frame update
    new protected void Start()
    {
        base.Start();

        // Set the beginning direction
        switch (initialDirection)
        {
            case FacingDirection.RIGHT:
                moveDirection = new Vector2(1, 0);
                break;
            case FacingDirection.LEFT:
                moveDirection = new Vector2(-1, 0);
                break;
        }
    }

    // Update is called once per frame
    new protected void Update()
    {
        base.Update();

        // Move the enemy
        move(moveDirection);
    }

    // Throw the object
    protected void throwObject(Rigidbody2D body)
    {
        // Sanity check
        if (body == null) return;

        // Throw the body
        body.AddForce(new Vector2(0, throwForce), ForceMode2D.Impulse);
    }

    //Upon collision with another GameObject, this GameObject will reverse direction
    new protected void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if object collides with the player
        if (TouchedPlayer(collision))
        {
            // If the player has jumped on the object, throw it
            var player = collision.gameObject.GetComponent<BlobController>();
            if (PressedByPlayer(collision))
            {
                throwObject(player.getBody());
                return;
            }

            // Kill the player
            player.kill();
        }
    }

    // This method is called when the collision is continuous
    new protected void OnCollisionStay2D(Collision2D collision)
    {
        // Turn if the object should be turned
        if (ShouldTurn(collision)) turn();

        // Check if object collides with the player
        if (TouchedPlayer(collision))
        {
            // If the player has jumped on the object, throw it
            var player = collision.gameObject.GetComponent<BlobController>();
            if (PressedByPlayer(collision))
            {
                throwObject(player.getBody());
                return;
            }

            // Kill the player
            player.kill();
        }
    }

    // Check if the player jumped on the object
    private bool PressedByPlayer(Collision2D collision)
    {
        // Sanity check
        if (!TouchedPlayer(collision)) return false;

        // Return True if the player jumped on the object
        var directions = getCollisionDirections(collision);
        foreach (Vector2 normal in directions)
        {
            if (Math.Round(normal.y, 2) == -1.0 &&
                Math.Round(normal.x, 2) == 0.0) return true;
        }

        return false;
    }

    // Check if the player touched the object
    private bool TouchedPlayer(Collision2D collision)
    {
        var gameObject = collision.gameObject;
        return gameObject.GetComponent<BlobController>() != null;
    }

    private bool ShouldTurn(Collision2D collision)
    {
        // Check if the object touched something besides the player
        var directions = getCollisionDirections(collision);
        foreach (Vector2 normal in directions)
        {
            // If the enemy encountered an obstacle, which is not the player,
            // reverse it's sprite and move into another direction
            if (Math.Round(normal.y, 2) == 0.0 &&
                reversingPosition != getPosition()) return true;
        }

        return false;
    }

    // This method is called when the collision is ended
    new protected void OnCollisionExit2D(Collision2D collision)
    {
        base.OnCollisionExit2D(collision);
    }

    // Turn the object
    public void turn()
    {
        // Reverse the direction
        moveDirection.x = -moveDirection.x;

        // Save the position
        reversingPosition = getPosition();

        // Reverse the sprite
        var animator = getAnimator();
        animator.transform.Rotate(new Vector3(0, 180, 0));
    }

    // FIXME : remove
    // The method is used to update the parameters of the RigidBody2D component
    override protected void setBodyParameters()
    {
        var body = getBody();

        // TODO
    }

    // FIXME : remove
    // The method is used to update the parameters of the controller
    override protected void setControllerParameters()
    {
        // TODO
    }

    // FIXME : remove
    // The method is used to update the parameters of the collider
    override protected void setColliderParameters()
    {
        var collider = getCollider() as BoxCollider2D;

        // TODO
    }

}
