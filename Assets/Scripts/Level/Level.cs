﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject[] blockPrefabs;
    GameObject currentLevel;
    private GameObject[,] blockMap; //Contains all the Block gameObjects in the current level
    private Level_Data levelData; //Contains all the information used to save and load the levels
    private int width, height;

    #region Unity
    private void Awake()
    {
        Gamemaster.Instance.Register(this);
    }
    #endregion

    #region LevelCreation
    /// <summary>
    /// Create a new Level from scratch
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="name"></param>
    public void CreateNewLevel(int width, int height, string name)
    {
        this.width = width;
        this.height = height;
        levelData = new Level_Data(width, height, name);
        CreateLevel();
    }

    /// <summary>
    /// Load levelData from fileSystem and create the level
    /// </summary>
    /// <param name="levelName">Name of Level</param>
    /// <param name="subFolder">FolderName e.g. Custom</param>
    public void LoadLevelFromFile(string levelName, string subFolder)
    {
        levelData = LevelSaveLoad.Load(levelName, subFolder);
        this.width = levelData.BlockMap.GetLength(0);
        this.height = levelData.BlockMap.GetLength(1);
        CreateLevel();
    }

    /// <summary>
    /// Creates levelObjects and initializes them with the data in levelData
    /// </summary>
    private void CreateLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
        currentLevel = new GameObject();
        currentLevel.name = levelData.Name;
        currentLevel.transform.SetParent(this.transform);
        this.transform.position = new Vector3(-height * Block_Data.BlockSize / 2f, -width * Block_Data.BlockSize / 2f, 0); //To do maybe change posiiton of level
        blockMap = new GameObject[width, height];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Block_Data blockData = levelData.BlockMap[j, i];
                GameObject blockObject = Instantiate(blockPrefabs[(int)blockData.BlockType]);
                blockObject.name = "[" + j + "-" + i + "]";
                blockObject.transform.SetParent(currentLevel.transform);
                blockObject.transform.localPosition = new Vector3(j * Block_Data.BlockSize, i * Block_Data.BlockSize, 0);
                blockMap[j, i] = blockObject;
                blockObject.GetComponent<Block>().InitializeBlock(blockData);
            }
        }
    }
    #endregion

    #region LevelEditing

    public void SetStartPlatform(int x, int y)
    {
        if (x == 0) x++;
        else if (x == width - 1) x--;

        int oldX = levelData.StartPlatformCoordinates.x;
        int oldY = levelData.StartPlatformCoordinates.y;

        if (oldX == x && oldY == y)
            return;

        //Remove old StartPlatform
        EmptyBlock_Data emptyData = new EmptyBlock_Data();
        for (int i = 0; i < 3; i++)
        {
            SetBlock((oldX - 1 + i), oldY, emptyData);
        }

        //Place new StartPlatform
        StartBlock_Data startData = new StartBlock_Data();
        for (int i = 0; i < 3; i++)
        {
            SetBlock((x - 1 + i), y, startData);
        }
        levelData.StartPlatformCoordinates = new Vector2Int(x, y);
    }

    public void SetGoalPlatform(int x, int y)
    {
        if (x == 0) x++;
        else if (x == width - 1) x--;

        int oldX = levelData.GoalPlatformCoordinates.x;
        int oldY = levelData.GoalPlatformCoordinates.y;

        if (oldX == x && oldY == y)
            return;

        //Remove old GoalPlatform
        EmptyBlock_Data emptyData = new EmptyBlock_Data();
        for (int i = 0; i < 3; i++)
        {
            SetBlock((oldX - 1 + i), oldY, emptyData);
        }

        //Place new GoalPlatform
        GoalBlock_Data data = new GoalBlock_Data();
        for (int i = 0; i < 3; i++)
        {
            SetBlock((x - 1 + i), y, data);
        }

        levelData.GoalPlatformCoordinates = new Vector2Int(x, y);
    }

    public void EmptyBlock(int x, int y)
    {
        SetBlock(x, y, new EmptyBlock_Data());
    }

    public void SetBlock(int x, int y, Block_Data data)
    {
        GameObject newBlockObject = Instantiate(blockPrefabs[(int)data.BlockType]);
        newBlockObject.transform.position = blockMap[x, y].transform.position;
        Destroy(blockMap[x, y]);//Could lead to performance problems
        blockMap[x, y] = newBlockObject;
        newBlockObject.name = "[" + x + "-" + y + "]";
        newBlockObject.transform.SetParent(currentLevel.transform);
        Block newBlock = newBlockObject.GetComponent<Block>();
        newBlock.InitializeBlock(data);
        levelData.BlockMap[x, y] = data;
    }

    #endregion

    #region DebugMethods
    public Level_Data GetLevelData()
    {
        return levelData;
    }
    #endregion

}