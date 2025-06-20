using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibraryManage.Src.Models;

/// <summary>
/// 管理书籍的添加、删除、显示和搜索功能。
/// </summary>
public class BookManager
{
    private readonly string _path;         // 存储数据的文件路径
    private readonly List<Book> _books;    // 书籍列表

    /// <summary>
    /// 构造函数，初始化文件路径并加载数据。
    /// </summary>
    public BookManager(string path)
    {
        _path = path;
        _books = File.Exists(_path) ? FileHelper.LoadFromFile(path) : new List<Book>();
        CleanUpBooks(); // 初始加载后进行数据清理
    }

    /// <summary>
    /// 清理加载的书籍数据，移除空项与重复ID，并重新分配连续ID。
    /// </summary>
    private void CleanUpBooks()
    {
        // 移除标题或作者为空的书籍
        _books.RemoveAll(book => string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author));

        // 处理重复 ID，保留每组第一个
        var distinctBooks = _books.GroupBy(b => b.Id).Select(g => g.First()).ToList();

        // 重新分配 ID（从 1 开始连续）
        if (distinctBooks.Count > 0)
        {
            int currentId = 1;
            foreach (var book in distinctBooks.OrderBy(b => b.Id))
            {
                book.Id = currentId++;
            }
        }

        // 更新列表并保存
        _books.Clear();
        _books.AddRange(distinctBooks.OrderBy(b => b.Id));
        Save();
    }

    /// <summary>
    /// 保存书籍列表到文件。
    /// </summary>
    public void Save() => FileHelper.SaveToFile(_path, _books);

    /// <summary>
    /// 打印标题边框。
    /// </summary>
    public void PrintBoard(string title)
    {
        Console.WriteLine("\n--------------");
        if (!string.IsNullOrEmpty(title))
            Console.WriteLine($"|{title,-5}|");
        Console.WriteLine("--------------\n");
    }

    /// <summary>
    /// 添加新书籍的交互逻辑。
    /// </summary>
    public void AddBook()
    {
        while (true)
        {
            Console.Clear();
            PrintBoard("增添书目(留白自动退出)");

            var (title, author) = PromptBookInfo();
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author)) break;

            if (IsInputInvalid(title, author)) continue;
            if (IsBookDuplicate(title, author)) continue;

            var book = CreateBook(title, author);
            SaveNewBook(book);

            if (ShouldReturnToMenu()) break;
        }
    }

    /// <summary>
    /// 提示用户输入书名和作者。
    /// </summary>
    private (string? Title, string? Author) PromptBookInfo()
    {
        Console.Write("书名：");
        string? title = Console.ReadLine();

        Console.Write("作者：");
        string? author = Console.ReadLine();

        return (title, author);
    }

    /// <summary>
    /// 检查用户输入是否为空。
    /// </summary>
    private bool IsInputInvalid(string? title, string? author)
    {
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
        {
            Console.WriteLine("书名和作者不能为空！请重新输入。");
            Pause();
            return true;
        }
        return false;
    }

    /// <summary>
    /// 检查书籍是否重复（书名 + 作者 相同）。
    /// </summary>
    private bool IsBookDuplicate(string title, string author)
    {
        if (_books.Any(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase) &&
                            b.Author.Equals(author, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine($"书籍 《{title}》 by {author} 已存在！请勿重复添加。");
            Pause();
            return true;
        }
        return false;
    }

    /// <summary>
    /// 创建一个新的 Book 实例。
    /// </summary>
    private Book CreateBook(string title, string author)
    {
        int nextId = _books.Count > 0 ? _books.Max(b => b.Id) + 1 : 1;
        return new Book { Id = nextId, Title = title, Author = author };
    }

    /// <summary>
    /// 将新书添加到列表并保存。
    /// </summary>
    private void SaveNewBook(Book book)
    {
        _books.Add(book);
        Save();
        Console.WriteLine($"保存成功 |《{book.Title}》 by {book.Author}|");
    }

    /// <summary>
    /// 删除书籍的交互逻辑。
    /// </summary>
    public void RemoveBook()
    {
        while (true)
        {
            Console.Clear();
            PrintBoard("删减书目(留白自动退出)");

            if (!_books.Any())
            {
                Console.Write("\n暂无书籍可删减!按任意键继续……");
                Console.ReadKey();
                return;
            }

            Console.Write("请输入要删除的书籍 ID: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) break;

            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("\n无效的 ID 输入!ID 必须是数字。");
                Pause();
                continue;
            }

            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                Console.WriteLine($"未找到编号为 [{id}] 的书籍！");
                Pause();
                continue;
            }

            PrintBook([book]);

            Console.Write("确认删除吗？输入 Y 确认，其他键取消：");
            string? confirm = Console.ReadLine();
            if (confirm != null && confirm.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                _books.Remove(book);
                Save();
                Console.WriteLine($"编号为 [{id}] 的书籍已成功删除！");
            }
            else
            {
                Console.WriteLine("已取消删除操作。");
            }

            if (ShouldReturnToMenu()) break;
        }
    }

    /// <summary>
    /// 展示当前所有书籍。
    /// </summary>
    public void DisplayBooks()
    {
        Console.Clear();
        PrintBoard("展示书籍");

        if (_books.Count == 0)
        {
            Console.WriteLine("暂无书籍展示!");
        }
        else
        {
            _books.ForEach(book => Console.WriteLine(book));
        }

        Pause();
    }

    /// <summary>
    /// 搜索书籍并展示结果。
    /// </summary>
    public void SearchBook()
    {
        while (true)
        {
            Console.Clear();
            PrintBoard("搜索书目");

            Console.Write("请输入关键词(书名或作者，留白返回)：");
            string? keyword = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(keyword)) break;

            var results = SearchByKeyword(keyword);
            if (!DisplaySearchResults(keyword, results)) continue;

            if (ShouldReturnToMenu()) break;
        }
    }

    /// <summary>
    /// 执行关键词搜索逻辑。
    /// </summary>
    private List<Book> SearchByKeyword(string keyword)
    {
        return [.. _books.Where(book =>
            book.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            book.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase))];
    }

    /// <summary>
    /// 展示搜索结果列表，若无匹配则提示。
    /// </summary>
    private bool DisplaySearchResults(string keyword, List<Book> results)
    {
        if (results.Count == 0)
        {
            Console.WriteLine($"没有匹配 '{keyword}' 的书籍!");
            Pause();
            return false;
        }

        PrintBook(results);
        return true;
    }

    /// <summary>
    /// 打印书籍信息。
    /// </summary>
    private void PrintBook(List<Book> books)
    {
        Console.WriteLine("\n--- 查询结果 ---");
        foreach (var book in books)
        {
            Console.WriteLine($"ID: {book.Id}");
            Console.WriteLine($"书名：《{book.Title}》");
            Console.WriteLine($"作者：{book.Author}");
            Console.WriteLine("------------------");
        }
    }

    /// <summary>
    /// 暂停，等待用户按键继续。
    /// </summary>
    private void Pause()
    {
        Console.WriteLine("\n按任意键继续……");
        Console.ReadKey();
    }

    /// <summary>
    /// 判断用户是否按下 Enter 键来返回主菜单。
    /// </summary>
    private bool ShouldReturnToMenu()
    {
        Console.Write("\n按任意键继续操作，或按 Enter 返回主菜单……");
        return Console.ReadKey().Key == ConsoleKey.Enter;
    }
}
