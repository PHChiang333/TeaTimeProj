TeaTimeProject
---
參考書籍: 輕鬆自學ASP.NET Core MVC(.NET 8)：從建置到部署的Web程式經典範例實作 (ISBN:978-626-7383-07-0)

* 使用框架: ASp.NET Core 8 (.NET8 LTS) MVC

* 學習目標: 熟悉 .NET8 MVC, DI, Layered Architecture

* 網站分為 Pruduct(including Landing Page), Member, Cart, Order management 四個part

* 架構設計: 
  - 展示層 (Presentation Layer) -> Proj: Controllers and Views
  - 商業邏輯層（Business Layer） -> None
  - 資料存取層 (Data Access Layer) -> DataAccess: Repos and UnitOfWork
  - 持久層 (Persistence Layer) -> Model: Domain Entities and ViewModels
  - 共用層（Common Layer）-> Utility

目前進度: Product
