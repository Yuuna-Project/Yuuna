# Google Cloud Speech V1
## 安裝方式
1. 進入 [**官網**](https://cloud.google.com/speech-to-text/docs/quickstart-client-libraries) 後。
2. 照著 **事前準備** 做完會產生一份帶有 secret 的 json 檔。
3. 把 json 檔改為 ***secret.json*** 並複製到執行檔的所在目錄就算完成安裝。

注意: ***不要把你的 secret 檔案公開、外流，有很大的機會害自己荷包失血。***

## 遺失 Secret 檔
1. 進入 [IAM 管理頁](https://console.cloud.google.com/iam-admin/serviceaccounts)。
2. 選擇你的專案，並在 **服務帳戶** 列表中的右方 **動作** 行中選擇 **建立金鑰**。
3. 選擇 JSON 格式，並照 [**安裝方式**](#安裝方式) 中的第三點處理。