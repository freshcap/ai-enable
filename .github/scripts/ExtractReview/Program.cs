using System.Text.Json;

// Read the API response
var responseJson = File.ReadAllText("review-response.json");
var doc = JsonDocument.Parse(responseJson);
var root = doc.RootElement;

string reviewText;

// Check if API returned an error
if (root.TryGetProperty("error", out var error))
{
    var errorMsg = error.TryGetProperty("message", out var msg)
        ? msg.GetString()
        : "Unknown error";
    
    reviewText = $@"⚠️ **AI Review Error**

Could not generate review: {errorMsg}

This might be due to:
- Invalid API key
- Rate limiting
- Network issues
- Service unavailability

Please check the GitHub Actions logs for more details.";
    
    Console.WriteLine($"❌ Error: {errorMsg}");
}
else
{
    // Extract the review text from content array
    reviewText = root
        .GetProperty("content")[0]
        .GetProperty("text")
        .GetString() ?? string.Empty;
    
    Console.WriteLine("✅ Review extracted successfully");
}

// Save the review text for the next step
File.WriteAllText("review.txt", reviewText);

Console.WriteLine($"Review length: {reviewText.Length} characters");
