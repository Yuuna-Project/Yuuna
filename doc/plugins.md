# 模組開發
## 導言
1. 在專案中引用 ```Yuuna.Contracts.dll```，裡面有大部分開發時你所需要的類別，除非你需要一些特殊的功能(像是序列化之類的)
    ，這時你可以考慮引用 ```Yuuna.Common.dll``` 這個函式庫。
2. 在設計模組及其類別介面時，為開發者提供了建構者模式(Builder Pattern)，逐一引導開發者建構模組行為，未來有需要我會考慮導入 IoC <del>及 Config (這部分我會優先處理)</del>。
3. 可以直接參考 Yuuna.Contracts.Test 這個專案中模組的實作方式。

##### **Config 的部分已經完成**

## 名詞解釋
### 同義詞 - Synonym
由開發者定義的多個相同意思的詞彙字串所組成，語法系統的最小單位。
### 群組 - Group
由開發者將多個類型相似或性質相似的同義詞進行分組及規劃所產生。
### 模式 - Pattern
由多個群組順序性串接而成，用以比對輸入的意圖是否具順序性的匹配。

## 建構文法規則及模組行為
1. 使用 ```IGroupManager``` 介面的 ```Define``` 方法定義群組名稱。
2. 再透過傳回的 ```IGroup``` 物件中的 ```AppendOrCreate``` 或其他相關方法定義同義詞物件。
3. 透過 ```IPatternBuilder``` 介面的 ```Build``` 方法及多個群組物件用以建構模式物件。
4. 並透過傳回的 ```IInvokeBuilder``` 物件所提供的方法 ```OnInvoke``` 建構 ```Invoke``` 委派物件。

## 模組處理流程
1. 當分詞器將輸入字串分詞後，會送入 ```Strategy``` 物件中，將分詞後的字串序列與模式物件進行評估及分析並傳回最佳的匹配結果物件。
2. ```Invoke``` 委派類型表示匹配結果為最佳時將進行的動作，並由開發者處理並回傳 ```Response``` 結構用以與使用者視圖層的互動。

## 模組的設定檔與自動保存
1. 在模組中定義你需要做為設定的屬性，並在上面加上 ```[Field]``` 標籤且 **屬性不可以為自動屬性**。
2. 透過 ```BuildPatterns``` 中的參數 ```config``` 存取你需要的屬性，並會在值不同時自動更新值到設定檔中。
```csharp
// 範例
[Field()]
public string Example { get; set; }
[Field("alias")]
public string ExampleWithAlias { get; set; }
...
void BuildPatterns(... dynamic config)
{
    ...
    var example = config.Example;
    var exampleWithAlias = config.alias;
}
```