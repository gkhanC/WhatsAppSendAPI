using System.Threading;
namespace WhatsAppSendAPI
{
    using System;
    using System.Threading;
    using System.Diagnostics;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public class Core
    {
        private IWebDriver _driver;
        private int _messageCount = 1;

        public IWebDriver driver => _driver;
        public int messageCount => _messageCount;

        public Core()
        {
            _driver = new ChromeDriver();
        }

        public void StartWhatsApp()
        => driver.Navigate().GoToUrl("https://web.whatsapp.com");

        public bool CheckLoggedIn()
        {
            try
            {
                System.Console.WriteLine("Yeni msj butonu algılandı.");
                return driver.FindElement(By.XPath("//*[@id=\"side\"]/header/div[2]/div/span/div[2]/div")).Displayed;
            }
            catch (System.Exception e)
            {
                Debug.Print(e.Message);
                return false;
            }
        }

        public bool ClickNewMessage()
        {
            try
            {
                System.Console.WriteLine("Yeni mesaj oluştur.");
                driver.FindElement(By.XPath("//*[@id=\"side\"]/header/div[2]/div/span/div[2]/div")).Click();
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                return false;
            }

            return true;
        }

        public bool FindPerson(string[] args)
        {
            try
            {
                System.Console.WriteLine("Kişiyi bul.");
                driver.FindElement(By.XPath("//*[@id=\"app\"]/div[1]/div[1]/div[2]/div[1]/span/div[1]/span/div[1]/div[1]/div/label/div/div[2]")).SendKeys(args[0]);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                return false;
            }


            return true;
        }

        public bool OpenPersonChat(string[] args)
        {
            try
            {
                System.Console.WriteLine("Kişi Sohbetini aç.");
                driver.FindElement(By.ClassName("_3OvU8")).Click();
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                return false;
            }


            return true;
        }

        public bool TypeMessage(string message)
        {
            try
            {
                System.Console.WriteLine("Mesajı gir.");
                driver.FindElement(By.XPath("//*[@id=\"main\"]/footer/div[1]/div/div/div[2]/div[1]/div/div[2]")).SendKeys(message);
            }
            catch (Exception e)
            {
               System.Console.WriteLine((e.Message));
                return false;
            }

            return true;
        }

        public bool ClickSendButton()
        {
            try
            {
                System.Console.WriteLine("Gönder tuşuna bas.");
                driver.FindElement(By.XPath("//*[@id=\"main\"]/footer/div[1]/div/div/div[2]/div[2]/button/span")).Click();
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                return false;
            }

            return true;
        }

        public bool SendMessage(string[] args, string msg)
        {
            if (args.Length < 1)
                return false;

            if (args.Length >= 2)
                _messageCount = Convert.ToInt32(args[1]);

            if (CheckLoggedIn())
            {
                if (ClickNewMessage())
                {
                    if (FindPerson(args))
                    {
                        Thread.Sleep(1000);
                        if (OpenPersonChat(args))
                        {
                            Thread.Sleep(500);
                            for (int i = 0; i < messageCount;)
                            {
                                Thread.Sleep(500);
                                if (TypeMessage(msg))
                                {
                                    Thread.Sleep(250);
                                    if (ClickSendButton())
                                    {
                                        i++;
                                    }
                                    else return false;
                                }
                                else return false;
                            }
                            return true;
                        }
                        return false;
                    }
                }
            }

            return false;
        }

    }
}