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

// FIXME : rename to MainCharacterController
public class BlobController : ICharacterController
{
    // Start is called before the first frame update
    new public void Start()
    {
        base.Start();
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

        // TODO
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

    // This method is called to kill the character
    public void kill()
    {
        // TODO : create a death animation

        // Reset the level
        Application.LoadLevel(Application.loadedLevel);
    }

    // The method is called on colliding with an object
    new protected void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object collides with anything but the ground
        var directions = getCollisionDirections(collision);
        foreach (Vector2 normal in directions)
        {
            // If the collision occured with a new object
            if (Math.Round(normal.y, 2) != 1.0) setIsColliding(true);
        }
    }

    // This method is called when the collision is continuous
    new protected void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);

        // Reset the colliding flag
        setIsColliding(false);

        // Check if the object collides with anything but the ground
        var directions = getCollisionDirections(collision);
        foreach (Vector2 normal in directions)
        {
            // If the collision occured with a new object
            if (Math.Round(normal.y, 2) != 1.0) setIsColliding(true);
        }
    }
}