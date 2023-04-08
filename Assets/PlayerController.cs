using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour{
  public float moveSpeed = 1f;
  public float collisionOffset = 0.05f;
  public ContactFilter2D movementFilter;
  public SwordAttack swordAttack;

  Vector2 movementInput;
  Rigidbody2D rigidBody;
  Animator animator;
  SpriteRenderer spriteRenderer;
  List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

  // Start is called before the first frame update
  void Start() {
    rigidBody = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void FixedUpdate() {
    // if movement input is not 0, try not to move
    if (movementInput != Vector2.zero) {
      bool success = TryMove(movementInput);

      if (!success & movementInput.x > 0) {
        // try to slide x
        success = TryMove(new Vector2(movementInput.x, 0));

        if (!success & movementInput.y > 0) {
          // try to slide y
          success = TryMove(new Vector2(0, movementInput.y));
        }
      }
      animator.SetBool("isMoving", success);
    } else {
      animator.SetBool("isMoving", false);
    }

    // set direction of the sprint to the direction of travel
    if (movementInput.x < 0) {
      spriteRenderer.flipX = true;
    } else if (movementInput.x > 0) {
      spriteRenderer.flipX = false;
    }
  }

  private bool TryMove(Vector2 direction) {
    if (direction != Vector2.zero) {
      int count = rigidBody.Cast(movementInput,
                     movementFilter,
                     castCollisions,
                     moveSpeed * Time.fixedDeltaTime + collisionOffset);
      if (count == 0) {
        rigidBody.MovePosition(rigidBody.position + movementInput * moveSpeed * Time.fixedDeltaTime);
        return true;
      } else {
        return false;
      }
    } else {
      return false;
    }
  }

  void OnMove(InputValue movementValue) => movementInput = movementValue.Get<Vector2>();
  void OnFire() {
    // for the animation to transition to weapon affect
    animator.SetTrigger("swordAttack");
  }

  // rename this to not be specific to a weapon
  public void Attack() {

    print("Player::Attack");

    if (spriteRenderer.flipX == true) {
      swordAttack.AttackLeft();
    } else {
      swordAttack.AttackRight();
    }
  }

  public void EndAttack(){
    print("EndAttack");

    swordAttack.StopAttack();
  }

}
