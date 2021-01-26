using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DO
{
    public class Interactable : MonoBehaviour
    {
        public float radius = 1f;
        public string interactableText; 

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public virtual void Interact(PlayerManager playerManager)
        {
            Debug.Log("You Interacted with an object");
        }
    }
}

