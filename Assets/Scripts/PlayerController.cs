using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public sealed class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController mCharacterController;
    [SerializeField] private float mMoveSpeed;
    [SerializeField] private float mJumpPower;

    const float mGravity = 9.8f;
    Vector3 mMoveDir = Vector3.zero;

    void Update()
    {
        if (mCharacterController.isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                mMoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
                mMoveDir = transform.TransformDirection(mMoveDir);
                mMoveDir *= mMoveSpeed;
            }
            else // input 없음으로 인한 미끄러짐 방지
            {
                mMoveDir = Vector3.zero;
                mMoveDir = transform.TransformDirection(mMoveDir);
                mMoveDir *= mMoveSpeed;
            }

            if (Input.GetButton("Jump"))
                mMoveDir.y = mJumpPower;

        }

        mMoveDir.y -= mGravity * Time.deltaTime;
        mCharacterController.Move(mMoveDir * Time.deltaTime);
    }
}