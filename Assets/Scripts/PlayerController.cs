using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
 public Vector2 moveValue;
 public float speed;

 void OnMove(InputValue value)
 {
  moveValue = value.Get<Vector2>();
 }

 private void FixedUpdate()
 {
  Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);

  var force = movement * speed * Time.fixedDeltaTime;
  // var force = movement * speed;
  
  GetComponent<Rigidbody>().AddForce(force);
 }
}
