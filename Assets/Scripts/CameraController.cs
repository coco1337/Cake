using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform mPlayerTrans;
    [SerializeField] Vector3 Offset = new Vector3(0, 0, 0);

    void LateUpdate()
    {
        transform.position = mPlayerTrans.position + Offset;
    }
}
