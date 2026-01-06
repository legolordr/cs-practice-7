using App;

var urls = Input.GetUrls();
var dest = Input.GetOutputFile();
bool deletefile = false;

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    cts.Cancel();
    deletefile = true;
    e.Cancel = true;
};

{
    await using var destStream = dest.OpenWrite();
    using var http = new HttpClient();
    
    try
    {
        await Parallel.ForEachAsync(urls, cts.Token, async (url, ct) =>
        {
            try
            {
                await using var content = await http.GetStreamAsync(url, ct);
                await content.CopyToAsync(destStream, ct);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        });
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("Canceled");
    }
}


dest.Refresh();

if (deletefile && dest.Exists)
{
    dest.Delete();
}
else if (dest.Exists)
{
    Count.Counter(dest);
}
