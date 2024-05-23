using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class FinalTileManager : Singleton<FinalTileManager>
{
    // Start is called before the first frame update
    [SerializeField]GameObject tilePrefab;
    [SerializeField]Transform finalTileParent;
    [SerializeField]int noOfTiles=7;
    [SerializeField]float tileGap=1.5f;
    [SerializeField]Transform startTile;
    List<FinalTile> finalTiles;
    List<Block> blocks;
    void Start()
    {
        finalTiles=new List<FinalTile>();
        blocks=new List<Block>();
        Vector3 startPos=startTile.position;
        for(int i=0;i<noOfTiles;i++)
        {
            GameObject go=Instantiate(tilePrefab, startPos+new Vector3(i*tileGap,0,0), Quaternion.identity);
            go.transform.SetParent(finalTileParent);
            finalTiles.Add(go.GetComponent<FinalTile>());
        }
    }

    // Adding a block to the final list
   public async void AddBlock(Block block)
   {
    //Checking if there's a block in front
    RaycastHit hit;
    bool didHit= Physics.Raycast(block.transform.position,block.transform.forward,out hit);
     if(didHit && hit.transform.gameObject.CompareTag("Block") &&
     !hit.transform.gameObject.GetComponent<Block>().isFinalized)return;
     //lose condition if total number of blocks > final tiles and final tiles are filled
    if(FindObjectsOfType<Block>().Length>finalTiles.Count && finalTiles.Count==blocks.Count)
    {
        LoseCondition();
         return;
    }
    
    ColorType color=block.color;
    for(int i=0;i<finalTiles.Count;i++)
    {
        
        if(finalTiles[i].blockOnTop!=null && finalTiles[i].blockOnTop.color==color){
            ShiftBlocks(block,i);
            // if(finalTiles[i+1].isTileOccupied){
            // finalTiles[i+1].blockOnTop.MoveToPosition(finalTiles[i+2]);
            // finalTiles[i+1].blockOnTop=null;
            // }
            // else
            // block.MoveToPosition(finalTiles[i]);
            await Task.Delay(500);
            
            
    break;
    }}
    for(int i=0;i<finalTiles.Count;i++)
    {
        if(finalTiles[i].isTileOccupied==false)
        {
        blocks.Add(block);
        block.MoveToPosition(finalTiles[i]);
        block.isFinalized=true;
        break;
    }
   }
   //CheckBlockElimination();
   CheckWinCondition();
   return;
}
void ShiftBlocks(Block block, int index)
{
    //Shifting blocks one unit to the left when a block of the same color enters
    if(finalTiles[finalTiles.Count-1].isTileOccupied)return;
    for(int i=finalTiles.Count-2;i>=index;i--)
    {
        if(finalTiles[i].isTileOccupied && finalTiles[i].blockOnTop.color!=block.color)
        {
            finalTiles[i].blockOnTop.MoveToPosition(finalTiles[i+1]);
            finalTiles[i].blockOnTop=null;
        }
    }
}
public void CheckBlockElimination()
{
    //if three blocks of same color in a row, eliminate
    for(int i=0;i<finalTiles.Count-2;i++)
    {
        if(finalTiles[i+2].isTileOccupied && finalTiles[i].blockOnTop.color==finalTiles[i+1].blockOnTop.color && 
        finalTiles[i+1].blockOnTop.color==finalTiles[i+2].blockOnTop.color)
        {
            Destroy(finalTiles[i].blockOnTop.gameObject);
            blocks.Remove(finalTiles[i].blockOnTop);
            Destroy(finalTiles[i+1].blockOnTop.gameObject);
            blocks.Remove(finalTiles[i+1].blockOnTop);
            Destroy(finalTiles[i+2].blockOnTop.gameObject);
            blocks.Remove(finalTiles[i+2].blockOnTop);
            GameManager.Instance.Score+=15;
            ResetExistingBlocks(i+2);
            break;
        }
    }
}
void ResetExistingBlocks(int index)
{
    //move blocks back when a row behind them is eliminated
    for(int i=index+1;i<finalTiles.Count;i++)
    {
        finalTiles[i].blockOnTop.MoveToPosition(finalTiles[i-3]);
        finalTiles[i].blockOnTop=null;
    }
}
void FixedUpdate()
{
    CheckWinCondition();
}
void CheckWinCondition()
{
if(FindObjectsOfType<Block>().Length==0)
    {
        GameManager.Instance.Win();
        return;
    }
}
void LoseCondition()
{
    GameManager.Instance.Lose();
}
}
