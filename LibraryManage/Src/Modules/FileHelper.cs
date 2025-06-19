using Newtonsoft.Json;
using System.Collections.Generic; // 确保引入 System.Collections.Generic
using System.IO;                  // 确保引入 System.IO
using System;                     // 确保引入 System 命名空间

namespace LibraryManage.Src.Modules;

/// <summary>
/// 提供文件操作的帮助方法，用于保存和加载书籍数据。
/// </summary>
public class FileHelper
{
    /// <summary>
    /// 将书籍列表序列化为 JSON 格式并保存到指定文件。
    /// </summary>
    /// <param name="path">文件的路径。</param>
    /// <param name="books">要保存的书籍列表。</param>
    public static void SaveToFile(string path, List<Book> books)
    {
        try
        {
            // 将书籍列表序列化为美化过的 JSON 字符串
            string json = JsonConvert.SerializeObject(books, Formatting.Indented);
            // 将 JSON 字符串写入文件
            File.WriteAllText(path, json);
        }
        catch (IOException ex)
        {
            // 捕获文件操作相关的异常
            System.Console.WriteLine($"保存失败：文件操作错误！详细信息：{ex.Message}");
            // TODO: 在实际应用中，这里应记录到日志系统，而不是直接输出到控制台
            // 例如：Logger.LogError($"文件保存失败：{ex.Message}", ex);
        }
        catch (JsonException ex)
        {
            // 捕获 JSON 序列化相关的异常
            System.Console.WriteLine($"保存失败：JSON 序列化错误！详细信息：{ex.Message}");
            // TODO: 记录日志
        }
        catch (UnauthorizedAccessException ex)
        {
            // 捕获权限不足的异常
            System.Console.WriteLine($"保存失败：没有足够的权限访问文件！详细信息：{ex.Message}");
            // TODO: 记录日志
        }
        catch (Exception ex)
        {
            // 捕获其他未预料的异常
            System.Console.WriteLine($"保存失败：发生未知错误！详细信息：{ex.Message}");
            // TODO: 记录日志
        }
    }

    /// <summary>
    /// 从指定文件加载 JSON 格式的书籍数据并反序列化为书籍列表。
    /// </summary>
    /// <param name="path">文件的路径。</param>
    /// <returns>加载的书籍列表；如果文件不存在、读取失败或反序列化失败，则返回空列表。</returns>
    public static List<Book> LoadFromFile(string path)
    {
        // 如果文件不存在，直接返回空列表
        if (!File.Exists(path)) return new List<Book>(); // 保持返回 List<Book> 而不是 [] 语法，兼容性更好

        try
        {
            // 读取文件所有文本内容
            string jsonFromFile = File.ReadAllText(path);
            // 将 JSON 字符串反序列化为书籍列表
            var books = JsonConvert.DeserializeObject<List<Book>>(jsonFromFile);
            // 如果反序列化结果为空，返回空列表，否则返回反序列化后的列表
            return books ?? new List<Book>();
        }
        catch (IOException ex)
        {
            // 捕获文件操作相关的异常
            System.Console.WriteLine($"读取失败：文件操作错误！详细信息：{ex.Message}");
            // TODO: 记录日志
            return new List<Book>();
        }
        catch (JsonException ex)
        {
            // 捕获 JSON 反序列化相关的异常
            System.Console.WriteLine($"读取失败：JSON 反序列化错误！请检查文件内容是否正确。详细信息：{ex.Message}");
            // TODO: 记录日志
            return new List<Book>();
        }
        catch (UnauthorizedAccessException ex)
        {
            // 捕获权限不足的异常
            System.Console.WriteLine($"读取失败：没有足够的权限访问文件！详细信息：{ex.Message}");
            // TODO: 记录日志
            return new List<Book>();
        }
        catch (Exception ex)
        {
            // 捕获其他未预料的异常
            System.Console.WriteLine($"读取失败：发生未知错误！详细信息：{ex.Message}");
            // TODO: 记录日志
            return new List<Book>();
        }
    }
}