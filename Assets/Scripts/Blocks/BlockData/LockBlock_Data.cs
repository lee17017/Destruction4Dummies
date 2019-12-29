﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LockBlock_Data : Block_Data
{
    public static Color[] LockBlockColors = new Color[5] { Color.white, Color.red, Color.green, Color.magenta, Color.blue };
    public override BlockType BlockType => BlockType.Lock;
    private uint blockID = 0;

    private bool blockIsLocked = true;

    public LockBlock_Data()
    {

    }

    public LockBlock_Data(uint blockID)
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

    public void SetLock(bool blockIsLocked)
    {
        this.blockIsLocked = blockIsLocked;
    }

    public bool GetLock()
    {
        return blockIsLocked;
    }

    public override bool Equals(Block_Data other)
    {
        return base.Equals(other) && this.blockID == ((LockBlock_Data)other).GetID();
    }
}
