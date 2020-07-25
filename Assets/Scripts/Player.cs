using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerController mPlayerController;
    [SerializeField] Animator mEffectAnimator;
    [SerializeField] float mSugarHighTime;

    WaitForSeconds mWaitSugarTime;

    void Start()
    {
        mWaitSugarTime = new WaitForSeconds(mSugarHighTime);
    }

    public void StartSugarHighTime()
    {
        StartCoroutine(CSugarHighTime());
    }
    
    IEnumerator CSugarHighTime()
    {
        mPlayerController.isSugarHighMode = true;
        yield return mWaitSugarTime;
        mPlayerController.isSugarHighMode = false;
    }

    public void ShowEatEffect()
    {
        StartCoroutine(CEatEffect());
    }

    IEnumerator CEatEffect()
    {
        mEffectAnimator.SetBool("getStrawberry", true);
        yield return new WaitForSeconds(.3f);
        mEffectAnimator.SetBool("getStrawberry", false);
    }
}
