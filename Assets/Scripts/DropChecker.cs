using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DropChecker : MonoBehaviour
{
    [SerializeField] Vector3 mStartPosition;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Trigger on");

            var cc = other.gameObject.GetComponent<CharacterController>();
            cc.Move(Vector3.zero);
            other.gameObject.transform.position = mStartPosition;
        }
    }
}
