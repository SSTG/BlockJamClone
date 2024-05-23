using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [HideInInspector]public List<Block> redBlocks;
     [HideInInspector] public List<Block> yellowBlocks;
     [HideInInspector]public List<Block> blueBlocks;
      [HideInInspector] public List<Block> greenBlocks;
    [Header("UI Elements")]
    [SerializeField]TMP_Text scoreText;
     [SerializeField]GameObject resultScreen;
      [SerializeField]TMP_Text resultText;
      public BlockDataSO[] blockDatas;
      
    int score=0;
    public int Score{get{ return score;}set{score = value;}}
    
    
    void Start()
    {
        Time.timeScale=1;
        
       RemoveExtraBlocks(GroupAllColorLists());
    }

    public void AddColorToList(Block block, ColorType color)
    {
        switch(color)
        {
            case ColorType.Red: redBlocks.Add(block); break;
            case ColorType.Yellow: yellowBlocks.Add(block); break;
            case ColorType.Green: greenBlocks.Add(block); break;
            case ColorType.Blue: blueBlocks.Add(block); break;
        }
    }
    public List<List<Block>> GroupAllColorLists()
    {
        List<List<Block>> allColorBlocksList=new List<List<Block>>();
        allColorBlocksList.Add(redBlocks);
        allColorBlocksList.Add(yellowBlocks);
        allColorBlocksList.Add(blueBlocks);
        allColorBlocksList.Add(greenBlocks);
        return allColorBlocksList;
    }
    public void RemoveExtraBlocks(List<List<Block>> listCollection)
    {
        foreach(List<Block> list in listCollection)
        {
           int diff=list.Count%3;
           for(int i=0; i<diff; i++)
           {
            //list.Remove(list[i]);
            Destroy(list[i].gameObject);
           }
        }
    }
    // Update is called once per frame
   private void FixedUpdate() {
    scoreText.text=score.ToString();
   }
   public void Win()
   {
    resultScreen.SetActive(true);
    Time.timeScale=0;
    resultText.text="YOU WIN. TAP TO RESTART!";
   }
   public void Restart()
   {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
   public void Lose()
   {
    Time.timeScale=0;
    resultScreen.SetActive(true);
    resultText.text="You lose :-(. Try Again";
   }
}
