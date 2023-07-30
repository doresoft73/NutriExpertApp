using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using System.Text;
using System.IO;
using System.Text.Json;
using Azure.AI.Vision.Common.Input;
using Azure.AI.Vision.Common.Options;
using Azure.AI.Vision.ImageAnalysis;
using Azure;
using Azure.AI.OpenAI;
using static System.Net.Mime.MediaTypeNames;
using OpenAI.Net.Models.Responses;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace NutriApp.Pages
{
    public class ImageOCR
    {
        // A base URL for the Azure Computer Vision API endpoint
        public const string AzureVisionUrl = "https://imageanalyzer.cognitiveservices.azure.com/";

        // An authorization header value for the Azure Computer Vision API (replace with your own subscription key)
        public const string AzureVisionKey = "769c3d1879cc47b5b9dcc12699bfa728";

        /*
         * AUTHENTICATE
         * Creates a Computer Vision client used by each example.
         */
        public static ComputerVisionClient Authenticate(string endpoint = AzureVisionUrl, string key = AzureVisionKey)
        {
            ComputerVisionClient client =
              new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
              { Endpoint = endpoint };
            return client;
        }

        // A method to get the text from an image using the Azure Computer Vision API
        public static string AnalyzeImage(string file)
        {
            var serviceOptions = new VisionServiceOptions(AzureVisionUrl, new AzureKeyCredential(AzureVisionKey));

            // Specify the image file on disk to analyze. sample.jpg is a good example to show most features
            using var imageSource = VisionSource.FromFile(file);

            var analysisOptions = new ImageAnalysisOptions()
            {
                Features =
                      ImageAnalysisFeature.Caption
                    | ImageAnalysisFeature.Objects
                    | ImageAnalysisFeature.Text,

                // Optional. Default is "en" for English. See https://aka.ms/cv-languages for a list of supported
                // language codes and which visual features are supported for each language.
                Language = "en",

                // Optional. Default is "latest".
                ModelVersion = "latest",

                // Optional, and only relevant when you select ImageAnalysisFeature.Caption.
                // Set this to "true" to get a gender neutral caption (the default is "false").
                GenderNeutralCaption = true
            };

            using var analyzer = new ImageAnalyzer(serviceOptions, imageSource, analysisOptions);

            // This call creates the network connection and blocks until Image Analysis results
            // return (or an error occurred). Note that there is also an asynchronous (non-blocking)
            // version of this method: analyzer.AnalyzeAsync().
            var result = analyzer.Analyze();

            // Create a string builder to store the analysis results
            var sb = new StringBuilder();

            if (result.Reason == ImageAnalysisResultReason.Analyzed)
            {
                if (result.Objects != null)
                {
                    sb.AppendLine("Objects:");
                    foreach (var detectedObject in result.Objects)
                    {
                        sb.AppendLine($" Object in picture: \"{detectedObject.Name}\"");
                    }
                }
                if (result.Text != null)
                {
                    sb.AppendLine("Nutritional Information:");
                    foreach (var line in result.Text.Lines)
                    {
                        sb.AppendLine($"{line.Content}");

                        // Save the line.Content to a file
                        File.AppendAllText("text_data.txt", line.Content + Environment.NewLine);
                    }
                }
            }
            else // result.Reason == ImageAnalysisResultReason.Error
            {
                var errorDetails = ImageAnalysisErrorDetails.FromResult(result);
                sb.AppendLine("Analysis failed.");
                sb.AppendLine($"  Error reason : {errorDetails.Reason}");
                sb.AppendLine($"  Error code : {errorDetails.ErrorCode}");
                sb.AppendLine($"  Error message: {errorDetails.Message}");
            }

            // Return the string representation of the analysis results
            return sb.ToString();
        }
    }
}
