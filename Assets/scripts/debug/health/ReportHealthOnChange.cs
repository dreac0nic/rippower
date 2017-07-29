using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Living))]
public class ReportHealthOnChange : MonoBehaviour
{
  private Living m_Living;
  
  public void Awake() {
    m_Living = this.GetComponent<Living>();
  }

  public void OnHealthChanged(float health_delta) {
    Debug.Log(this.gameObject.name + "'s health changed by " + health_delta + " to " + m_Living.Health);
  }
}
