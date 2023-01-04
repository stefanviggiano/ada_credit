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

        public static string GenerateNewAccountNumber(DatabaseClient databaseClient)
        {
            string accountNumber;
            while (true)
            {
                accountNumber = RandomString(6, "0123456789");
                if (databaseClient.Clients.FirstOrDefault(
                        x => x.AccountNumber == accountNumber) == null)
                    break;
            }

            return accountNumber;
        }
    }
}
