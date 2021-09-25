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
using System.Threading;
using UnityEngine;

abstract public class ICharacterController : IItemController
{
    // Private fields
    private bool isJumping = false;     // Flag of jumping
    private bool isMoving = false;      // Flag of moving
    private bool isColliding = false;   // Flag of colliding

    // Threads
    private Thread jumpThread;          // Current thread of waiting for the jumping flag to be reset
    private Thread moveThread;          // Current thread of waiting for the moving flag to be reset

    // Parameters visible in the editor
    [Range(0.0f, 10.0f)]
    public float rotationSpeed;         // The speed of rotation of the character
    [Range(0.0f, 10.0f)]
    public float moveSpeed;             // The speed of motion of the character
    [Range(0.0f, 10.0f)]
    public float jumpSpeed;             //  The speed of jumping of the character
    [Range(0.0f, 10.0f)]
    public float moveForce;             // The force of the movement
    [Range(0.0f, 10.0f)]
    public float jumpForce;             // The force of jumps

    // Start is called before the first frame update
    new public void Start()
    {
        base.Start();

        // TODO
    }

    // Update is called once per frame
    new public void Update()
    {
        base.Update();

        // TODO
    }

    // The method is called after the fixed amount of time
    new protected void FixedUpdate()
    {
        base.FixedUpdate();

        // Update Animator properties
        var animator = getAnimator();
        if (animator != null)
        {
            animator.SetBool("isJumping", isJumping);
            animator.SetBool("isMoving", isMoving);
        }
    }

    // Called on application exit
    new protected void OnApplicationQuit()
    {
        base.OnApplicationQuit();

        // Stop the threads
        if (jumpThread != null) jumpThread.Abort();
        if (moveThread != null) moveThread.Abort();
    }

    // Get the moving flag
    public bool getIsMoving()
    {
        return isMoving;
    }

    // Get the jumping flag
    public bool getIsJumping()
    {
        return isJumping;
    }

    // Update the moving flag
    public void setIsMoving(bool value)
    {
        // Change the flag if it is changed
        if (isMoving != value) isMoving = value;
    }

    // Update the jumping flag
    public void setIsJumping(bool value)
    {
        // Change the flag if it is changed
        if (isJumping != value) isJumping = value;
    }

    // Get the colliding flag
    public bool getIsColliding()
    {
        return isColliding;
    }

    // Update the moving flag
    public void setIsColliding(bool value)
    {
        // Change the flag if it is changed
        if (isColliding != value) isColliding = value;
    }

    // Makes the character jump
    public void jump()
    {
        // Sanity check
        var body = getBody();
        if (isJumping || isColliding || body == null) return;

        // Set the speed but don't surpass the max value
        body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        if (body.velocity.y > jumpSpeed)
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);

        // Set the flag
        setIsJumping(true);

        // If the thread is not alive
        if (jumpThread == null || !jumpThread.IsAlive)
        {
            Action<ICharacterController> wait = (ICharacterController controller) =>
            {
                // Busy-wait 
                while (Math.Round(controller.getVelocity().y, 1) != .0f) ;

                // Reset the flag
                controller.setIsJumping(false);
            };

            // Start the thread
            jumpThread = new Thread(() => wait(this));
            jumpThread.IsBackground = true;
            jumpThread.Start();
        }
    }

    // Makes the character move
    public void move(Vector2 direction)
    {
        // Sanity check
        var body = getBody();
        if (direction.Equals(null) ||
            direction.normalized != direction ||
            direction.y != 0 ||
            Math.Abs(getVelocity().x) > moveSpeed||
            body == null) return;

        // Set the speed
        body.AddForce(direction * moveForce);
        setIsMoving(true);

        // If the thread is not alive
        if (moveThread == null || !moveThread.IsAlive)
        {
            Action<ICharacterController> wait = (ICharacterController controller) => 
            {
                // Busy-wait 
                while (Math.Round(controller.getVelocity().x, 2) != .0f) ;

                // Reset the flag
                controller.setIsMoving(false);
            };

            // Start the thread
            moveThread = new Thread(() => wait(this));
            moveThread.IsBackground = true;
            moveThread.Start();
        }
    }

    // Stop the character
    public void stop()
    {
        // Change velocity to zero
        setVelocity(new Vector2(0, 0));

        // Reset the flags
        isMoving = false;
        isJumping = false;
    }

    // The method is called on colliding with an object
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        // Object starts to collide with an object
        setIsColliding(true);
    }

    // This method is called when the collision is continuous
    protected void OnCollisionStay2D(Collision2D collision)
    {
        // TODO
    }

    // This method is called when the collision is ended
    protected void OnCollisionExit2D(Collision2D collision)
    {
        // The collision is ended
        setIsColliding(false);
    }

    // Get the normals of the collisions
    protected Vector2[] getCollisionDirections(Collision2D collision)
    {
        // Store the normals of the touches
        var directions = new Vector2[collision.contactCount];
        for (int index = 0; index < collision.contactCount; ++index)
            directions[index] = collision.contacts[index].normal;

        return directions;
    }
}
