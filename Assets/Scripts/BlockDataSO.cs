using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName="BlockData" , menuName="BlockData")]
public class BlockDataSO : ScriptableObject
{
    public ColorType color;
    
}
public enum ColorType
{
    Red, Green, Yellow, Blue 
}