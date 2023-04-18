using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    public class InlineKeyboardController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public InlineKeyboardController(IStorage memoryStorage, ITelegramBotClient telegramBotClient)
        {
            _memoryStorage = memoryStorage;
            _telegramClient = telegramBotClient;
        }

        public async Task Handle(CallbackQuery callbackQuery, CancellationToken ct)
        {
            if (callbackQuery.Data == null)
                return;

            // Обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).SumType = callbackQuery.Data;

            // Информационное сообщение
            string operationType = callbackQuery.Data switch
            {
                "text" => "Вычисление длины текста",
                "nums" => "Сумма чисел",
                _ => String.Empty
            };

            // Уведомление о выборе
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id, $"<b> Тип операции: {operationType}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);

            Console.WriteLine($"Контроллер {GetType().Name} обнаружил нажатие на кнопку");
        }
    }
}