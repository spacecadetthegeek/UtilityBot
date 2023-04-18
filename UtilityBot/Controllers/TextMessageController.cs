using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using UtilityBot.Models;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public TextMessageController(IStorage memoryStorage, ITelegramBotClient botClient)
        {
            _telegramClient = botClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                    InlineKeyboardButton.WithCallbackData($"Вычислить длину текста", $"text"),
                    InlineKeyboardButton.WithCallbackData($"Вычислить сумму чисел", $"nums")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b> Бот может:\n1) Вычислить длину текста" +
                    $"\n2) Суммировать числа</b> {Environment.NewLine}",
                            cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default:
                    if (_memoryStorage.GetSession(message.Chat.Id).SumType == "nums")
                    {
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"{SumNums.SumResult(message.Text)}",
                            cancellationToken: ct);
                    }
                    else
                    {
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                            $"Длина сообщения: {TextLength.CalculateTextLength(message.Text)}", cancellationToken: ct);
                    }
                    break;
            }
        }
    }
}