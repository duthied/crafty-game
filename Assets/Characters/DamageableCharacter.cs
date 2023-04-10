using System.Collections;
using System.Collections.Generic;
using DD;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable {

  Animator animator;
  Rigidbody2D rb;
  GameObject Player;
  TextMesh label;
  private float _health;
  private bool _targettable;

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

  public bool Targetable { 
    set {
      _targettable = value;
      rb.simulated = value;
    }
    get {
      return _targettable;
    }
  }

  void Start() {
    Player = GameObject.FindGameObjectWithTag("Player");
    label = GetComponentInChildren<TextMesh>();
    animator = GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();

    // defaults
    _health = 10;
  }

  private void Update() {
  }

  public void TakeDamage(float damage, Vector2 knockback) {
    Health -= damage;
    Debug.Log("TakeDamage: " + damage);
  }
  public void TakeDamage(float damage) {
    Health -= damage;
  }

  public void Death() {
    Destroy(gameObject);
  }

  public void onHit(float damageSent, Vector2 knockback) {
    animator.SetTrigger("hit");

    // do the knockback thing

    TakeDamage(damageSent, knockback);
  }

  public void onHit(float damageSent) {
    animator.SetTrigger("hit");
    TakeDamage(damageSent);
  }

}
