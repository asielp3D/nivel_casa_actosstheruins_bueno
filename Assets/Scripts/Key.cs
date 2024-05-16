using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject lockedArea;
    private bool isPickedUp = false;

    public bool IsPickedUp()
    {
        return isPickedUp;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp();
        }
    }

    public void PickUp()
    {
        if (!isPickedUp)
        {
            isPickedUp = true;
            Destroy(gameObject);
            UnlockArea();
        }
    }

    void UnlockArea()
    {
        if (lockedArea != null)
        {
            lockedArea.SetActive(false);
        }
    }
}