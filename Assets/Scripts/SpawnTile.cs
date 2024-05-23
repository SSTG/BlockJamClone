using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]GameObject blockPrefab;
    Transform parentTransform;
    BlockDataSO[] blockDatas;
    bool isTileOccupied;
    void Awake()
    {
        blockDatas=GameManager.Instance.blockDatas;
        parentTransform=GameObject.Find("Blocks").transform;
        float f=Random.Range(0,5f);
        int randomIndex=Random.Range(0,blockDatas.Length);
        if(f>1f)
       {
        GameObject go=Instantiate(blockPrefab,transform.position,Quaternion.identity);
        go.GetComponent<Block>().blockData=blockDatas[randomIndex];
        GameManager.Instance.AddColorToList(go.GetComponent<Block>(),go.GetComponent<Block>().blockData.color);
        go.transform.SetParent(parentTransform);}
        }

    
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Block"))
        isTileOccupied=true;
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Block"))
        isTileOccupied=false;
    }
}
