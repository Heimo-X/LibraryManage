using System; // 确保引入 System 命名空间

namespace LibraryManage.Src.Modules;

/// <summary>
/// 表示图书馆中的一本书籍。
/// </summary>
public class Book
{
    /// <summary>
    /// 获取或设置书籍的唯一标识符。
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// 获取或设置书籍的标题。
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// 获取或设置书籍的作者。
    /// </summary>
    public required string Author { get; set; }

    /// <summary>
    /// 返回表示当前书籍的字符串。
    /// </summary>
    /// <returns>格式为 "[ID] 《书名》|作者" 的字符串。</returns>
    public override string ToString() => $"[{Id}] 《{Title}》|{Author}";

    /// <summary>
    /// 确定指定的对象是否等于当前对象。
    /// 比较基于书籍的标题和作者（不区分大小写）。
    /// </summary>
    /// <param name="obj">要与当前对象进行比较的对象。</param>
    /// <returns>如果指定对象等于当前对象，则为 true；否则为 false。</returns>
    public override bool Equals(object? obj)
    {
        // 如果对象为空或者不是 Book 类型，则不相等
        if (obj is not Book other) return false;
        // 比较标题和作者，不区分大小写
        return Title.Equals(other.Title, StringComparison.OrdinalIgnoreCase) && 
               Author.Equals(other.Author, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 作为默认哈希函数。
    /// 返回基于书籍标题和作者的哈希代码。
    /// </summary>
    /// <returns>当前对象的哈希代码。</returns>
    public override int GetHashCode()
    {
        // 使用 StringComparer.OrdinalIgnoreCase 的 GetHashCode 来处理不区分大小写的哈希码
        return HashCode.Combine(
            StringComparer.OrdinalIgnoreCase.GetHashCode(Title), 
            StringComparer.OrdinalIgnoreCase.GetHashCode(Author)
        );
    }
}