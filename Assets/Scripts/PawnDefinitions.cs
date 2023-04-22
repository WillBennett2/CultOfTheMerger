using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct PawnDefinitions
{ 
    public enum MPawnObjects {Empty,Minions,Building,Item,Enemy,Reward}
    public enum MMinionType {Empty,Zombie,Skeleton}
    public enum MManaType {Empty,Necro,Life,Night}
    public enum MBuildingType {Empty,Grave,Life,Night,General }
    public enum MItemType {Empty,Coin,Potion,Food }
    public enum MSacrificeTypes
    {
        Empty,CultValue,Mana1,Mana2,Mana3,Coin,Gem,Rune1,Rune2,Rune3,Rune4
    }
    public enum MEnemyTypes
    {
        Empty,Enemy1,Enemy2,Enemy3
    }
    public enum MRewardType
    {
        Empty,Chest1,Chest2,Chest3
    }


    public MPawnObjects m_pawnObjects;
    public MMinionType m_minionType;
    public MManaType m_manaType;
    public MBuildingType m_buildingType;
    public MItemType m_itemType;
    public MSacrificeTypes m_sacrificeTypes;
    public MEnemyTypes m_enemyTypes;
    public MRewardType m_rewardTypes;
    public PawnLevels m_pawnLevels;
    public int m_currentLevel;

    public PawnDefinitions(MPawnObjects pawnObjects,MMinionType minionType,MManaType manaType,
        MBuildingType buildingType,MItemType itemType,MSacrificeTypes sacrificeTypes, MEnemyTypes enemyTypes, MRewardType rewardType,
        PawnLevels pawnLevels ,int currentLevel)
    {
        this.m_pawnObjects = pawnObjects;
        this.m_minionType = minionType;
        this.m_manaType = manaType;
        this.m_buildingType = buildingType;
        this.m_itemType = itemType;
        this.m_sacrificeTypes = sacrificeTypes;
        this.m_enemyTypes = enemyTypes;
        this.m_rewardTypes = rewardType;
        this.m_pawnLevels = pawnLevels;
        this.m_currentLevel = currentLevel;
    }
}
