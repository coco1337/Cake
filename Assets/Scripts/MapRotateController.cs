using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public sealed class MapRotateController : MonoBehaviour
{
	[SerializeField] private Transform mMapTrans;
	[SerializeField] private Transform mPlayerTrans;
    [SerializeField] private float mDuration = 0.5f;
    [SerializeField] private Transform pivot;

    private bool inRotateCoroutine = false;
    private int rotateCount = 0;

    private void Update()
    {
        if (inRotateCoroutine)
            return;
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            inRotateCoroutine = true;
            StartCoroutine(CRotateMap(-1));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            inRotateCoroutine = true;
            StartCoroutine(CRotateMap(1));
        }
    }

    private IEnumerator CRotateMap(int rotateCode)
    {
        rotateCount += rotateCode;
        var yRot = pivot.rotation.eulerAngles.y;
        float time = 0;

        while (true)
        {
            if (time < mDuration)
            {
                time += Time.fixedDeltaTime;
                mMapTrans.SetParent(pivot);
                pivot.Rotate(pivot.up * rotateCode, (90f * Time.deltaTime / mDuration));
            }
            else
            {
                pivot.rotation = Quaternion.Euler(new Vector3(0, 90 * rotateCount, 0));
                mMapTrans.SetParent(null);
                break;
            }
            yield return new WaitForFixedUpdate();
        }

        inRotateCoroutine = false;
    }
}