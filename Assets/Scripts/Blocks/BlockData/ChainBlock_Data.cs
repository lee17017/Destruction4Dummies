﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChainBlock_Data : Block_Data
{
    public override BlockType BlockType => BlockType.Chain;

    private uint blockID = 0;
    public ChainBlock_Data()
    {

    }

    public ChainBlock_Data(uint blockID)
    {
        this.blockID = blockID;
    }

    public void SetChainID(uint blockID)
    {
        this.blockID = blockID;
    }

    public uint GetChainID()
    {
        return blockID;
    }
}