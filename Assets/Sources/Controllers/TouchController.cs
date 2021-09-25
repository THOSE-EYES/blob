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

public class TouchController : MonoBehaviour
{
    // Constants
    private readonly float MIN_DRAG_DISTANCE = 
        Screen.height * 15 / 100;           // Constant of the minimum distance of the swipe

    // Game object's components
    private ICharacterController characterController;

    // Swipe parameters and variables
    private Vector2 startSwipe;             // Start position of the swipe
    private Vector2 endSwipe;               // End position of the swipe

    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<ICharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Filter double touches and wrong gestures
        if (Input.touchCount != 1)
        {
            return;
        }

        // Get the touch
        Touch touch = Input.GetTouch(0);
        switch (touch.phase)
        {
            // If the swipe begin, upate the position of the start of the swipe
            case TouchPhase.Began:
                startSwipe = touch.position;
                break;
            // On touch moving
            case TouchPhase.Moved:
                endSwipe = touch.position;

                // Handle the gesture
                processGesture(startSwipe, endSwipe);

                break;
            // If the swipe ends, upate the position of the end of the swipe
            case TouchPhase.Ended:
                endSwipe = touch.position;

                // Handle the gesture
                processGesture(startSwipe, endSwipe);

                break;
        }
    }

    // Method processes the given gesture
    protected void processGesture(Vector2 start, Vector2 end)
    {
        // Do nothing if it's not a swipe
        if (!isSwipe(start, end))
        {
            return;
        }

        // Handle the swipes
        if (getMotionXDistance(start, end) >= MIN_DRAG_DISTANCE)
        {
            // Handle the horizontal swipes
            onHorizontalSwipe(start, end);
        }
        else
        {
            // Handle the vertical swipes
            onVerticalSwipe(start, end);
        }
    }

    // Handle horizontal swipes
    protected void onHorizontalSwipe(Vector2 start, Vector2 end)
    {
        if (end.x > start.x)
        {
            // Move the element to the right
            characterController.move(new Vector2(1,0));
        }
        else
        {
            // Move the element to the left
            characterController.move(new Vector2(-1, 0));
        }
    }

    // Handle vertical swipes
    protected void onVerticalSwipe(Vector2 start, Vector2 end)
    {
        if (end.x > start.x)
        {
            characterController.jump();
        }
    }
        
    // Check if the gesture is a swipe
    protected bool isSwipe(Vector2 start, Vector2 end)
    {
        return (getMotionXDistance(start, end) > MIN_DRAG_DISTANCE) ||
            (getMotionYDistance(start, end) > MIN_DRAG_DISTANCE);
    }

    // Get distance of the movement along the X axis
    protected float getMotionXDistance(Vector2 start, Vector2 end)
    {
        return Mathf.Abs(endSwipe.x - startSwipe.x);
    }

    // Get distance of the movement along the Y axis
    protected float getMotionYDistance(Vector2 start, Vector2 end)
    {
        return Mathf.Abs(endSwipe.y - startSwipe.y);
    }

    // FIXME : remove
    // Get the start of the swipe
    public Vector3 getStartSwipe()
    {
        return startSwipe;
    }
}
