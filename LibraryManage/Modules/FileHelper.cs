using Newtonsoft.Json;

namespace LibraryManage.Modules;

public class FileHelper
{
    public static void SaveToFile(string path, List<Book> books)
    {
        try
        {
            string json = JsonConvert.SerializeObject(books, Formatting.Indented);
            File.WriteAllText(path, json);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("保存失败!");
            System.Console.WriteLine($"详细信息：{ex.Message}");
        }
    }

    public static List<Book> LoadFromFile(string path)
    {
        if (!File.Exists(path)) return [];

        try
        {
            string jsonFromFile = File.ReadAllText(path);
            var books = JsonConvert.DeserializeObject<List<Book>>(jsonFromFile);
            return books ?? [];
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"读取失败!错误信息:{ex.Message}");
            return [];
        }
    }
}