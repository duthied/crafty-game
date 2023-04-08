using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour {

  public float damage = 3;

  public enum AttackDirection{
    left, right
  }

  public AttackDirection attackDirection;

  Collider2D swordCollider;
  Vector2 rightAttackOffset;

  // Start is called before the first frame update
  void Start() {
    swordCollider = GetComponent<Collider2D>();
    rightAttackOffset = transform.position;
  }

  public void AttackRight() {
    print("Attack right");
    swordCollider.enabled = true;
    transform.position = rightAttackOffset;
  }
  public void AttackLeft() {
    print("Attack left");
    swordCollider.enabled = true;
    transform.position = new Vector2(rightAttackOffset.x * -1, rightAttackOffset.y);
  }
  public void StopAttack() {
    swordCollider.enabled = false;
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if(other.tag == "Enemy") {
      // handle
      EnemyController enemy = other.GetComponent<EnemyController>();
      if (enemy != null) {

        print("damage!");

        enemy.TakeDamage(damage);
      }
    }
  }
}
