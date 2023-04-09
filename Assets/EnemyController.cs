using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
  
  public float health;

  public float movementSpeed;
  private float escapeSpeed = 0.6F;
  private float chaseSpeed = 0.4F;

  public float minDistance;
  private float escapeDistance = 0.5F;
  // private float chaseDistance = 0.1F;

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
  private string[] attitudes = {
    "idle",
    "escape",
    "persue"
  };

  GameObject Player;
  TextMesh label;

  void Start() {
    Player = GameObject.FindGameObjectWithTag("Player");
    label = GetComponentInChildren<TextMesh>();

    // defaults
    movementSpeed = 0.3F;
    minDistance = 0.3F;
    health = 10;

    // what is my mood?
    int i = Random.Range(0, attitudes.Length);
    attitude = attitudes[i];
    label.text = attitude + " " + health;
  }

  private void Update() {

    switch (attitude) {
      case "idle":
        break;

      case "persue":
        // do we need to move?
        if (Vector2.Distance(transform.position, Player.transform.position) > minDistance) {
          // move
          transform.position = Vector2.MoveTowards(transform.position, 
                                                    Player.transform.position, 
                                                    chaseSpeed * Time.deltaTime);
        } else {
          // no need to move, already here!
          // now what?
        }
        break;

      case "escape":
        // do we need to move?
        // speed up
        if (Vector2.Distance(transform.position, Player.transform.position) < escapeDistance) {
          // move
          transform.position = Vector2.MoveTowards(transform.position, 
                                                    Player.transform.position, 
                                                    -escapeSpeed * Time.deltaTime);
        } else {
          // no need to move, already here!
          // now what?
        }
        break;
      // default:
    }

    // update text mesh
  

  }

  public void TakeDamage(float damage) {
    Health -= damage;

    // randomly change attitude
    int i = Random.Range(0, attitudes.Length);
    attitude = attitudes[i];
    label.text = attitude + " " + health;
  }

  public void Death() {
    print("He killed me Mal, a guy...with a sword!");
    Destroy(gameObject);
  }

}
