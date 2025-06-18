namespace LibraryManage.Modules;

public class BookManager
{
    private readonly string _path;
    private readonly List<Book> _books;

    public BookManager(string path)
    {
        _path = path;
        _books = new List<Book>();

        if (File.Exists(_path))
        {
            _books = FileHelper.LoadFromFile(path);
        }
    }

    public void Save() => FileHelper.SaveToFile(_path, _books);

    public void PrintBoard(string title)
    {
        System.Console.WriteLine("\n--------------");
        if (!string.IsNullOrEmpty(title))
            System.Console.WriteLine($"|{title,-5}|");
        System.Console.WriteLine("--------------\n");
    }

    public void AddBook()
    {
        while (true)
        {
            PrintBoard("增添书目(留白自动退出)");

            System.Console.Write("书名：");
            string? title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title)) break;

            System.Console.Write("作者：");
            string? author = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(author)) break;

            _books.Add(new Book { Title = title, Author = author });
            Save();
            System.Console.WriteLine($"《{title}》 by {author} 保存成功!");

            System.Console.Write("按任意键继续……");
            Console.ReadKey();
            Console.Clear();
        }
    }

    public void RemoveBook()
    {
        while (true)
        {
            PrintBoard("删减书目(留白自动退出)");
            if (_books.Count == 0)
            {
                System.Console.Write("暂无书籍可删减!按任意键继续……");
                Console.ReadKey();
                return;
            }

            System.Console.Write("书名：");
            string? title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title)) break;

            System.Console.Write("作者：");
            string? author = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(author)) break;

            bool removed = _books.Remove(new Book { Title = title, Author = author });
            Save();
            System.Console.WriteLine(removed ? $"《{title}》 by {author} 删减成功!" : $"《{title}》 by {author} 未找到!");
        }
    }

    public void DisplayBooks()
    {
        PrintBoard("展示书籍");

        if (_books.Count == 0)
        {
            System.Console.WriteLine("暂无书籍展示!");
            System.Console.WriteLine("");
            System.Console.Write("按任意键退出……");
            Console.ReadKey();
        }
        else
        {
            int index = 1;
            _books.ForEach(book => System.Console.WriteLine($"{index++}.{book}"));
            System.Console.WriteLine("");
            System.Console.Write("按任意键退出……");
            Console.ReadKey();
        }
    }
}