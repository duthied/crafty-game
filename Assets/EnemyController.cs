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

    float dist_ = Vector3.Distance(Player.transform.position, transform.position); //find distance
    
    if (Vector2.Distance(transform.position, Player.transform.position) > minDistance) {

      // print("Moving!");
      transform.position = Vector2.MoveTowards(transform.position, 
                                                Player.transform.position, 
                                                movementSpeed * Time.deltaTime);
    } else {
      // do something?
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
