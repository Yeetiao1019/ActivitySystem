# ActivitySystem
活動報名系統
筆記連結：https://hackmd.io/@n1533512/Hk0Bxf-0F

## 系統使用技術
- 主要使用 .NET 5 版本的 ASP.NET Core MVC （還有一些 RazorPages）來實現前後端服務
- 資料庫使用 SQL Server 2019 搭配 Entity Framework Core 5
- 使用 ASP.NET Core Identity 實現帳戶相關服務
- 單元測試框架使用 NUnit 3
- （ToDo）前端使用 React 框架，實現前後端分離

## 系統功能
- 使用者可使用此活動報名系統來建立活動以及報名活動
- （ToDo）結合 Facebook Message API 作為客服服務
- （ToDo）活動日期將近時，系統可自動寄發活動通知給使用者

## 建置須知
- 需自行更改 appsettings.json 內的 SMTP 服務，讓系統的發信功能可以正常運作
- appsettings.json 目前的 DB ConnectionString 是連接到 localdb，如有其他需求，則需要修改
