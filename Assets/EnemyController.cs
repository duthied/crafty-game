using System.Collections;
using System.Collections.Generic;
using DD;
using UnityEngine;


public class EnemyController : MonoBehaviour, IDamageable
{
  
  private float _health;
  private bool _targettable;

  public float movementSpeed;
  private float escapeSpeed = 0.2F;
  private float chaseSpeed = 0.4F;

  public float minDistance;
  private float escapeDistance = 0.5F;
  // private float chaseDistance = 0.1F;

  public float Health {
    set {

      Debug.Log("value: " + value + " _health: " + _health);

      _health = value;
      if(_health <= 0){
        animator.SetTrigger("death");
      }
    
    }
    get {
      return _health;
    }
  }

  public bool targetable { 
    set {
      _targettable = value;
      rb.simulated = value;
    }
    get {
      return _targettable;
    }
  }

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
    _health = 10;

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

  public void TakeDamage(float damage, Vector2 knockback) {
    Health -= damage;

    Debug.Log("TakeDamage: " + damage);

    // do the knockback thing
    // rb.AddForce(knockback);
    // rb.MovePosition(rb.position + knockback * 1f * Time.fixedDeltaTime);
    rb.transform.position = Vector2.MoveTowards(rb.transform.position, 
                                                  knockback, 
                                                  escapeSpeed * Time.deltaTime);

    // rb.(knockback);

    // randomly change attitude
    // int i = Random.Range(0, attitudes.Length);
    // attitude = attitudes[i];
    // label.text = attitude + " " + _health;
  }
  public void TakeDamage(float damage) {
    Health -= damage;

    // randomly change attitude
    // int i = Random.Range(0, attitudes.Length);
    // attitude = attitudes[i];
    // label.text = attitude + " " + _health;
  }

  public void Death() {
    Destroy(gameObject);
  }

  public void onHit(float damageSent, Vector2 knockback) {
    animator.SetTrigger("hit");
    TakeDamage(damageSent, knockback);
  }

  public void onHit(float damageSent) {
    animator.SetTrigger("hit");
    TakeDamage(damageSent);
  }

}
