﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoCaller : MonoBehaviour
{
    private Level_Data data;
    // Start is called before the first frame update
    void Start()
    {
        Gamemaster.Instance.GetLevel().CreateNewLevel(8, 7, "testLevel");
        data = Gamemaster.Instance.GetLevel().GetLevelData();
        //Gamemaster.Instance.GetLevel().LoadLevelFromFile("testLevel", "TestLevels");
    }

    private void Update()
    {
        if(Input.GetKeyDown("j"))
            Gamemaster.Instance.GetLevel().SetBlock(3, 3, new WoodBlock_Data());
        else
        if (Input.GetKeyDown("k"))
            Gamemaster.Instance.GetLevel().SetStartPlatform(0, 0);

        if (Input.GetKeyDown("l"))
            Gamemaster.Instance.GetLevel().SetGoalPlatform(6, 6);
    }

    private void OnApplicationQuit()
    {
        LevelSaveLoad.Save(data, "TestLevels");
    }

}