﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBlock : Block
{
    private Block_Data woodBlockData = new WoodBlock_Data();
    public override Block_Data BlockData { get => woodBlockData; set => woodBlockData = value; }


    #region Initialization / Destruction
    public override void InitializeBlock(Block_Data data)
    {
        base.InitializeBlock(data);
    }

    protected override void DestroyBlock()
    {
        base.DestroyBlock();
    }

    public override void ResetBlock()
    {
        base.ResetBlock();
    }

    #endregion

    #region PlayerInteraction

    protected override void OnTouch(GameObject player)
    {
        base.OnTouch(player);
    }

    protected override void OnTouchEnd(GameObject player)
    {
        base.OnTouchEnd(player);
    }

    #endregion


}