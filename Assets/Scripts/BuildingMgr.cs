using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMgr : MonoBehaviour
{
    public enum BuildingMgrState { IDLE, INPICKING, INBUILD}
    public BuildingMgrState state;

    [SerializeField] List<BuildingA> buildingPrefabs;
    [SerializeField] LandGrid landGrid;

    private BuildingA buildingInPreparation;

    private List<BuildingA> buildingList;
    

    private void Start()
    {
        buildingList = new List<BuildingA>();
        state = BuildingMgrState.IDLE;
    }

    public void pickBuilding(int buildingPrefabIdx)
    {
        Vector2 recomendatePos = landGrid.GetRecomendateEmptyAreaPos();
        Vector3 pos = new Vector3(recomendatePos.x, 0, recomendatePos.y);
        buildingInPreparation = Instantiate(buildingPrefabs[buildingPrefabIdx], pos, Quaternion.identity);
        buildingInPreparation.SetToPick();
        state = BuildingMgrState.INPICKING;
    }

    public void Build()
    {
        if (buildingInPreparation != null)
        {
            buildingInPreparation.StartBuild(new Vector2(0,0));
            landGrid.SetCellTaken(new Vector2(buildingInPreparation.transform.position.x, buildingInPreparation.transform.position.z));
            buildingList.Add(buildingInPreparation);

            state = BuildingMgrState.INBUILD;
            //buildingInPreparation = null;
        }
    }

    public void SkipBuildingProgress()
    {
        if(state == BuildingMgrState.INBUILD)
        {
            foreach (BuildingA it in buildingList)
            {
                if (it.buildingState == Building.AnimState.INBUILD)
                {
                    it.SetDone();
                }
            }
        }
    }

    public bool IsAlowedToBuild(BuildingA building)
    {
        if(building.buildingIdsRequirement.Count > 0)
        {
            int counter = 0;
            foreach (string stringId in building.buildingIdsRequirement)
            {
                foreach (BuildingA it in buildingList)
                {
                    if(it.buildingId == stringId)
                    {
                        counter++;
                        break;
                    }

                }
            }
            if(counter < building.buildingIdsRequirement.Count)
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
    {
        switch (state)
        {
            case BuildingMgrState.IDLE:
                break;
            case BuildingMgrState.INPICKING:
                break;
            case BuildingMgrState.INBUILD:
                bool inBuild = false;
                foreach(BuildingA it in buildingList)
                {
                    if(it.buildingState == Building.AnimState.INBUILD)
                    {
                        inBuild = true;
                    }
                }
                if (!inBuild)
                {
                    state = BuildingMgrState.IDLE;
                }
                break;
        }
    }
}
