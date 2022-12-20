using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct MinionDefinions
{
    public enum MMinionType {Zombie,Skeleton}

    public enum MManaType {Necro,Life,Night}

    public MMinionType m_minionType;
    public MinionLevels m_minionLevels;
    public int m_currentLevel;

    public MinionDefinions(MMinionType minionType,MinionLevels minionLevels, int currentLevel)
    {
        this.m_minionType = minionType;
        this.m_minionLevels = minionLevels;
        this.m_currentLevel = currentLevel;
    }

}
