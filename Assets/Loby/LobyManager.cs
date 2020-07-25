using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobyManager : MonoBehaviour
{
    public Button[] btn;
    public GameObject[] panel;
    int btnIdx;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < btn.Length; i++)
        {
            btn[i].interactable = false;
            panel[i].SetActive(false);
        }
        btn[0].interactable = true;
        panel[0].SetActive(true);

        btnIdx = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (btnIdx - 1 >= 0) btnIdx--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (btnIdx + 1 < btn.Length) btnIdx++;
        }
        for (int i = 0; i < btn.Length; i++)
        {
            if (i == btnIdx) continue;
            btn[i].interactable = false;
            panel[i].SetActive(false);
        }
        btn[btnIdx].interactable = true;
        panel[btnIdx].SetActive(true);
    }
}
