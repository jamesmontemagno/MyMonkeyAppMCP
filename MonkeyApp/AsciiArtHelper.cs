using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Text;

namespace MonkeyApp
{
    public static class AsciiArtHelper
    {
        private static readonly string ASCII_CHARS = "@%#*+=-:. ";
        private static readonly HttpClient httpClient = new HttpClient();
        
        public static async Task<string> ConvertImageToAsciiAsync(string imageUrl, int width = 80)
        {
            try
            {
                // Download the image
                using var response = await httpClient.GetAsync(imageUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return GetFallbackAsciiArt();
                }

                using var stream = await response.Content.ReadAsStreamAsync();
                using var image = await Image.LoadAsync<Rgba32>(stream);
                
                return ConvertImageToAscii(image, width);
            }
            catch (Exception)
            {
                // If anything goes wrong, return fallback ASCII art
                return GetFallbackAsciiArt();
            }
        }

        private static string ConvertImageToAscii(Image<Rgba32> image, int width)
        {
            // Calculate height to maintain aspect ratio
            int height = (int)(image.Height * width / (double)image.Width * 0.5); // 0.5 factor for character aspect ratio
            
            // Resize the image
            image.Mutate(x => x.Resize(width, height));
            
            var result = new StringBuilder();
            
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Rgba32 pixel = image[x, y];
                    
                    // Calculate brightness (0-255)
                    int brightness = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
                    
                    // Map brightness to ASCII character
                    int charIndex = brightness * (ASCII_CHARS.Length - 1) / 255;
                    result.Append(ASCII_CHARS[charIndex]);
                }
                result.AppendLine();
            }
            
            return result.ToString();
        }
        
        private static string GetFallbackAsciiArt()
        {
            return "  //\\ \n (o o)\n(  V  )\n--m-m--\n";
        }
    }
}