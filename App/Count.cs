namespace App;

public static class Count
{
    public static void Counter(FileInfo file)
    {
        if (!file.Exists)
        {
            Console.WriteLine($"Файл не существует");
            return;
        }
        int count = 0;
        using var stream = file.OpenRead();
        using var reader = new StreamReader(stream);
        while (reader.ReadLine() != null) count++;
        Console.Write($"\n{count}");
    }
}