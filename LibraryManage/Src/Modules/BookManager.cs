using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; // 引入 Linq 命名空间，用于 Max 和 Any 方法

namespace LibraryManage.Src.Modules;

/// <summary>
/// 管理书籍的添加、删除、显示和搜索功能。
/// </summary>
public class BookManager
{
    private readonly string _path; // 存储文件路径
    private readonly List<Book> _books; // 存储书籍列表

    /// <summary>
    /// 使用指定的路径初始化 BookManager 类的新实例，并从文件中加载书籍。
    /// </summary>
    /// <param name="path">存储书籍数据的文件的路径。</param>
    public BookManager(string path)
    {
        _path = path;
        _books = new List<Book>(); // 初始化书籍列表

        // 如果文件存在，则尝试从文件加载书籍
        if (File.Exists(_path))
        {
            _books = FileHelper.LoadFromFile(path);
            // 数据校验：加载后可以对书籍列表进行初步检查，例如移除Id重复或数据不完整的项
            // 这里可以添加逻辑来处理潜在的“脏数据”
            CleanUpBooks(); // 调用内部方法清理加载的书籍数据
        }
    }

    /// <summary>
    /// 清理加载的书籍数据，例如移除重复或不完整的书籍。
    /// </summary>
    private void CleanUpBooks()
    {
        // 示例：移除标题或作者为空白的书籍
        _books.RemoveAll(book => string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author));

        // 示例：处理重复的ID，可以考虑重新分配ID或者保留第一个
        // 这里的逻辑会根据ID对书籍进行分组，只保留每组的第一个，确保ID的唯一性
        // 注意：这会改变书籍的原始ID，如果ID是外部引用，可能需要更复杂的策略
        var distinctBooks = _books.GroupBy(b => b.Id)
                                  .Select(g => g.First())
                                  .ToList();
        
