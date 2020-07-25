﻿using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public sealed class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController mCharacterController;
    [SerializeField] private Animator mAnimator;
    [SerializeField] private Transform mModelTrans;

    [SerializeField] private float mMoveSpeed;
    [SerializeField] private float mJumpPower;

    [SerializeField] private float mSugarMoveSpeed;
    [SerializeField] private float mSugarJumpPower;

    public bool isSugarHighMode;

    public GaugeController GC;

    const float mGravity = 9.8f;
    Vector3 mMoveDir = Vector3.zero;
    bool isJumping;

    bool isDie = false;

    void Update()
    {
        if (mCharacterController.isGrounded)
        {
            float moveSpeed = isSugarHighMode ? mSugarMoveSpeed : mMoveSpeed;
            float jumpPower = isSugarHighMode ? mSugarJumpPower : mJumpPower;

            if (isJumping)
            {
                mAnimator.SetBool("Jump", false);
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                mMoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
                mMoveDir = transform.TransformDirection(mMoveDir);
                mMoveDir *= moveSpeed;

                mModelTrans.localRotation = mMoveDir.x < 0 ? 
                    Quaternion.Euler(0, 180f, 0) : Quaternion.Euler(0, 0, 0);
                mAnimator.SetBool("Walk", true);

                //
                if (!SoundManager.Inst.isPlay("walk"))
                    SoundManager.Inst.Play("walk",false,0.5f);

            }
            else // input 없음으로 인한 미끄러짐 방지
            {
                mMoveDir = Vector3.zero;
                mMoveDir = transform.TransformDirection(mMoveDir);
                mMoveDir *= moveSpeed;
                mAnimator.SetBool("Walk", false);
            }

            if (Input.GetButtonDown("Jump"))
            {
                mMoveDir.y = jumpPower;
                isJumping = true;
                mAnimator.SetBool("Jump", true);

                //
                SoundManager.Inst.Play("jump",false,0.7f);
            }
        }

        mMoveDir.y -= mGravity * Time.deltaTime;
        mCharacterController.Move(mMoveDir * Time.deltaTime);

        //
        if( this.transform.localPosition.y < -3f && !isDie)
        {
            isDie = true;
            SoundManager.Inst.Play("game_over");
        }
        else if( this.transform.localPosition.y > -1f && isDie)
        {
            isDie = false;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Debug.Log(hit.gameObject.name);
        if (hit.collider.CompareTag("Goal"))
        {
            GameManager.instance.ClearStage();
            var animator = hit.transform.GetComponent<Animator>();
            animator.SetBool("Goal", true);

            //
            SoundManager.Inst.Play("full_Strawberry");
        }
        else
        {
            var cubeBreak = hit.collider.gameObject.GetComponent<CubeBreak>();
            if (cubeBreak != null && GC.GetStrawberryCount >= 2)
            {
                cubeBreak.BreakOk(this.gameObject);
            }

        }
    }
}