namespace App;

public static class Count
{
    public static void Counter(FileInfo file)
    {
        try
        {
            int count = 0;
            using var fileStream = file.OpenRead();
            using var reader = new StreamReader(fileStream);
            while (reader.ReadLine() != null) count++;
            Console.Write($"\n{count}");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл не найден");
        }
    }
}