﻿using System.Threading.Tasks;
using Watchman.Cqrs;
using Watchman.Integrations.MongoDB;

namespace Watchman.DomainModel.Users.Commands.Handlers
{
    public class MarkMuteEventAsUnmutedCommandHandler : ICommandHandler<MarkMuteEventAsUnmutedCommand>
    {
        private readonly ISessionFactory _sessionFactory;

        public MarkMuteEventAsUnmutedCommandHandler(ISessionFactory sessionFactory)
        {
            this._sessionFactory = sessionFactory;
        }

        public async Task HandleAsync(MarkMuteEventAsUnmutedCommand command)
        {
            using var session = _sessionFactory.Create();
            var muteEvent = session.Get<MuteEvent>(command.MuteEventGuid);
            muteEvent.Unmuted = true;
            await session.UpdateAsync(muteEvent);
        }
    }
}
