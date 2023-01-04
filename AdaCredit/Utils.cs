namespace AdaCredit
{
    public static class Utils
    {
        public static string RandomString(int length, string chars)
        {
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
