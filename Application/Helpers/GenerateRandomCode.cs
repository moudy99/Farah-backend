﻿namespace Application.Helpers
{
    public static class GenerateRandomCode
    {
        static string code;
        static GenerateRandomCode()
        {
            code = "";
        }

        public static string GetCode()
        {
            Random random = new Random();
            code = random.Next(100000, 1000000).ToString();

            return code;
        }
    }
}
