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
                //new message button
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
                driver.FindElement(By.XPath("//*[@id=\"app\"]/div[1]/div[1]/div[2]/div[1]/span/div[1]/span/div[1]/div[1]/div/label/div/div[2]")).SendKeys(args[0]);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                return false;
            }

            Thread.Sleep(1000);
            if (OpenPersonChat(args))
            {
                return true;
            }

            return false;
        }

        public bool OpenPersonChat(string[] args)
        {
            try
            {                
                driver.FindElement(By.ClassName("_3OvU8")).Click();
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                return false;
            }

            Thread.Sleep(1000);
            for (int i = 0; i < messageCount;)
            {
                Thread.Sleep(1000);
                if (TypeMessage(args[1]))
                {
                    Thread.Sleep(1000);
                    if (ClickSendButton())
                    {
                        i++;
                    }
                    else break;
                }
                else break;
            }


            return true;
        }

        public bool TypeMessage(string message)
        {
            try
            {
                driver.FindElement(By.XPath("//*[@id=\"main\"]/footer/div[1]/div[2]/div/div[1]/div/div[2]")).SendKeys(message);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                return false;
            }

            return true;
        }

        public bool ClickSendButton()
        {
            try
            {
                driver.FindElement(By.XPath("//*[@id=\"main\"]/footer/div[1]/div[2]/div/div[2]")).Click();
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                return false;
            }

            return true;
        }

        public bool SendMessage(string[] args)
        {            
            if (args.Length < 2)
                return false;

            if (args.Length >= 3)
                _messageCount = Convert.ToInt32(args[2]);

            if (CheckLoggedIn())
            {
                if (ClickNewMessage())
                {
                    if (FindPerson(args))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}