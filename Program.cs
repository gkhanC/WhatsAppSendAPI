using System;
using System.Linq;


namespace WhatsAppSendAPI
{
    class Program
    {
        private Core _core = new Core();
        public Core core
        => _core;

        public void Run()
        {
            core.StartWhatsApp();

            while (true)
            {
                System.Console.WriteLine("WhatsApp web client'a giriş yap ve ENTER'a bas.");
                var s = System.Console.ReadLine();
                if (s.ToLower() == "exit")
                {
                    core.driver.Quit();
                    Environment.Exit(-1);
                }
                if (core.CheckLoggedIn())
                {
                    break;
                }
            }


        mesajgonder:

            System.Console.WriteLine("Mesaj göndermek istediğiniz kişinin ismini ve mesajınızı aralarında boşluk bırakarak gir ve ENTER'A bas.");
            var data = System.Console.ReadLine();
            if (data.ToLower() == "exit")
            {
                Environment.Exit(-1);
            }
            var regularData = data.Split(" ");

            if (core.SendMessage(regularData))
                System.Console.WriteLine("Gönderim başarılı");
            else System.Console.WriteLine("Gönderim başarısız");

            goto mesajgonder;


        }

        static void Main(string[] args)
        {
            System.Console.WriteLine("Komut bekleniyor:");

            while (true)
            {
                var command = System.Console.ReadLine();
                if (command.ToLower() == "start")
                {
                    break;
                }
                else if (command.ToLower() == "exit")
                {                    
                    Environment.Exit(-1);
                }

            }

            new Program().Run();

        }
    }
}
