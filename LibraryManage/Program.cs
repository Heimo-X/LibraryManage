using LibraryManage.Src.Models;
using System;

namespace LibraryManage;

class Program
{
    static void Main(string[] args)
    {
        var manager = new BookManager(AppConstant.FilePath);

        while (true)
        {
            Console.Clear();
            PrintMenu();

            Console.Write("请输入你的操作选择（输入序号）：");
            string? choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    manager.AddBook();
                    break;
                case "2":
                    manager.RemoveBook();
                    break;
                case "3":
                    manager.DisplayBooks();
                    break;
                case "4":
                    manager.SearchBook();
                    break;
                case "5":
                    ExitApp();
                    return;
                default:
                    Console.WriteLine("\n无效指令!请重新输入……");
                    Console.ReadKey();
                    break;
            }
        }
    }

    /// <summary>
    /// 打印菜单选项
    /// </summary>
    static void PrintMenu()
    {
        Console.WriteLine("""
            ┌───────────────┐
            │ 图书管理系统  │
            └───────────────┘
            1. 增添书目
            2. 删减书目
            3. 展示书目
            4. 搜索书目
            5. 退出系统
            """);
    }

    /// <summary>
    /// 退出程序时显示告别信息
    /// </summary>
    static void ExitApp()
    {
        Console.WriteLine("\n感谢使用图书管理系统！再见～");
        Thread.Sleep(800); // 稍作停顿
    }
}
