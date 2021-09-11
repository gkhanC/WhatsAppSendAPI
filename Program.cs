using System.Net.Mime;
using System.Diagnostics;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WhatsAppSendAPI
{
    class Program
    {
        IWebDriver driver;

        private bool CheckLoggedIn()
        {
            try
            {
                return driver.FindElement(By.CssSelector("_2Uo0Z")).Displayed;
            }
            catch (System.Exception e)
            {
                Debug.Print(e.Message);
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private void SendMessage(string number, string message)
        {
            //Wait for maximum of 10 second if any element is not found
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.Navigate().GoToUrl(
                                        "https://api.whatsapp.com/send?phone=" +
                                        number +
                                        "&text=" +
                                        Uri.EscapeDataString(message)
                                     );

            driver.FindElement(By.Id("action-button")).Click(); // send button
            driver.FindElement(By.CssSelector("button._2lkdt>span")).Click(); // send arrow button
        }

        public void Run()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://web.whatsapp.com");

            while (true)
            {
                Console.WriteLine("Login to WhatsApp Web and Press Enter");
                var c = Console.ReadLine();
                if (c.ToLower() == "q")
                {
                    Environment.Exit(-1);
                }
                else
                    break;
            }

            Console.WriteLine("Enter the phone number without leading zeros and Press Enter");
            var number = Console.ReadLine();

            Console.WriteLine("Enter your message and Press Enter");
            var message = Console.ReadLine();

            SendMessage(number, message);

        }

        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}
