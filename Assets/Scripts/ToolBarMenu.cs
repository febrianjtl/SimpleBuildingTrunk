using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBarMenu : MonoBehaviour
{
    [SerializeField] BuildingMgr buildingMgr;
    [SerializeField] Button building01_btn;
    [SerializeField] Button building02_btn;
    [SerializeField] Button build_btn;
    [SerializeField] Button skip_btn;



    [SerializeField] BuildingA building01;
    [SerializeField] BuildingA building02;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ForceUpdateUI();
    }

    public void ForceUpdateUI()
    {
        switch (buildingMgr.state)
        {
            case BuildingMgr.BuildingMgrState.IDLE:
                building01_btn.interactable = buildingMgr.IsAlowedToBuild(building01);
                    building02_btn.interactable = buildingMgr.IsAlowedToBuild(building02);
                build_btn.interactable = false;
                skip_btn.interactable = false;
                break;
            case BuildingMgr.BuildingMgrState.INPICKING:
                building01_btn.interactable = false;
                building02_btn.interactable = false;
                build_btn.interactable = true;
                skip_btn.interactable = false;
                break;
            case BuildingMgr.BuildingMgrState.INBUILD:
                building01_btn.interactable = false;
                building02_btn.interactable = false;
                build_btn.interactable = false;
                skip_btn.interactable = true;
                break;
        }
    }
}
