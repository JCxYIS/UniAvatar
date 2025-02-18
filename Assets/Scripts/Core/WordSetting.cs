﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utopia;
using TMPro;

namespace UniAvatar
{
    [Serializable]
    public class WordData
    {
        public string PrimaryKey;
        public string[] Contents;
    }

    [CreateAssetMenu(fileName = "WordSetting", menuName = "UniAvatar/WordSetting")]
    public class WordSetting : BaseSetting
    {
        [Header("Word Settings")]
        public TextAsset WordsheetCSV;
        public List<WordData> WordSheet = new List<WordData>();
        public List<string> Languages;

        protected override TextAsset Csv => WordsheetCSV;

        public void SetUpWord(string dataStr = "")
        {
            if(dataStr == "")
                dataStr = WordsheetCSV.text;

            var grid = CSVReader.SplitCsvGrid(dataStr);

            WordSheet = new List<WordData>();
            Languages = new List<string>();
            
            // Get Languages
            for (var i = 1; i < grid.GetLength(0); i++)
            {
                Languages.Add(grid[i, 0]);
            }

            for (var i = 1; i < grid.GetLength(1); i++)
            {
                WordData data = new WordData();
                data.PrimaryKey = grid[0, i];
                int colLen = grid.GetLength(0);
                data.Contents = new string[colLen - 1];
                for (var j = 1; j < colLen; j++)
                {
                    data.Contents[j - 1] = grid[j, i];
                }
                if (!string.IsNullOrEmpty(data.PrimaryKey))
                    WordSheet.Add(data);
            }

            GC.Collect();
        }
    }
}