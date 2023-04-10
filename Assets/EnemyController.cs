using System.Collections;
using System.Collections.Generic;
using DD;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
  
  // private float _health;
  private bool _targettable;

  public float movementSpeed;
  private float escapeSpeed = 0.2F;
  private float chaseSpeed = 0.4F;

  public float minDistance;
  private float escapeDistance = 0.5F;
  // private float chaseDistance = 0.1F;

  public string attitude = "idle";
  private string[] attitudes = {
    "idle",
    "escape",
    "persue"
  };

  GameObject Player;
  TextMesh label;
  Animator animator;
  Rigidbody2D rb;

  void Start() {
    Player = GameObject.FindGameObjectWithTag("Player");
    label = GetComponentInChildren<TextMesh>();
    animator = GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();

    // defaults
    movementSpeed = 0.3F;
    minDistance = 0.3F;
    // _health = 20;

    // what is my mood?
    // int i = Random.Range(0, attitudes.Length);
    attitude = attitudes[0];
    // label.text = attitude + " " + _health;
  }

  private void Update() {

    switch (attitude) {
      case "idle":
        animator.SetBool("isMoving", false);
        break;

      case "persue":
        // do we need to move?
        if (Vector2.Distance(transform.position, Player.transform.position) > minDistance) {
          animator.SetBool("isMoving", true);
          // move
          transform.position = Vector2.MoveTowards(transform.position, 
                                                    Player.transform.position, 
                                                    chaseSpeed * Time.deltaTime);
        } else {
          animator.SetBool("isMoving", false);

          // no need to move, already here!
          // now what?
        }
        break;

      case "escape":
        // do we need to move?
        // speed up
        animator.SetBool("isMoving", true);
        if (Vector2.Distance(transform.position, Player.transform.position) < escapeDistance) {
          // move
          transform.position = Vector2.MoveTowards(transform.position, 
                                                    Player.transform.position, 
                                                    -escapeSpeed * Time.deltaTime);
        } else {
          animator.SetBool("isMoving", false);

          // no need to move, already here!
          // now what?
        }
        break;
      // default:
    }

    // update text mesh
  

  }

}
