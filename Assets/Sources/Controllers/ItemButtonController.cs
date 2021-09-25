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

public class ItemButtonController : MonoBehaviour
{
    private ICharacterController characterController;       // The object to control

    // Start is called before the first frame update
    void Start()
    {
        // Get the controller object
        characterController = gameObject.GetComponent<ICharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move to the right if the pressed key is the right arrow key
        if (Input.GetKey(KeyCode.RightArrow))
            characterController.move(new Vector2(1, 0));

        // Move to the left if the pressed key is the left arrow key
        if (Input.GetKey(KeyCode.LeftArrow))
            characterController.move(new Vector2(-1, 0));

        // Jump if the pressed key is the up arrow key
        if (Input.GetKey(KeyCode.UpArrow))
            characterController.jump();
    }
}
