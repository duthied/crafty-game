using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour {

  public Collider2D swordCollider;

  public float damage = 3;

  public enum AttackDirection{
    left, right
  }

  public AttackDirection attackDirection;
  Vector2 rightAttackOffset;
  
  // Start is called before the first frame update
  void Start() {
    rightAttackOffset = transform.position;
  }

  public void AttackRight() {
    swordCollider.enabled = true;
    transform.localPosition = rightAttackOffset;
  }
  public void AttackLeft() {
    swordCollider.enabled = true;
    transform.localPosition = new Vector2(rightAttackOffset.x * -1, rightAttackOffset.y);
  }
  public void StopAttack() {
    swordCollider.enabled = false;
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if(other.tag == "Enemy") {
      // handle
      EnemyController enemy = other.GetComponent<EnemyController>();
      if (enemy != null) {
        enemy.TakeDamage(damage);
      }
    }
  }
}
