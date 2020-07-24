using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public sealed class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController mCharacterController;
    [SerializeField] private float mMoveSpeed;
    [SerializeField] private float mJumpPower;

    [SerializeField] private float mSugarMoveSpeed;
    [SerializeField] private float mSugarJumpPower;

    public bool isSugarHighMode;

    const float mGravity = 9.8f;
    Vector3 mMoveDir = Vector3.zero;

    void Update()
    {
        if (mCharacterController.isGrounded)
        {
            float moveSpeed = isSugarHighMode ? mSugarMoveSpeed : mMoveSpeed;
            float jumpPower = isSugarHighMode ? mSugarJumpPower: mJumpPower;

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                mMoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
                mMoveDir = transform.TransformDirection(mMoveDir);
                mMoveDir *= moveSpeed;
            }
            else // input 없음으로 인한 미끄러짐 방지
            {
                mMoveDir = Vector3.zero;
                mMoveDir = transform.TransformDirection(mMoveDir);
                mMoveDir *= moveSpeed;
            }

            if (Input.GetButtonDown("Jump"))
                mMoveDir.y = jumpPower;

        }

        mMoveDir.y -= mGravity * Time.deltaTime;
        mCharacterController.Move(mMoveDir * Time.deltaTime);
    }
}