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

public class StaticEnemyController : ICharacterController
{
    // Start is called before the first frame update
    new protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new protected void Update()
    {
        base.Update();
    }

    // The method is called after the fixed amount of time
    new protected void FixedUpdate()
    {
        base.FixedUpdate();
    }

    // Upon collision with another GameObject, this GameObject will reverse direction
    new protected void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        // If the object touched the player, kill it
        if (TouchedPlayer(collision))
        {
            // Kill the player
            var player = collision.gameObject.GetComponent<BlobController>();
            player.kill();
        }
    }

    // This method is called when the collision is continuous
    new protected void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);

        // If the object touched the player, kill it
        if (TouchedPlayer(collision))
        {
            // Kill the player
            var player = collision.gameObject.GetComponent<BlobController>();
            player.kill();
        }
    }

    // Check if the player touched the object
    private bool TouchedPlayer(Collision2D collision)
    {
        var gameObject = collision.gameObject;
        return gameObject.GetComponent<BlobController>() != null;
    }
}
