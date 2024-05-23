using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Block : MonoBehaviour
{
    // Block Data(Color)
    public BlockDataSO blockData;
    //if the block has reached the final tile
    [HideInInspector]public bool isFinalized=false;
    MeshRenderer meshRenderer;
    [HideInInspector]public ColorType color;
    NavMeshAgent navMeshAgent;
    void Start()
    {
        navMeshAgent=GetComponent<NavMeshAgent>();
        meshRenderer=GetComponent<MeshRenderer>();
        color=blockData.color;
        switch (blockData.color)
        {
            case ColorType.Red: meshRenderer.material.color=Color.red; break;
            case ColorType.Green: meshRenderer.material.color=Color.green; break;
            case ColorType.Yellow: meshRenderer.material.color=Color.yellow; break;
            case ColorType.Blue: meshRenderer.material.color=Color.blue; break;

        }
    }
    void OnMouseDown()
    {
        if(!isFinalized){
        FinalTileManager.Instance.AddBlock(this);
        }
    }
    public void MoveToPosition(FinalTile finalTile)
    {
       
        Vector3 location=finalTile.gameObject.transform.position;
        finalTile.blockOnTop=this;
        navMeshAgent.SetDestination(location);
    }

    // Update is called once per frame
   
}
