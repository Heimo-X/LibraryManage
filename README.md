# 📚 LibraryManage 图书管理系统

一个使用 C# 编写的**轻量级图书管理系统**，支持命令行下的增删查书籍操作，并通过 JSON 文件进行本地数据持久化。

---

## 🛠️ 技术栈

- **语言**：C#
- **框架**：.NET 9.0
- **数据存储**：JSON 文件（`books.json`）
- **依赖库**： [Newtonsoft.Json](https://www.newtonsoft.com/json)

---

## ✨ 功能介绍

- 📥 **添加书籍**：输入书名与作者后保存到数据库  
- 🗑️ **删除书籍**：根据书名与作者信息删除对应条目  
- 📖 **查看书籍**：以序号展示所有已保存的书目  
- 💾 **自动保存**：所有操作自动写入本地 JSON 文件

---

## 📂 项目结构

```

LibraryManage/
├── Program.cs                  // 主程序，命令行交互入口
├── Modules/
│   ├── Book.cs                // 图书数据类
│   ├── BookManager.cs         // 业务逻辑管理类
│   ├── FileHelper.cs          // JSON 文件读写工具类
│   └── AppConstant.cs         // 常量配置
└── books.json                 // 自动生成的图书数据库

````

---

## 💡 项目亮点

- 面向对象 + 模块化设计，结构清晰易扩展
- 使用 `Equals()` + `GetHashCode()` 实现书籍查重
- 控制台操作简洁直观
- 使用 JSON 文件进行数据本地化管理，方便备份/迁移

---

## 🚀 如何运行

1. 克隆项目到本地：
   ```bash
   git clone git@github.com:YourUsername/LibraryManage.git
   
2. 使用 Visual Studio 或命令行运行项目：
   ```bash
   dotnet run
