using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD;

public class SwordAttack : MonoBehaviour {

  public Collider2D swordCollider;

  public float damage = 3f;
  public float knockbackForce = 500f;

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

  private void OnTriggerEnter2D(Collider2D collider) {

    IDamageable damageableObject = collider.GetComponent<IDamageable>();
    if (damageableObject != null) {
      if(collider.tag == "Enemy") {
      // handle
      EnemyController enemy = collider.GetComponent<EnemyController>();
      if (enemy != null) {

        // calculation direction and force
        Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
        Vector2 direction = (Vector2) (parentPosition + collider.gameObject.transform.position).normalized;
        Vector2 knockback = direction * knockbackForce;

        // Debug.Log("direction: " + direction + " knockback: "+ knockback);

        damageableObject.onHit(damage, knockback);
      }
    }
    } else {
      // isn't damageable
      Debug.Log(name + " doesn't have IDamageable");
    }
  }
}