        // 示例：确保ID连续且从1开始（如果需要）
        // 如果您的ID不是严格要求连续，这部分可以省略
        if (distinctBooks.Count > 0)
        {
            int currentId = 1;
            foreach (var book in distinctBooks.OrderBy(b => b.Id)) // 按现有ID排序，确保处理顺序
            {
                book.Id = currentId++;
            }
        }
        _books.Clear();
        _books.AddRange(distinctBooks.OrderBy(b => b.Id)); // 清理后按ID排序添加
        Save(); // 清理后保存一下，确保文件数据是干净的
    }


    /// <summary>
    /// 将当前书籍列表保存到文件。
    /// </summary>
    public void Save() => FileHelper.SaveToFile(_path, _books);

    /// <summary>
    /// 在控制台打印一个带标题的边框。
    /// </summary>
    /// <param name="title">要显示的标题。</param>
    public void PrintBoard(string title)
    {
        System.Console.WriteLine("\n--------------");
        if (!string.IsNullOrEmpty(title))
            System.Console.WriteLine($"|{title,-5}|"); // 格式化输出标题，左对齐，宽度为5
        System.Console.WriteLine("--------------\n");
    }

    /// <summary>
    /// 允许用户添加新书籍。
    /// </summary>
    public void AddBook()
    {
        while (true)
        {
            Console.Clear();
            PrintBoard("增添书目(留白自动退出)");

            System.Console.Write("书名：");
            string? title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title)) break; // 如果为空白，则退出

            System.Console.Write("作者：");
            string? author = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(author)) break; // 如果为空白，则退出

            // 数据校验：确保书名和作者不为空白
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
            {
                System.Console.WriteLine("书名和作者不能为空！请重新输入。");
                System.Console.Write("\n按任意键继续……");
                Console.ReadKey();
                continue; // 继续循环，让用户重新输入
            }

            // 业务逻辑校验：检查书籍是否已存在 (根据标题和作者判断，不区分大小写)
            if (_books.Any(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase) &&
                                 b.Author.Equals(author, StringComparison.OrdinalIgnoreCase)))
            {
                System.Console.WriteLine($"书籍 《{title}》 by {author} 已存在！请勿重复添加。");
                System.Console.Write("\n按任意键继续……");
                Console.ReadKey();
                continue; // 继续循环，让用户重新输入或退出
            }

            // 生成下一个书籍ID，如果列表为空则从1开始
            int nextId = _books.Count > 0 ? _books.Max(b => b.Id) + 1 : 1;
            // 创建新书籍对象并添加到列表中
            _books.Add(new Book { Id = nextId, Title = title, Author = author });
            Save(); // 保存到文件
            System.Console.WriteLine($"保存成功 |《{title}》 by {author}|");

            System.Console.Write("\n按任意键继续……");
            Console.ReadKey();
            Console.Clear();
        }
    }

    /// <summary>
    /// 允许用户删除现有书籍。
    /// </summary>
    public void RemoveBook()
    {
        while (true)
        {
            Console.Clear();
            PrintBoard("删减书目(留白自动退出)");

            if (_books.Count == 0)
            {
                System.Console.Write("\n暂无书籍可删减!按任意键继续……");
                Console.ReadKey();
                return; // 没有书籍可删除，直接返回
            }

            System.Console.Write("请输入要删除的书籍 ID: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) break; // 如果为空白，则退出

            // 数据校验：确保输入的ID是有效数字
            if (!int.TryParse(input, out int id))
            {
                System.Console.Write("\n无效的 ID 输入!ID 必须是数字。按任意键继续……");
                Console.ReadKey();
                Console.Clear();
                continue; // 继续循环，让用户重新输入
            }

            // 查找匹配ID的书籍
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                System.Console.WriteLine($"未找到编号为 [{id}] 的书籍！请检查 ID 是否正确。按任意键继续……");
                Console.ReadKey();
                Console.Clear();
                continue; // 继续循环，让用户重新输入
            }

            System.Console.WriteLine($"""
                --- 查询到以下信息 ---
                ID: {id}
                书名：{book.Title}
                作者：{book.Author}
                ---------------------
                """);

            System.Console.Write("确认删除吗？输入 Y 确认，其他键取消：");
            string? confirm = Console.ReadLine();
            // 确认删除，不区分大小写
            if (confirm != null && confirm.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                _books.Remove(book); // 从列表中移除书籍
                Save(); // 保存到文件
                System.Console.WriteLine($"编号为 [{id}] 的书籍已成功删除！");
            }
            else
            {
                System.Console.WriteLine("已取消删除操作。");
            }

            Console.Write("\n按任意键继续……");
            Console.ReadKey();
            Console.Clear();
        }
    }

    /// <summary>
    /// 显示所有现有书籍。
    /// </summary>
    public void DisplayBooks()
    {
        Console.Clear();
        PrintBoard("展示书籍");

        if (_books.Count == 0)
        {
            System.Console.WriteLine("暂无书籍展示!");
        }
        else
        {
            // 遍历并显示所有书籍
            _books.ForEach(book => System.Console.WriteLine($"{book}"));
        }

        System.Console.WriteLine();
        System.Console.Write("按任意键退出……");
        Console.ReadKey();
    }

    /// <summary>
    /// 允许用户通过关键词搜索书籍。
    /// </summary>
    public void SearchBooks()
    {
        while (true)
        {
            Console.Clear();
            PrintBoard("搜索书目");

            System.Console.Write("请输入关键词(书名或作者，留白返回)：");
            string? keyword = System.Console.ReadLine();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                // 如果关键词为空白，则退出搜索循环
                break;
            }

            // 根据关键词搜索书籍，不区分大小写
            var results = _books
                .Where(book => book.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                book.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine($"没有匹配 '{keyword}' 的书籍!按任意键继续……");
                Console.ReadKey();
                continue; // 继续循环，让用户可以再次搜索
            }
            else
            {
                System.Console.WriteLine("\n------- 搜索结果 -------");
                results.ForEach(book => Console.WriteLine(book));
                System.Console.WriteLine("-------------------------");

                System.Console.Write("\n按任意键继续搜索，或按 Enter 返回主菜单……");
                // 用户按 Enter 键时，ReadLine() 返回空字符串，此时退出搜索循环
                if (Console.ReadKey().Key == ConsoleKey.Enter) 
                {
                    break;
                }
            }
        }
    }
}