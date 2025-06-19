using LibraryManage.Src.Modules;
using System; // 确保引入 System 命名空间

namespace LibraryManage;

class Program
{
    static void Main(string[] args)
    {
        // 使用 AppConstant 定义的文件路径初始化 BookManager
        BookManager manager = new BookManager(AppConstant.FilePath);

        while (true)
        {
            Console.Clear(); // 清空控制台
            System.Console.WriteLine("""
                1.增添书目
                2.删减书目
                3.展示书目
                4.搜索书目
                5.退出
                """);

            System.Console.Write("请输入你的操作选择（输入序号）：");
            string? choice = Console.ReadLine(); // 使用可空引用类型

            switch (choice)
            {
                case "1":
                    manager.AddBook(); // 调用添加书籍方法
                    break;
                case "2":
                    manager.RemoveBook(); // 调用删除书籍方法
                    break;
                case "3":
                    manager.DisplayBooks(); // 调用展示书籍方法
                    break;
                case "4":
                    manager.SearchBooks(); // 调用搜索书籍方法
                    break;
                case "5":
                    return; // 退出程序
                default:
                    System.Console.Write("\n无效指令!请重新输入……");
                    Console.ReadKey(); // 等待用户按键继续
                    break;
            }
        }
    }
}