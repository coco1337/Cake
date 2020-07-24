using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DropChecker : MonoBehaviour
{
    [SerializeField] Transform mStartPosition;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var cc = other.gameObject.GetComponent<CharacterController>();
            cc.enabled = false;
            other.gameObject.transform.position = mStartPosition.position;
            cc.enabled = true;
        }
    }
}
