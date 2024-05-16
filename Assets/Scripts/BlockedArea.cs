using UnityEngine;

public class BlockedArea : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Key key = other.GetComponent<Key>();
            if (key != null && key.IsPickedUp())
            {
                gameObject.SetActive(false);
            }
        }
    }
}
