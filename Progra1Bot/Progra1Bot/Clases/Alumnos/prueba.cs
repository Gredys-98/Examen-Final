using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegrambotExamenFinal
{
    public class prueba
    {
        //Key del bot
        private static readonly TelegramBotClient Bot = new TelegramBotClient("1618007715:AAFEjq6_HRXhrOc9vaQajmpN1-a-t97D0bE");

        static void Main(string[] args)
        {
            //Método que se ejecuta cuando se recibe un mensaje
            Bot.OnMessage += BotOnMessageReceived;

            //Método que se ejecuta cuando se recibe un callbackQuery
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;

            //Método que se ejecuta cuando se recibe un error
            Bot.OnReceiveError += BotOnReceiveError;

            //Inicia el bot
            Bot.StartReceiving();
            Console.WriteLine("Bot levantado!");
            Console.ReadLine();
            Bot.StopReceiving();
        }

        internal Task IniciarTelegram()
        {
            throw new NotImplementedException();
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.Text) return;

            switch (message.Text.Split(' ').First())
            {
                //Enviar un inline keyboard con callback
                case "/Vehiculos":

                    //Simula que el bot está escribiendo
                    await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                    await Task.Delay(50);

                    var keyboardEjemplo1 = new InlineKeyboardMarkup(new[]
                    {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(
                            text:"Accesorios",
                            callbackData: "Accesorios"),
                       
                    },
                    new []
                    {
                        
                        InlineKeyboardButton.WithCallbackData(
                            text:"Contacto",
                            callbackData: "contacto"),
                    },
                    new []
                    {
                        
                        InlineKeyboardButton.WithCallbackData(
                            text: "Repuestos",
                            callbackData: "repuestos"),
                    }

                     });

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Elija una opción",
                        replyMarkup: keyboardEjemplo1);
                    break;

                case "/Motocicletas":

                    var keyboardEjemplo2 = new InlineKeyboardMarkup(new[]
                    {
                   
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(
                            text:"Accesorios",
                            callbackData: "formato"),
                        InlineKeyboardButton.WithCallbackData(
                            text:"Repuestos",
                            callbackData: "video"),
                    }
                });

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Elija una opción",
                        replyMarkup: keyboardEjemplo2);
                    break;

                //Mensaje por default
                default:
                    const string usage = @"
Bienvenidos a la tienda de accesorios y repuestos sera un gusto atenderte...🔧
Elige una de las dos opciones que Buscas:😎

                Comandos:
                /Vehiculos  - accesorios y Repuestos
                /Motocicletas- Accesorio y Repuestos";

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        text: usage,
                        replyMarkup: new ReplyKeyboardRemove());

                    break;
            }
        }

        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;

            switch (callbackQuery.Data)
            {
                case "keyboard":
                    ReplyKeyboardMarkup tipoContacto = new[]
                    {
                        new[] { "Opción 1", "Opción 2" },
                        new[] { "Opción 3", "Opción 4" },
                    };

                    await Bot.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: "Keyboard personalizado",
                        replyMarkup: tipoContacto);
                    break;

               

                case "ACESORIOSV": // accesorios 


                    SqlConnection conexione = new SqlConnection("Data Source = DESKTOP-03L4M4P\\SQLEXPRESS; Initial Catalog = Accesorios; Integrated Security = True");
                    SqlCommand comandos = new SqlCommand("Select into TbVehiculos values(2,'Pieza de motro',500");
                    comandos.Connection = conexione;
                    conexione.Open();


                    comandos.ExecuteReader();
                    conexione.Close();

                    await Bot.SendTextMessageAsync(
                       chatId: callbackQuery.Message.Chat.Id,

                       text: "Mensaje cargado a la base");
                    break;

                case "REPUESTOS":
                    await Bot.SendAnimationAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        animation: "https://motoshopvrc.com/"
                        );
                    break;

                case "ACCESORIOSM": // fuera
                    await Bot.SendVideoAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        video: "https://www.youtube.com/watch?v=Ca1m_r1lX1Q"
                        );
                    break;

                case "Listado de repuestos":

                    SqlConnection conexion = new SqlConnection("Data Source = DESKTOP-03L4M4P\\SQLEXPRESS; Initial Catalog = Accesorios; Integrated Security = True");
                    SqlCommand comando = new SqlCommand("Insert into TbVehiculos values(2,'Pieza de motro',500");
                    comando.Connection = conexion;
                    conexion.Open();

                    
                    comando.ExecuteReader();
                    conexion.Close();

                    await Bot.SendTextMessageAsync(
                       chatId: callbackQuery.Message.Chat.Id,

                       text: "Mensaje cargado a la base");


                    break;


                case "formato":
                    await Bot.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: "<b>bold</b>, <strong>bold</strong>",
                        parseMode: ParseMode.Html
                        );
                    await Bot.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: "<i>italic</i>, <em>italic</em>",
                        parseMode: ParseMode.Html
                        );
                    await Bot.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: "<a href='https://motoshopvrc.com/'>inline URL</a>",
                        parseMode: ParseMode.Html
                        );
                    break;

                case "reply":
                    await Bot.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: "ID: " + callbackQuery.Message.MessageId + " - " + callbackQuery.Message.Text,
                        replyToMessageId: callbackQuery.Message.MessageId);
                    break;

                case "contacto":
                    await Bot.SendContactAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        phoneNumber: "+502 42357805",
                        firstName: "Gredy",
                        lastName: "Carrillo"
                        );
                    break;
                



                case "forceReply":
                    await Bot.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: "Forzar respuesta a este mensaje",
                        replyMarkup: new ForceReplyMarkup());
                    break;

                case "reenviar":
                    await Bot.ForwardMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        fromChatId: callbackQuery.Message.Chat.Id,
                        messageId: callbackQuery.Message.MessageId
                        );
                    break;
            }
            
        }

        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Console.WriteLine("Received error: {0} — {1}",
                receiveErrorEventArgs.ApiRequestException.ErrorCode,
                receiveErrorEventArgs.ApiRequestException.Message);
        }
    }
}