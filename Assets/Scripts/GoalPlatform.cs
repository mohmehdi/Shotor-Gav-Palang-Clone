using UnityEngine;

public class GoalPlatform : MonoBehaviour
{
    [Tooltip("Each area can have its own ID and detects the Box with the same ID")]
    [SerializeField] int ID = 0;

    public bool check = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<GoalBox>(out var box))
        {
            if (ID == box.ID)
            {
                check = true;
                GameManager.Instance.CheckObjective();
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<GoalBox>(out var box))
        {
            if (ID == box.ID)
            {
                if (other.TryGetComponent<Rigidbody2D>(out var body))
                {
                    if (body.bodyType == RigidbodyType2D.Static)
                        return;
                }
                check = false;
                GameManager.Instance.CheckObjective();
            }
        }
    }
}
