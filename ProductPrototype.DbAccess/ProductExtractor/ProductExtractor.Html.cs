using File = System.IO.File;

namespace ProductPrototype.DbAccess;

public partial class HtmlProductExtractor
{
    private string ReadHtmlFromFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        return File.ReadAllText(filePath);
    }

    private async Task<string> GetHtmlFromUrlAsync(string url)
    {
        // 1. Create an instance of HttpClient
        using (var client = new HttpClient())
        {
            try
            {
                // 2. Make an asynchronous GET request to the URL
                // GetStringAsync handles the entire process: making the request, 
                // waiting for the response, and reading the response content as a string.
                string html = await client.GetStringAsync(url);

                // 3. Return the HTML content
                return html;
            }
            catch (HttpRequestException e)
            {
                // Handle potential errors, such as a bad URL or network issues
                Console.WriteLine($"\nRequest Exception Caught!");
                Console.WriteLine($"Message: {e.Message}");
                return null;
            }
        }
    }
}
