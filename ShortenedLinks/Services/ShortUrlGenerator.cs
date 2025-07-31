namespace ShortenedLinks.Services
{
    public static class ShortUrlGenerator
    {
        private const string AllowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static Random _random = new Random();

        public static string Generate(int lenght = 6)
        {
            var result = new char[lenght];
            for (int i = 0; i < lenght; i++)
            {
                result[i] = AllowedChars[_random.Next(AllowedChars.Length)];
            }
            return new string(result);
        }
    }
}
