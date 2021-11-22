namespace Blok
{
    internal static class Utils
    {
        public static string zeros(int number)
        {
            string s = "";
            for (int i = 0; i < number; i++)
            {
                s += "0";
            }

            return s;
        }
    }
}