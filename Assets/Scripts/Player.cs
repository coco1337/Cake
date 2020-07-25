using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerController mPlayerController;
    [SerializeField] float mSugarHighTime;
    //[SerializeField] SugarModeEffect  // 아직 미정

    WaitForSeconds mWaitSugarTime;

    void Start()
    {
        mWaitSugarTime = new WaitForSeconds(mSugarHighTime);
    }

    public void StartSugarHighTime()
    {
        StartCoroutine(CSugarHighTime());
    }

    /// <summary>
    /// 딸기 조각을 모을 때마다 캐릭터 변화
    /// </summary>
    public void ChangeCharacterModel(int level)
    {

    }

    IEnumerator CSugarHighTime()
    {
        mPlayerController.isSugarHighMode = true;
        yield return mWaitSugarTime;
        mPlayerController.isSugarHighMode = false;
    }
}
