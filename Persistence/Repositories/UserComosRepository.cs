using Domain.Models;
using Domain.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{

    public class UserComosRepository : ICosmosRepository
    {
        
        private readonly IDbContextFactory<AkamaiContext> _contextFactory;

        private readonly int _groupCount = 200;
        static SemaphoreSlim _sem = new SemaphoreSlim(10);

        ConcurrentBag<string> ids = new ConcurrentBag<string>();

        public UserComosRepository(IDbContextFactory<AkamaiContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task SaveAsync(List<AkamaiUser> users)
        {

            await RecreateDatabase();

            var distinctList = users.GroupBy(b => b.Id).Select(x => x.First()).ToList();

            var newList = CollectionHelper.BreakIntoChunks<AkamaiUser>(distinctList, _groupCount);
            int i = 0;
            var listOfTasks = new List<Task>();

            try
            {
                foreach (var list in newList)
                {
                    listOfTasks.Add(Task.Run(() => LoadUsers(list, i++)));
                }
                Task.WaitAll(listOfTasks.ToArray());
            }
            catch (Exception)
            {
                throw;
            }

            Console.WriteLine($"Total chunks created {i}");

        }


        private async Task LoadUsers(List<AkamaiUser> userList, int counter) {

            await _sem.WaitAsync();
            int subCount = 0;
            foreach (var item in userList)
            {
                subCount++;
                item.Id = counter.ToString().PadLeft(7, '0') + subCount.ToString().PadLeft(3, '0');
                //ids.Add(item.Id);
            }

            try {
                using var context = await _contextFactory.CreateDbContextAsync();
                foreach (var user in userList) {
                    //var idCount = ids.Where(x => x == user.Id).Select(x => x).ToList();
                    //if (idCount.Count == 1)
                    context.Add(user);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex) {
                throw new Exception($"Error thrown in LoadUsers on counter {counter}.", ex);
            } finally {
                _sem.Release();
            }

        }

        private async Task RecreateDatabase()
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }

    }
}
