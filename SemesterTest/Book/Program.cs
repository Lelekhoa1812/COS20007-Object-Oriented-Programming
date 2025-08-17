using System;
using Microsoft.VisualBasic;

public class Book
{
    public string title { get; set; }
    public string author { get; set; }
    public int establishmentDate { get; set; }
    public int bookType { get; set; }


    public Book(string _title, string _author, int _dateTime, int _type)
    {
        title = _title;
        author = _author;
        establishmentDate = _dateTime;
        bookType = _type;
    }

    public override string ToString()
    {
        return $"Title:{title}<br>Type:{bookType}<br>Publication Date: { establishmentDate}";
    }

    public void OpenBook()
    {
    }

    public void CloseBook()
    {
    }

    public void ReadBook()
    {
    }

    public void LastOpennedPage()
    {
    }

    public void AddComment()
    {
    }
}
