using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTileManager : MonoBehaviour
{
    // Simple Cube-Grid Like Spawning, which will be 
    //overriden by the environment assets to create unique patterns of the grid
    public int something;
    [SerializeField]int gridCubeSize=5;
     [SerializeField]Vector3 offset;
    [SerializeField]float tileGap=1f;
     [SerializeField]Transform parentComponent;
    [SerializeField]GameObject tilePrefab;
    List<SpawnTile> spawnTiles;
    void Awake()
    {
        spawnTiles=new List<SpawnTile>();
        for(int i=0;i<gridCubeSize;i++)
        {
            for(int j=0;j<gridCubeSize;j++)
            {
                Vector3 spawnPos=new Vector3(i*tileGap,0,j*tileGap)+offset;
                GameObject go=Instantiate(tilePrefab,spawnPos,Quaternion.identity);
                go.transform.SetParent(parentComponent);
                spawnTiles.Add(go.GetComponent<SpawnTile>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
