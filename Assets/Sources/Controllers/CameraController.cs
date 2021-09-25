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

public class CameraController : MonoBehaviour
{
    // Struct is used to store camera moving borders
    [System.Serializable]
    public struct Borders
    {
        [Range(0.0f, 20.0f)]
        public float top;       // Top border of the camera

        [Range(0.0f, 20.0f)]
        public float bottom;    // Bottom border of the camera

        [Range(0.0f, 20.0f)]
        public float left;      // Left border of the camera

        [Range(0.0f, 20.0f)]
        public float right;     // Right border of the camera
    }

    public GameObject player;       // Public variable to store a reference to the player game object
    public Borders borders;         // Current borders for the camera
    
    // Use this for initialization
    void Start()
    {
        // TODO
    }

    // Update is called once after the frame
    void LateUpdate()
    {
        // Follow the current object
        followObject(player);
    }

    // Get the offset between the camera an the player
    protected Vector3 getOffset(GameObject gameObject)
    {
        return transform.position - gameObject.transform.position;
    }

    // Try to follow the object
    protected void followObject(GameObject gameObject)
    {
        Vector3 cameraPosition = getPosition();                     // Current camera position
        Vector3 playerPosition = gameObject.transform.position;     // Current player position
        Vector3 offset = getOffset(gameObject);                     // Offset between the camera and the player

        // Check if the object is out of the borders along X axis
        if (offset.x >  borders.right || 
            offset.x <  (-borders.left))
        {
            cameraPosition.x = playerPosition.x;
            cameraPosition.x += (offset.x > borders.right) ? borders.right : 0;
            cameraPosition.x -= (offset.x < (-borders.left)) ? borders.left : 0;
        }

        // Check if the object is out of the borders along Y axis
        if (offset.y > borders.top ||
            offset.y < (-borders.bottom))
        {
            cameraPosition.y = playerPosition.y;
            cameraPosition.y += (offset.y > borders.top) ? borders.top : 0;
            cameraPosition.y -= (offset.y < (-borders.bottom)) ? borders.bottom : 0;
        }

        // If the camera moved, update the position
        if (cameraPosition != getPosition()) setPosition(cameraPosition);
    }

    // Get the position of the camera
    protected Vector3 getPosition()
    {
        return transform.position;
    }

    // Update the position of the camera
    protected void setPosition(Vector3 value)
    {
        // Sanity check and position update
        if (value != null) transform.position = value;
    }
}
