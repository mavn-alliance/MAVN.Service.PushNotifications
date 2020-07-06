using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MAVN.Persistence.PostgreSQL.Legacy;
using MAVN.Service.PushNotifications.Domain;
using MAVN.Service.PushNotifications.Domain.Enums;
using MAVN.Service.PushNotifications.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.PushNotifications.MsSqlRepositories.Repositories
{
    public class PushNotificationRegistrationRepository : IPushNotificationRegistrationRepository
    {
        private readonly PostgreSQLContextFactory<DatabaseContext> _contextFactory;

        public PushNotificationRegistrationRepository(PostgreSQLContextFactory<DatabaseContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<PushTokenInsertionResult> CreateAsync(PushNotificationRegistration data)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                // This is a specific case and we do not want to return error,
                // rather we want to update all token information in DB
                // This is related to some providers reusing their tokens
                var entity = await context.PushNotificationRegistrations.FirstOrDefaultAsync(x =>
                    x.PushRegistrationToken == data.PushRegistrationToken);
                if (entity != null)
                {
                    if (entity.CustomerId != data.CustomerId)
                    {
                        entity.CustomerId = data.CustomerId;
                        entity.RegistrationDate = data.RegistrationDate;
                        context.PushNotificationRegistrations.Update(entity);
                    }
                    else
                    {
                        return PushTokenInsertionResult.Ok;
                    }
                }
                else
                {
                    await context.PushNotificationRegistrations.AddAsync(data);
                }

                await context.SaveChangesAsync();

                return PushTokenInsertionResult.Ok;
            }
        }

        public async Task<List<PushNotificationRegistration>> GetAllAsync()
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                return await context.PushNotificationRegistrations.ToListAsync();
            }
        }

        public async Task<List<PushNotificationRegistration>> GetAllForCustomerAsync(string customerId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                return await context.PushNotificationRegistrations.Where(x => x.CustomerId == customerId).ToListAsync();
            }
        }

        public async Task<bool> DeleteAsync(Guid pushRegistrationId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity =
                    await context.PushNotificationRegistrations.FirstOrDefaultAsync(x => x.Id == pushRegistrationId);

                if (entity == null)
                    return false;

                context.Remove(entity);

                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<bool> DeleteByTokenAsync(string token)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity =
                    await context.PushNotificationRegistrations.FirstOrDefaultAsync(x => x.PushRegistrationToken == token);

                if (entity == null)
                    return false;

                context.Remove(entity);

                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<bool> DeleteAllForCustomerAsync(string customerId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entities = context.PushNotificationRegistrations.Where(x => x.CustomerId == customerId);

                if (!entities.Any())
                    return false;

                context.RemoveRange(entities);

                await context.SaveChangesAsync();

                return true;
            }
        }
    }
}
