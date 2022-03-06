# Differences between the original repo

> New demo (v2) script can be found [here](https://docs.google.com/spreadsheets/d/1oB2HXQk431cSjQhKUycwlZqBkFeKDjdaRFOy_vxZAyM/edit?usp=sharing)

## 從 Unity 直接下載 Google Sheet 的資料
- 在 ActionSetting, FlagSetting 跟 WordSetting 加了個填網址的欄位跟下載的按鈕
- 這樣方便多了 :100:

## 對話內容可以顯示 flag 字串
- 聊天文本可以透過 {} 放入 flag 的名稱
- 執行時會用該 flag 的值去查 Word 裡面的字串，並取代文本內 {} 的內容

## 多選項支援
- 現在可以顯示多個選項 (Choices)
- 試算表格式請見上方 demo 連結
- 動畫的部分我用程式重寫 (原本是 animator)

## 啟動組件
- 把物件包裝在 prefab 裡，要使用時直接 instantiate 使用
- 新增 UniAvatar 組件，作為該 prefab 的啟動器

## 監聽結束事件
- 可以掛對話結束後要做什麼 (UniAvatar 或 GameStoryManager 的 OnFinishStory)

## 其他小改動與修正
- 在 Word 找不到 key 的時候，之前會報錯並卡住，現在是會報出警告，但是會先回傳 key 作為文本內容
    - Flag 也是類似，當要設定 flag 值時，找不到該 flag 也是先讓它設
- 修正在對話進行中 (typing) 的時候按下互動鍵會直接跳到下一句對話
- 修正在選項出現時按互動鍵，會直接跳過該選項直接進行下一段對話
- 修正讀取 Google Sheet CSV 時出現 `換行` 與 `"` 和 `,` 造成的問題


@jcxyis 20220302