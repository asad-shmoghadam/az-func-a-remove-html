#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log) {

   log.LogInformation("HttpWebhook triggered");

   // Parse query parameter
   string emailBodyContent = await new StreamReader(req.Body).ReadToEndAsync();

   // Replace HTML with other characters
   string updatedBody = Regex.Replace(emailBodyContent, "<.*?>", string.Empty);
   updatedBody = updatedBody.Replace("\\r\\n", " ");
   updatedBody = updatedBody.Replace(@"&nbsp;", " ");

   // Return cleaned text
   return (ActionResult)new OkObjectResult(new { updatedBody });
}