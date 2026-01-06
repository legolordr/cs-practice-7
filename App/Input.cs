namespace App;

/// <summary>
/// Считывает и валидирует ввод пользователя.
/// </summary>
public static class Input
{
    /// <summary>
    /// Считывает от пользователя URL файлов из интернета.
    /// </summary>
    public static string[] GetUrls()
    {
        string[] result = [];
        var valid = true;
        while (valid)
        {
            Console.Write("Введите нужные URL через пробел: ");
            result = Console.ReadLine()!.Split(' ');
            
            valid = result.Any(x => IsValidUrl(x) is false);
            if (valid)
            {
                Console.WriteLine("Ошибка! Вы ввели некорректные URL!");
            }
        }
        return result;
    }

    private static bool IsValidUrl(string url) => url.StartsWith("https://");
    
    
    /// <summary>
    /// Считывает от пользователя путь до файла с результатом.
    /// </summary>
    public static FileInfo GetOutputFile()
    {
        while (true)
        {
            Console.WriteLine("\nВведите путь до файла с результатом: ");
            try
            {
                var file = new FileInfo(Console.ReadLine()!);
                if (file.Exists)
                {
                    Console.Write(
                        "Файл уже существует, вы хотите его перезаписать?\n 1. Да\n 2. Нет\n Нажмите соответствующую цифру");
                    char number = Console.ReadKey().KeyChar;
                    switch (number)
                    {
                        case '1':
                            return file;
                        case '2':
                            continue;
                        default:
                            throw new ArgumentException("\nНажата некорректная клавиша.");
                    }
                }

                return file;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch
            {
                Console.WriteLine("Произошла ошибка, вероятно вы ввели некорректный путь. Попробуйте ещё раз.");
            }
        }
    }
}