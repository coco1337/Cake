using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MapRotateController : MonoBehaviour
{
	[SerializeField] private Transform mMapTrans;
	[SerializeField] private Transform mPlayerTrans;
    [SerializeField] private float mDuration = 0.5f;

    Coroutine mRotateCoroutine;
    WaitForFixedUpdate mWaitUpdate = new WaitForFixedUpdate();

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
        int rotateCode = 0;

        if (Input.GetKeyDown(KeyCode.A))
        {
            rotateCode = -1;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            rotateCode = 1;
        }

        if (mRotateCoroutine == null && rotateCode != 0)
            mRotateCoroutine = StartCoroutine(CRotateMap(rotateCode));
    }

    IEnumerator CRotateMap(int rotateCode)
    {
        Vector3 axis = rotateCode > 0 ? Vector3.up : Vector3.down;
        bool isRotating = true;
        float time = 0;

        while (isRotating)
        {
            if (time < mDuration)
            {
                time += Time.deltaTime;
                mMapTrans.RotateAround(mPlayerTrans.position, axis, (90f * Time.deltaTime / mDuration));
            }
            else // time과 duration 값의 오차에 비례하게 카메라를 한번 더 회전시켜서 오차를 최소화
            {
                mMapTrans.RotateAround(mPlayerTrans.position, axis, (90f * (mDuration - time) / mDuration));
                isRotating = false;
            }
            yield return mWaitUpdate;
        }
        mRotateCoroutine = null;
    }
}