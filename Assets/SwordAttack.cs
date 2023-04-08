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

  // Collider2D swordCollider;
  Vector2 rightAttackOffset;

  // Start is called before the first frame update
  void Start() {
    // swordCollider = GetComponent<Collider2D>();
    rightAttackOffset = transform.position;
  }

  public void AttackRight() {
    print("SwordAttack::Attack right");
    swordCollider.enabled = true;
    transform.localPosition = rightAttackOffset;
  }
  public void AttackLeft() {
    print("SwordAttack::Attack left");
    swordCollider.enabled = true;
    transform.localPosition = new Vector2(rightAttackOffset.x * -1, rightAttackOffset.y);
  }
  public void StopAttack() {
    print("  SwordAttack::StopAttack");

    swordCollider.enabled = false;
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if(other.tag == "Enemy") {
      // handle
      EnemyController enemy = other.GetComponent<EnemyController>();
      if (enemy != null) {

        print("SwordAttack::damage!");

        enemy.TakeDamage(damage);
      }
    }
  }
}
