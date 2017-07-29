using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RaycastDamageOnClick : MonoBehaviour
{
  public float Damage = 5.0f;
  public float Distance = 100.0f;
  public string ButtonName = "Fire1";
  
  private Camera m_Camera;
  
  public void Awake() {
    m_Camera = GetComponent<Camera>();
  }
  
  public void Update() {
    if(Input.GetButtonDown(ButtonName)) { // button pushed
      RaycastHit hit;
      Ray cast_ray = m_Camera.ScreenPointToRay(Input.mousePosition);

      if(Physics.Raycast(cast_ray, out hit, Distance)) {
	Living hit_living = hit.collider.GetComponentInParent<Living>();

	if(hit_living) {
	  hit_living.TakeDamage(Damage);
	}
      }
    }
  }
}
