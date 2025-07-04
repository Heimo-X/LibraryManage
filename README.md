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

- 📥 **添加书籍**：输入书名与作者，系统将检查是否重复，并保存到本地 JSON 文件。
- 🗑️ **删除书籍**：根据书籍的唯一 ID 删除对应条目，删除前会进行二次确认。
- 📖 **查看书籍**：以清晰的列表形式展示所有已保存的书目。
- 🔍 **搜索书籍**：支持根据书名或作者关键词进行模糊搜索，方便快速查找。
- 💾 **自动保存**：所有添加和删除操作都会自动写入本地 JSON 文件，确保数据持久化。

---

## 📂 项目结构

```
LibraryManage/
├── Program.cs           // 主程序，命令行交互入口
├── Modules/
│   ├── Book.cs          // 图书数据类，定义书籍属性及比较逻辑
│   ├── BookManager.cs   // 核心业务逻辑管理类，处理书籍的增删查和数据校验
│   ├── FileHelper.cs    // JSON 文件读写工具类，负责数据持久化和异常处理
│   └── AppConstant.cs   // 常量配置，定义文件路径等
└── books.json           // 自动生成的图书数据库文件
```

---

## 💡 项目亮点

- **面向对象 + 模块化设计**：项目结构清晰，职责分离，易于理解和未来扩展。
- **健壮的数据管理**：
    - **自动 ID 分配**：新添加书籍的 ID 会自动生成并保持唯一性。
    - **数据校验**：对用户输入（如书名、作者、ID）进行有效性检查，防止无效数据。
    - **重复项检查**：添加书籍时会检查是否已存在同名同作者的书籍，避免重复添加。
    - **加载数据清理**：程序启动时，对从 JSON 文件加载的数据进行初步清理，提升数据质量。
- **完善的异常处理**：文件读写操作采用细致的 `try-catch` 块，捕获并处理 `IOException`、`JsonException` 等具体异常，增强程序稳定性。
- **友好的用户交互**：提供清晰的命令行提示、操作反馈和错误信息，提升用户体验。
- **高效的搜索功能**：支持基于书名或作者的关键词模糊搜索，快速定位目标书籍。
- **数据持久化**：使用 JSON 文件进行本地数据存储，方便备份和迁移，无需数据库依赖。
- **使用 `Equals()` + `GetHashCode()` 实现书籍查重**：在 `Book` 类中重写了 `Equals()` 和 `GetHashCode()` 方法，确保基于书籍内容（标题和作者）进行准确的查重判断。

---

## 🚀 如何运行

1.  克隆项目到本地：
    ```bash
    git clone git@github.com:YourUsername/LibraryManage.git
    ```
2.  进入项目目录：
    ```bash
    cd LibraryManage
    ```
3.  使用 Visual Studio 或命令行运行项目：
    ```bash
    dotnet run
    ```