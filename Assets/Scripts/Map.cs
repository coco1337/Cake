using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] DropChecker mDropChecker;

    public Transform StartPosition;
    Player mPlayer;

    public void Init()
    {
        mPlayer = GameManager.instance.GetPlayer;
        
        if (StartPosition != null)
        {
            mDropChecker.SetStartPosition(StartPosition);
            mPlayer.transform.position = StartPosition.position;
        }
        else
        {
            Debug.LogError("시작 지점이 설정되지 않았습니다.");
        }
    }
}
