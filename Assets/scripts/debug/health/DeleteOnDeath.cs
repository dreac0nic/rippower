using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnDeath : MonoBehaviour
{
  public float DestroyDelay = 1.0f;
  
  public void OnDeath() {
    Destroy(this.gameObject, DestroyDelay);
  }
}
