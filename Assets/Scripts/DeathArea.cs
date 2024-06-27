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
public class GoalPlatform : MonoBehaviour
{
    [Tooltip("Each area can have its own ID and detects the Box with the same ID")]
    [SerializeField] int ID = 0;
    public bool check =false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<GoalBox>(out var box))
        {
            if (ID == box.ID){
                check = true;
                GameManager.Instance.CheckObjective();
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<GoalBox>(out var box))
        {
            if (ID == box.ID){
                check = false;
                GameManager.Instance.CheckObjective();
            }
        }
    }
}
public class GoalBox : MonoBehaviour
{
    public int ID {get;private set;}
}
