using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BitcoinCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            BitcoinRate currentBitcoin = GetRates();
            Console.WriteLine("Enter the amount of bitcoin");
            float userCoins = float.Parse(Console.ReadLine());
            Console.WriteLine("Calculate in EUR/GBP/USD:");
            string calculationChoice = Console.ReadLine().ToUpper();
            float currentCoinRate = 0;
            string currencyCode = "";
            //Console.WriteLine($"Current rate: {currentBitcoin.bpi.USD.code} {currentBitcoin.bpi.USD.rate_float}");
            Console.WriteLine($"{currentBitcoin.disclaimer}");
            if (calculationChoice == "EUR")
            {
                currentCoinRate = currentBitcoin.bpi.EUR.rate_float;
                currencyCode = currentBitcoin.bpi.EUR.code;
            }
            else if (calculationChoice == "GBP")
            {
                currentCoinRate = currentBitcoin.bpi.GBP.rate_float;
                currencyCode = currentBitcoin.bpi.GBP.code;
            }
            else if (calculationChoice == "USD")
            {
                currentCoinRate = currentBitcoin.bpi.USD.rate_float;
                currencyCode = currentBitcoin.bpi.USD.code;
            }
            float result = userCoins * currentCoinRate;
            Console.WriteLine($"Your bitcoins are {result} {currencyCode} worth");

        }
        public static BitcoinRate GetRates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();
            BitcoinRate bitcoinData;
            using (var responseReader = new StreamReader(webStream))
            {
                var reponse = responseReader.ReadToEnd();
                bitcoinData = JsonConvert.DeserializeObject<BitcoinRate>(reponse);
            }
            return bitcoinData;
        }
    }
}
