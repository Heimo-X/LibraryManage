namespace LibraryManage.Modules;

public class Book
{
    public required string Title { get; set; }
    public required string Author { get; set; }

    public override string ToString() => $"《{Title}》 by {Author}";

    public override bool Equals(object? obj)
    {
        if (obj is not Book other) return false;
        return Title == other.Title && Author == other.Author;
    }

    public override int GetHashCode() => HashCode.Combine(Title, Author);
}

