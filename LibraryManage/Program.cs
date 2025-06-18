using System.Runtime.InteropServices;
using LibraryManage.Modules;

namespace LibraryManage;

class Program
{
    static void Main(string[] args)
    {
        BookManager manager = new BookManager(AppConstant.FilePath);
        while (true)
        {
            Console.Clear();
            System.Console.WriteLine("""
                1.增添书目
                2.删减书目
                3.展示书目
                4.退出
            """);

            System.Console.Write("请输入你的操作选择（输入序号）：");
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1": manager.AddBook(); break;
                case "2": manager.RemoveBook(); break;
                case "3": manager.DisplayBooks(); break;
                case "4": return;
                default:
                    System.Console.WriteLine("无效指令!请重新输入!");
                    Console.ReadKey(); break;
            }
        }
    }
}