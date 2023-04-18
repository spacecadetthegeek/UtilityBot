using UtilityBot.Models;
using System.Collections.Concurrent;

namespace UtilityBot.Services
{
    public class MemoryStorage : IStorage
    {
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        public Session GetSession(long chatID)
        {
            if (_sessions.ContainsKey(chatID))
                return _sessions[chatID];

            var newSession = new Session() { SumType = "text" };
            _sessions.TryAdd(chatID, newSession);
            return newSession;
        }
    }
}