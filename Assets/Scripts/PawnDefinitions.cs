using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct PawnDefinitions
{ 
    public enum MPawnObjects {Empty,Minions,Building,Item,Enemy,Reward,Store}
    public enum MMinionType {Empty,Undead,Plant,Demon}
    public enum MManaType {Empty,Necro,Life,Hell}
    public enum MBuildingType {Empty,Grave,Life, Hell, General }
    public enum MItemType {Empty,Coin,Gem,DeathRune,LifeRune,HellRune,Potion,Food }
    public enum MSacrificeTypes
    {
        Empty,CultValue,Necro,Life,Hell,Coin,Gem,DeathRune,LifeRune,HellRune,Rune4
    }
    public enum MEnemyTypes
    {
        Empty,Undead,Life,Hell
    }
    public enum MRewardType
    {
        Empty,Money,DeathChest,LifeChest,HellChest
    }
    public enum MStoreType
    {
        Empty,NecroStore,LifeStore,HellStore
    }


    public MPawnObjects m_pawnObjects;
    public MMinionType m_minionType;
    public MManaType m_manaType;
    public MBuildingType m_buildingType;
    public MItemType m_itemType;
    public MSacrificeTypes m_sacrificeTypes;
    public MEnemyTypes m_enemyTypes;
    public MRewardType m_rewardTypes;
    public MStoreType m_storeTypes;
    public PawnLevels m_pawnLevels;
    public int m_currentLevel;

    public PawnDefinitions(MPawnObjects pawnObjects,MMinionType minionType,MManaType manaType,
        MBuildingType buildingType,MItemType itemType,MSacrificeTypes sacrificeTypes, MEnemyTypes enemyTypes, 
        MRewardType rewardType, MStoreType storeType,
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
        this.m_storeTypes = storeType;
        this.m_pawnLevels = pawnLevels;
        this.m_currentLevel = currentLevel;
    }
}
