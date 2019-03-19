using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  /** Player movement params */
  private RigidBody2D playerRB; // Used to apply forces - we're not going to use accel
  private float moveSpeed; // Movement force we want to apply
  private Vector2 currDirection; // Used for animations
}
