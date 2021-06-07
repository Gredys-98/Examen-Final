
using Progra1Bot.Clases;
using Progra1Bot.Clases.emojis;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using TelegrambotExamenFinal;

namespace Progra1Bot
{
    class Program
    {
       
        private static TelegramBotClient Bot;

        public static async Task Main()
        {
            await new prueba().IniciarTelegram();
            //await new clsEjemplo2().IniciarTelegram();

        }

    } // fin de la clase
  
}
































































// await new clsEjemplo2().IniciarTelegram();
// await new clsBotAlumnos().IniciarTelegram();

