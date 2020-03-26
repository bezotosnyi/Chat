namespace Chat.Service.Front.Console
{
    using System;
    using System.Configuration;
    using System.ServiceModel;

    using Autofac;
    using Autofac.Core;
    using Autofac.Integration.Wcf;

    using Chat.BLL.Infrastructure;
    using Chat.Service.Front.Console.Configuration;

    public class Program
    {
        public static void Main(string[] args)
        {
            AutoMapperConfig.Configure();
            using (var container = AutofacConfig.Configure())
            {
                var loggingService = container.Resolve<ILoggingService>();

                var uri = new Uri(ConfigurationManager.AppSettings["uri"]);

                using (var host = new ServiceHost(typeof(ChatService), uri))
                {
                    try
                    {
                        host.AddDependencyInjectionBehavior<IChatService>(container);
                        host.Open();

                        var message = $"Сервер {uri} запущен.";
                        loggingService.Info(message);
                        Console.WriteLine(message);

                        Console.WriteLine("Нажмите любую кнопку для выхода...");
                        Console.ReadKey();

                        host.Close();
                        loggingService.Info($"Сервис {uri} остановлен.");
                    }
                    catch (Exception exception)
                    {
                        var message = $"Возникла следующая исключительная ситуация: '{exception.Message}'. Стек вызова: '{exception.StackTrace}'.";
                        loggingService.Error(message);
                        Console.WriteLine(message);

                        Console.WriteLine("Нажмите любую кнопку для выхода...");
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}