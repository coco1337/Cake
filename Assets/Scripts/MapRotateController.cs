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

    // Coroutine mRotateCoroutine;
    // WaitForFixedUpdate mWaitUpdate = new WaitForFixedUpdate();

    private int rotateCode = 0;
    private bool inRotateCoroutine = false;
    private int rotateCount = 0;
    
    //void FixedUpdate()
    //{
    //       if (Input.GetKey(KeyCode.A))
    //       {
    //           mMapTrans.RotateAround(mPlayerTrans.position, Vector3.up, mRotateSpeed);
    //       }
    //       else if (Input.GetKey(KeyCode.D))
    //       {
    //           mMapTrans.RotateAround(mPlayerTrans.position, Vector3.down, mRotateSpeed);
    //       }
    //   }

    void Update()
    {
        rotateCode = 0;

        if (Input.GetKeyDown(KeyCode.A))
        {
            rotateCode = -1;
            inRotateCoroutine = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            rotateCode = 1;
            inRotateCoroutine = true;
        }

        if (inRotateCoroutine && rotateCode != 0)
            StartCoroutine(CRotateMap(rotateCode));
    }

    IEnumerator CRotateMap(int rotateCode)
    {
        rotateCount += rotateCode;
        inRotateCoroutine = true;
        var yRot = pivot.rotation.eulerAngles.y;
        float axis = rotateCode > 0 ? 1 : -1;
        float time = 0;

        while (true)
        {
            if (time < mDuration)
            {
                time += Time.fixedDeltaTime;
                mMapTrans.SetParent(pivot);
                pivot.Rotate(pivot.up * axis, (90f * Time.deltaTime / mDuration));
                // mMapTrans.RotateAround(mPlayerTrans.position, axis, (90f * Time.deltaTime / mDuration));
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