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
    var semaphore = new SemaphoreSlim(1, 1);
    
    try
    {
        await Parallel.ForEachAsync(urls, cts.Token, async (url, ct) =>
        {
            bool isSemaphore = false;
            try
            {
                await using var content = await http.GetStreamAsync(url, ct);
                await semaphore.WaitAsync(ct);
                isSemaphore = true;
                await content.CopyToAsync(destStream, ct);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (isSemaphore) semaphore.Release();
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
