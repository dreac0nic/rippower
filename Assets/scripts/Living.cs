using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Living : MonoBehaviour
{
  [SerializeField] protected float m_MaximumHealth = 100.0f;
  [SerializeField] protected float m_Health = 100.0f;

  [Header("Health Behavior")]
  [SerializeField] protected bool m_AllowOverhealth = true;
  [SerializeField] protected float m_OverhealthDrainSpeed = 2.0f;
  
  public bool IsAlive { get { return m_Health > 0.0f; } }
  public float Health { get { return m_Health; } }
  public float HealthNormalized { get { return m_Health/m_MaximumHealth; } }

  public void Awake() {
    // Sets health properly on the first frame while avoiding a frame of drain
    if(!m_AllowOverhealth) {
      constrainHealth();
    }
  }
  
  public void Update() {
    constrainHealth();
  }
  
  public float TakeDamage(float damage) {
    float damage_taken = applyHealthDelta(-damage);
    this.BroadcastMessage("OnDamaged", damage_taken, SendMessageOptions.DontRequireReceiver);

    return damage_taken;
  }

  public float HealDamage(float gains) {
    float damage_healed = applyHealthDelta(gains);
    this.BroadcastMessage("OnHealed", damage_healed, SendMessageOptions.DontRequireReceiver);

    return damage_healed;
  }

  protected float applyHealthDelta(float delta) {
    float original_health = m_Health;
    float health_change = 0.0f;

    // Apply health
    m_Health += delta;
    constrainHealth();

    // Calculate change
    health_change = original_health - m_Health;
    this.BroadcastMessage("OnHealthChanged", health_change, SendMessageOptions.DontRequireReceiver);

    // Check to see if the living thing is not
    if(!this.IsAlive) {
      this.BroadcastMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
    }

    return health_change;
  }

  protected void constrainHealth() {
    if(m_Health > m_MaximumHealth) {
      float delta = 0.0f;
      float target = m_Health;

      // Calculate new health values based on characteristics
      if(m_AllowOverhealth && m_OverhealthDrainSpeed != 0.0f) {
	delta = m_OverhealthDrainSpeed*Time.deltaTime;
	target = m_Health - delta;
      } else if(!m_AllowOverhealth) {
	delta = m_Health - m_MaximumHealth;
	target = m_MaximumHealth;
      }

      // Apply correction
      m_Health = target;

      this.BroadcastMessage("OnOverhealthDrain", delta, SendMessageOptions.DontRequireReceiver);
    }
  }
}
