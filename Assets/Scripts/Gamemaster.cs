﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Gamemaster : Singleton<Gamemaster>
{
    public bool ApplicationQuit { get => IsApplicationQuitting; }
    private Level level;
    private PlayCameraControl playCamera;
    private PlaySceneUI playSceneUI;
    private LevelEditor editor;
    private Player player;

    public bool IsUsingXbox { get; set; }
    private LevelType nextLevelType = LevelType.Main;
    private string nextLevelName;
    private int nextLevelId = 1;
    private int numberOfMainLevels = -1;

    #region Level
    public LevelType GetLevelType()
    {
        return nextLevelType;
    }

    public bool HasNextLevel()
    {
        if(numberOfMainLevels == -1)
        {
            numberOfMainLevels = Directory.GetFiles(FilePaths.MainLevelFolder).Where(filePath => filePath.EndsWith(".dat")).Count();
        }
        return nextLevelId < numberOfMainLevels;
    }

    public int GetLevelId()
    {
        return nextLevelId;
    }

    /// <summary>
    /// SEts nextLevelType to default value(Main)
    /// </summary>
    public void SetDefaultLevelToLoad()
    {
        nextLevelType = LevelType.Main;
    }

    public void SetNextMainLevelToLoad()
    {
        nextLevelType = LevelType.Main;
        nextLevelId++;
    }

    public void SetNextMainLevelToLoad(int levelId)
    {
        nextLevelType = LevelType.Main;
        nextLevelId = levelId;
    }

    public void SetNextCustomLevelToLoad(string levelName)
    {
        nextLevelType = LevelType.Custom;
        nextLevelName = levelName;
    }

    public void SetNextTestLevelToLoad()
    {
        nextLevelType = LevelType.Test;
    }

    public void CreatePlayLevel()
    {
        if (level == null)
        {
            throw new InvalidOperationException("No level registered yet");
        }
        if (nextLevelType == LevelType.Main)
            level.LoadLevelFromFile("Level" + nextLevelId, FilePaths.MainLevelFolder);
        else if (nextLevelType == LevelType.Custom)
            level.LoadLevelFromFile(nextLevelName, FilePaths.CustomPlayLevelFolder);
    }

    public void Register(Level level)
    {
        if (nextLevelType == LevelType.Test)
        {
            level.CopyLevel(this.level.GetLevelData());
        }
        this.level = level;
    }

    public Level GetLevel()
    {
        return this.level;
    }
    #endregion

    #region LevelEditor
    public void Register(LevelEditor editor)
    {
        this.editor = editor;
    }

    public LevelEditor GetLevelEditor()
    {
        return this.editor;
    }
    #endregion

    #region PlayCameraControl
    public void Register(PlayCameraControl camera)
    {
        playCamera = camera;
    }

    public PlayCameraControl GetCameraPlayControl()
    {
        return playCamera;
    }
    #endregion

    #region PlaySceneUI
    public void Register(PlaySceneUI playUI)
    {
        this.playSceneUI = playUI;
    }

    public PlaySceneUI GetPlaySceneUI()
    {
        return this.playSceneUI;
    }
    #endregion

    #region Player

    public void Register(Player player)
    {
        this.player = player;
    }

    public Player GetPlayer()
    {
        return this.player;
    }

    #endregion


}
