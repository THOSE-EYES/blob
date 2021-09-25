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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IItemController : MonoBehaviour
{
    // Game object's components
    private Rigidbody2D body;           // RigidBody2D component on the object
    new private Collider2D collider;    // Collider2D component on the object
    private Animator animator;          // Animator component on the object

    // Flags
    public bool movable;        // The flag to allow the object to move 
    public bool resizable;      // The flag to allow the object to be resized
    public bool rotatable;      // The flag to allow the object to be rotated

    private Vector2 velocity;   // Current velocity of the component
    private Vector2 position;   // Current position of the component

    // Start is called before the first frame update
    protected void Start()
    {
        // Get the components
        body = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<Collider2D>();
        animator = gameObject.GetComponent<Animator>();

        // Set controller parameters
        setControllerParameters();

        // Set collider parameters
        setColliderParameters();

        // Set body parameters
        setBodyParameters();
    }

    // Update is called once per frame
    protected void Update()
    {
        // Sanity check
        var body = getBody();
        if (body == null) return;
        
        // TODO
    }

    // The method is called after the fixed amount of time
    protected void FixedUpdate()
    {
        // Sanity check
        var body = getBody();
        if (body == null) return;

        // Update the controller body parameters
        velocity = body.velocity;
        position = body.position;
    }

    // Called on application exit
    protected void OnApplicationQuit()
    {
        // Do nothing here
    }

    // Return object's collider component
    public Collider2D getCollider()
    {
        return collider;
    }

    // Return object's body component
    public Rigidbody2D getBody()
    {
        return body;
    }

    // Return object's animator component
    public Animator getAnimator()
    {
        return animator;
    }

    // Update the rotation angle of the object
    public void setRotation(float value)
    {
        if (!value.Equals(0) && rotatable)
        {
            body.rotation = value;
        }
    }

    // Get current position
    public Vector2 getPosition()
    {
        return position;
    }

    // Set new position of the object
    protected void setPosition(Vector2 value)
    {
        if (!value.Equals(null) && movable)
        {
            body.position = value;
        }
    }

    // Get the velocity of the object
    public Vector2 getVelocity()
    {
        return velocity;
    }

    // Set velocity of the object
    protected void setVelocity(Vector2 value)
    {
        if (!value.Equals(null) && movable)
        {
            body.velocity = value;
        }
    }

    // FIXME : remove
    // The method is used to update the parameters of the RigidBody2D component
    virtual protected void setBodyParameters()
    {
        // Do nothing
    }

    // FIXME : remove
    // The method is used to update the parameters of the controller
    virtual protected void setControllerParameters()
    {
        // Do nothing
    }

    // FIXME : remove
    // The method is used to update the parameters of the collider
    virtual protected void setColliderParameters()
    {
        // Do nothing
    }
}
