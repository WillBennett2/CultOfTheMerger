using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct PawnDefinitions
{
    
    public enum MPawnObjects {Empty,Minions,Building,Item}
    public enum MMinionType {Empty,Zombie,Skeleton}
    public enum MManaType {Empty,Necro,Life,Night}
    public enum MBuildingType {Empty,Grave,Life,Night,General }
    public enum MItemType {Empty,Coin,Potion,Food }

    public MPawnObjects m_pawnObjects;
    public MMinionType m_minionType;
    public MManaType m_manaType;
    public MBuildingType m_buildingType;
    public MItemType m_itemType;
    public PawnLevels m_pawnLevels;
    public int m_currentLevel;

    public PawnDefinitions(MPawnObjects pawnObjects,MMinionType minionType,MManaType manaType,MBuildingType buildingType,MItemType itemType,PawnLevels pawnLevels ,int currentLevel)
    {
        this.m_pawnObjects = pawnObjects;
        this.m_minionType = minionType;
        this.m_manaType = manaType;
        this.m_buildingType = buildingType;
        this.m_itemType = itemType;
        
        this.m_pawnLevels = pawnLevels;
        this.m_currentLevel = currentLevel;
    }
}
