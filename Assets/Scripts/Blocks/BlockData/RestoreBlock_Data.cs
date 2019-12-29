﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RestoreBlock_Data : Block_Data
{
    public static Color[] RestoreBlockColors = new Color[5] { Color.white, Color.red, Color.green, Color.magenta, Color.blue };
    public override BlockType BlockType => BlockType.Restore;
    private uint blockID = 0;
    public RestoreBlock_Data()
    {

    }

    public RestoreBlock_Data(uint blockID)
    {
        this.blockID = blockID;
    }

    public void SetID(uint blockID)
    {
        this.blockID = blockID;
    }

    public uint GetID()
    {
        return blockID;
    }

    public override bool Equals(Block_Data other)
    {
        return base.Equals(other) && this.blockID == ((RestoreBlock_Data)other).GetID();
    }
}