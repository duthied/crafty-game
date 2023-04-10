using UnityEngine;

namespace DD {
  public interface IDamageable {
    public float Health { set; get; }
    public void onHit(float damageSent, Vector2 knockback);
    public void onHit(float damageSent);
    public bool targetable { set; get; }
    public void Death();

  }

}