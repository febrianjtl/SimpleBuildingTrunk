using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public enum AnimState { IDLE, INPICK,INBUILD}
    [SerializeField] private Vector2 size;
    [SerializeField] private int buildTime;
    [SerializeField] private int maxItemCount;
    [SerializeField] private Animator animator;


    public string buildingId;
    public List<string> buildingIdsRequirement;

    private Vector2 posIndexInGrid;
    private int buildEndTime;
    public AnimState buildingState;
    public abstract void ExampleMethod();

    public void Update()
    {
        switch (buildingState)
        {
            case AnimState.IDLE:
                break;
            case AnimState.INPICK:
                break;
            case AnimState.INBUILD:
                if((int)Time.time > buildEndTime)
                {
                    SetDone();
                }
                
                break;
        }
    }

    public void StartBuild(Vector2 posInGrid)
    {
        buildEndTime = (int)Time.time + buildTime;
        posIndexInGrid = posInGrid;
        animator.SetInteger("State", (int)AnimState.INBUILD);
        buildingState = AnimState.INBUILD;
    }

    public void SetToPick()
    {
        animator.SetInteger("State", (int)AnimState.INPICK);
        buildingState = AnimState.INPICK;
    }

    public void SetDone()
    {
        if(buildingState == AnimState.INBUILD)
        {
            buildEndTime = (int)Time.time;
            buildingState = AnimState.IDLE;
            animator.SetInteger("State", (int)AnimState.IDLE);
        }
    }

}
