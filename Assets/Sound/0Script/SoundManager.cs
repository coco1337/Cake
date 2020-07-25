using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _inst = null;
    public static SoundManager Inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = FindObjectOfType(typeof(SoundManager)) as SoundManager;
            }
            return _inst;
        }
    }

    public AudioClip[] audioClips;
    public Dictionary<string, AudioSource> SoundDic;
    //
    void Awake()
    {

        SoundDic = new Dictionary<string, AudioSource>();

        int Len = audioClips.Length;

        for (int i = 0; i < Len; i++)
        {
            this.gameObject.AddComponent<AudioSource>();
        }

        AudioSource[] _as = this.GetComponentsInChildren<AudioSource>();

        for (int i = 0; i < Len; i++)
        {
            _as[i].clip = audioClips[i];
            _as[i].playOnAwake = false;
            SoundDic.Add(_as[i].clip.name, _as[i]);
        }
    }

    public void Play(string name, bool loop = false, float volume = 1)
    {
        if (SoundDic.ContainsKey(name))
        {
            AudioSource aS = SoundDic[name];
            aS.loop = loop;
            aS.volume = volume;
            aS.Play();
        }
    }

    public void PlayOneShot(string name)
    {
        if (SoundDic.ContainsKey(name))
        {
            AudioSource aS = SoundDic[name];
            aS.PlayOneShot(aS.clip);
        }
    }

    public void Stop(string name)
    {
        if (SoundDic.ContainsKey(name))
        {
            AudioSource aS = SoundDic[name];
            aS.Stop();
        }
    }

    public bool isPlay(string name)
    {
        if (SoundDic.ContainsKey(name))
        {
            AudioSource aS = SoundDic[name];
            return aS.isPlaying;
        }
        return false;
    }

    public void Mute(bool isMute)
    {
        AudioSource[] AS = SoundManager.Inst.GetComponentsInChildren<AudioSource>();
        for (int i = 0; i < AS.Length; i++)
        {
            AS[i].mute = isMute;
        }
    }
}

