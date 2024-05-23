using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTile : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isTileOccupied=false;
    public Block blockOnTop;
    

    // Update is called once per frame
     private void FixedUpdate() {
    isTileOccupied=blockOnTop!=null;    
    }
   
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Block")){
        FinalTileManager.Instance.CheckBlockElimination();
        //Debug.Log("ELIMINATED");}
    }}}
    

