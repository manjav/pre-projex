using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public int order = 1;
    public int rowsCount = 3;
    public int columnsCount = 4;
    public List<int> turnesThresholds;


    public void CopyFrom(LevelData levelData)
    {
        this.order = levelData.order;
        this.rowsCount = levelData.rowsCount;
        this.columnsCount = levelData.columnsCount;
        this.turnesThresholds = levelData.turnesThresholds;
    }
}