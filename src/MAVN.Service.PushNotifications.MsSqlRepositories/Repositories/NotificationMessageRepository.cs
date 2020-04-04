using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Common.MsSql;
using MAVN.Service.PushNotifications.Domain.Contracts;
using MAVN.Service.PushNotifications.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using NotificationMessage = Lykke.Service.PushNotifications.MsSqlRepositories.Entities.NotificationMessage;

namespace MAVN.Service.PushNotifications.MsSqlRepositories.Repositories
{
    public class NotificationMessageRepository : INotificationMessageRepository
    {
        private readonly MsSqlContextFactory<DatabaseContext> _msSqlContextFactory;
        private readonly IMapper _mapper;
        private readonly ILog _log;

        public NotificationMessageRepository(MsSqlContextFactory<DatabaseContext> msSqlContextFactory, IMapper mapper,
            ILogFactory logFactory)
        {
            _msSqlContextFactory = msSqlContextFactory;
            _mapper = mapper;
            _log = logFactory.CreateLog(this);
        }

        public async Task<bool> CheckIfMessageExistsAsync(string messageGroupId, string customerId)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                return await context.NotificationMessages.AnyAsync(x =>
                    x.CustomerId == customerId && x.MessageGroupId == messageGroupId);
            }
        }

        public async Task CreateAsync(string customerId, string messageGroupId, string customPayload,
            string encryptedMessage)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                var entity = new NotificationMessage
                {
                    IsRead = false,
                    MessageGroupId = messageGroupId,
                    CreationTimestamp = DateTime.UtcNow,
                    CustomerId = customerId,
                    CustomPayload = customPayload,
                    Message = encryptedMessage
                };

                context.Add(entity);

                await context.SaveChangesAsync();
            }
        }

        public async Task<PaginatedList<Domain.Contracts.NotificationMessage>> GetNotificationMessagesForCustomerAsync(
            int skip, int take, string customerId)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                var query = context.NotificationMessages.Where(x => x.CustomerId == customerId);

                var result = await query
                    .OrderByDescending(x => x.CreationTimestamp)
                    .Skip(skip)
                    .Take(take)
                    .ToArrayAsync();

                var totalCount = await query.CountAsync();

                return new PaginatedList<Domain.Contracts.NotificationMessage>()
                {
                    Data = _mapper.Map<IEnumerable<Domain.Contracts.NotificationMessage>>(result),
                    TotalCount = totalCount
                };
            }
        }

        public async Task MarkMessageAsReadAsync(string messageGroupId)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                var entity = await  context.NotificationMessages.SingleOrDefaultAsync(nm => 
                    nm.MessageGroupId == messageGroupId);

                if (entity == null)
                {
                    _log.Error(null, $"Could not find message with messageGroupId: {messageGroupId}." +
                                     " Marking as read skipped.",
                        new {messageGroupId});

                    return;
                }

                entity.IsRead = true;

                context.Update(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task MarkAllMessagesAsReadAsync(string customerId)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                var entities = await context.NotificationMessages
                    .Where(nm => nm.CustomerId == customerId)
                    .ToListAsync();

                foreach (var entity in entities)
                    entity.IsRead = true;

                context.UpdateRange(entities);
                await context.SaveChangesAsync();
            }
        }

        public async Task<int> GetUnreadMessagesCountAsync(string customerId)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                return await context.NotificationMessages
                    .CountAsync(nm => nm.CustomerId == customerId && !nm.IsRead);
            }
        }
    }
}
