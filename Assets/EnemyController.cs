using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
  
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

  public float health = 1;

  public void TakeDamage(float damage) {
    Health -= damage;
  }

  public void Death() {
    print("He killed me Mal, a guy...with a sword!");
    Destroy(gameObject);
  }
}
