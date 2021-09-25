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

public class SawController : IItemController
{
    public float rotationSpeed;     // Rotation speed of the object

    // Start is called before the first frame update
    new public void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new public void Update()
    {
        base.Update();

        // Rotate the saw
        float angle = 0;
        while (true)
        {
            setRotation(angle);
            angle += rotationSpeed;
        }
    }

    // FIXME : remove
    // The method is used to update the parameters of the RigidBody2D component
    override protected void setBodyParameters()
    {
        var body = getBody();

        // Disable gravity for saws as it won't be affected by the gravity
        body.gravityScale = 0;
    }

    // FIXME : remove
    // The method is used to update the parameters of the controller
    override protected void setControllerParameters()
    {
        // Set the ability to move
        movable = false;
        resizable = false;
        rotatable = true;
    }

    // FIXME : remove
    // The method is used to update the parameters of the collider
    override protected void setColliderParameters()
    {
        var collider = getCollider() as CircleCollider2D;

        collider.radius = gameObject.GetComponent<Renderer>().bounds.size.x;
    }
}
