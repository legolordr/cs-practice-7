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
        var valid = false;
        while (valid is false)
        {
            Console.Write("Введите нужные URL через пробел: ");
            result = Console.ReadLine()!.Split(' ');
            
            valid = result.All(x => IsValidUrl(x));
            if (valid is false)
            {
                Console.WriteLine("Ошибка! Вы ввели некорректные URL!");
            }
        }
        return result;
    }

    private static bool IsValidUrl(string url)
    {
        bool isUri = Uri.TryCreate(url, UriKind.Absolute, out var uriResult);
        bool isHttp = uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
        return isUri && isHttp;
    }
    
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
                        "Файл уже существует, вы хотите его перезаписать? (y/n)");
                    char number = Console.ReadKey().KeyChar;
                    switch (number)
                    {
                        case 'y':
                            return file;
                        case 'n':
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