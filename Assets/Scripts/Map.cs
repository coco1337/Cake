using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] DropChecker mDropChecker;
    [SerializeField] Transform mStartPosition;

    Player mPlayer;

    public void Init()
    {
        mPlayer = GameManager.instance.GetPlayer;
        mDropChecker.SetStartPosition(mStartPosition);
        mPlayer.transform.position = mStartPosition.position;
    }
}
