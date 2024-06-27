using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
    public GameObject owner;
    [SerializeField] LayerMask killMask;
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((killMask.value & (1 << other.gameObject.layer)) != 0){
            if(owner != null && owner == other.gameObject){ // TODO: this is buggy with moving lazers. fix it
                // Debug.Log($"{gameObject.name} -> {owner.name}");
                return;
            }
            other.gameObject.SetActive(false);
        }
    }
}
