using System;
namespace UtilityBot.Services
{
    public class SumNums
    {
        public static string SumResult(string sourceText)
        {
            string[] signs = sourceText.Split(' ');
            int[] nums = new int[signs.Length];
            bool numCheck;

            try
            {
                for (int i = 0; i < signs.Length; i++)
                {
                    numCheck = Int32.TryParse(signs[i], out nums[i]);

                    if (numCheck == false)
                        throw new Exception("Только целые числа");
                }

                int sumResult = 0;
                foreach (int num in nums)
                {
                    sumResult += num;
                }
                return sumResult.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
                return e.Message;
            }
        }
    }
}