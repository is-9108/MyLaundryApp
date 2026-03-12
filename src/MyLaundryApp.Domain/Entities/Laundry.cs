namespace MyLaundryApp.Domain.Entities;

public class Laundry
{
    public bool IsRain { get; set; }
    public string Message { get; set; } = string.Empty;

    public Laundry(bool isRain)
    {
        Message = isRain ? "洗濯物は室内に干しましょう。" : "洗濯物は外に干しても大丈夫です。";
    }
}
