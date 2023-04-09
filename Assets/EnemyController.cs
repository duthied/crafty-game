using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
  
  public float health = 1;
  // public int attitude = (int) attitudes.idle;
  public float movementSpeed;
  // public Transform currentTarget;
  public float minDistance;

  // Transform player;

  public float Health {
    set {
      health = value;
      if(health <= 0){
        Death();
      }
    }
    get {
      return health;
    }
  }

  public string attitude = "idle";

  // enum attitudes {
  //   angry,
  //   scared,
  //   idle
  // }

  GameObject Player;

  void Start() {
    Player = GameObject.FindGameObjectWithTag("Player");

    // defaults
    movementSpeed = 0.2F;
    minDistance = 0.3F;
  }

  private void Update() {

    switch (attitude) {
      case "idle":
        break;

      case "angry":
        // do we need to move?
        if (Vector2.Distance(transform.position, Player.transform.position) > minDistance) {
          // move
          transform.position = Vector2.MoveTowards(transform.position, 
                                                    Player.transform.position, 
                                                    movementSpeed * Time.deltaTime);
        } else {
          // no need to move, already here!
          // now what?
        }
        break;

      case "scared":
        // do we need to move?
        if (Vector2.Distance(transform.position, Player.transform.position) < minDistance) {
          // move
          transform.position = Vector2.MoveTowards(transform.position, 
                                                    Player.transform.position, 
                                                    -movementSpeed * Time.deltaTime);
        } else {
          // no need to move, already here!
          // now what?
        }
        break;
      // default:
    }

  }

  public void TakeDamage(float damage) {
    Health -= damage;
  }

  public void Death() {
    print("He killed me Mal, a guy...with a sword!");
    Destroy(gameObject);
  }

}
