# Differences between the original repo

> New demo (v2) script can be found [here](https://docs.google.com/spreadsheets/d/1oB2HXQk431cSjQhKUycwlZqBkFeKDjdaRFOy_vxZAyM/edit?usp=sharing)

## 從 Unity 直接下載 Google Sheet 的資料
- 在 ActionSetting, FlagSetting 跟 WordSetting 加了個填網址的欄位跟下載的按鈕
- 這樣方便多了 :100:

## 多選項支援
- 現在可以顯示多個選項 (Choices)
- 試算表格式請見上方 demo 連結
- 動畫的部分我用程式重寫 (原本是 animator)

## 啟動組件
- 把物件包裝在 prefab 裡，要使用時直接 instantiate 使用
- 新增 UniAvatar 組件，作為該 prefab 的啟動器

## 監聽結束事件
- 可以掛對話結束後要做什麼 (UniAvatar 或 GameStoryManager 的 OnFinishStory)

## 其他小改動
- 像是在 Word 找不到 key 的時候，之前會報錯並卡住，現在一樣抱錯，但先回傳 key
    - Flag 也是，找不到 flag 也是先讓它設


@jcxyis 20220302