# 級聯型生成器模式 (Cascade Builder Pattern)

## 介紹及使用方式
> 級聯型生成器模式，較傳統生成器模式多了流程引導，此種設計模式可以有效引導套件使用者使用函式。

級聯元件具有三種
- 初始級聯器：```Initial<TNext>``` 及 ```Initial<TNext, T>``` 
- 管道級聯器：```Pipeline<TNext, T>```
- 輸出級聯器：```Output<T>```

> 實作時，建議用堆疊式思維設計每個級聯器，也就是由 ***Output*** 類別開始至 ***Initial*** 類別。
