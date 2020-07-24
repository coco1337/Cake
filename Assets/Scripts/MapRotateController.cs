using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MapRotateController : MonoBehaviour
{
	[SerializeField] private Transform mMapTrans;
	[SerializeField] private Transform mPlayerTrans;
    [SerializeField] private float mRotateSpeed;

    Coroutine mRotateCoroutine;
    WaitForEndOfFrame mWaitFrame = new WaitForEndOfFrame();

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
        float totalRotate = 0;
        Vector3 axis = rotateCode > 0 ? Vector3.up : Vector3.down;
        Vector3 rot = mMapTrans.rotation.eulerAngles;

        while (totalRotate <= 90f)
        {
            totalRotate += mRotateSpeed;
            mMapTrans.RotateAround(mPlayerTrans.position, axis, mRotateSpeed);
            yield return mWaitFrame;
        }

        float newY = rot.y + (90f * axis.y);
        mMapTrans.rotation = Quaternion.Euler(new Vector3(0, newY, 0));
        mRotateCoroutine = null;
    }
}