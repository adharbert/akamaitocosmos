using Domain;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{


    public delegate void WriteLine(string text = "", bool highlight = false, bool isException = false);

    public class MigrateDataService : IMigrateDataService
    {

        private readonly IRepository _sourceRepository;
        private readonly ICosmosRepository _destRepository;
        private readonly WriteLine _writeLine;


        public MigrateDataService(IRepository sourceRepository, ICosmosRepository destRepository, WriteLine writeLine)
        {
            _sourceRepository = sourceRepository;
            _destRepository = destRepository;
            _writeLine = writeLine;
        }
        //  https://exceptionnotfound.net/the-repository-service-pattern-with-dependency-injection-and-asp-net-core/

        public async Task<TransactionResultDto> Transfer()
        {
            TransactionResultDto tr = new TransactionResultDto();
            DateTime startAll = DateTime.Now;

            _writeLine();
            _writeLine("Getting Source..."); 
            var sourceCount = await _sourceRepository.CountAsync(); // ****** Getting count
            tr.SourceRecordCount = sourceCount;
            _writeLine($"Source Count: {sourceCount}");

            var sourceRecords = await _sourceRepository.GetAllUsersAsync("0", sourceCount); // ****** Getting records
            DateTime endPull = DateTime.Now;
            TimeSpan pullTs = startAll.Subtract(endPull);



            DateTime startInsert = DateTime.Now;
            try
            {
                _writeLine();
                _writeLine("Adding items...");
                await _destRepository.SaveAsync(sourceRecords.ToList());                
            }
            catch (Exception ex)
            {
                _writeLine(ex.Message, isException: true);
            }
            DateTime endtInsert = DateTime.Now;
            TimeSpan insertTs = startInsert.Subtract(endtInsert);

            DateTime endAll = DateTime.Now;
            TimeSpan allTs = startAll.Subtract(endAll);


            // Display times spees ************************************
            _writeLine("******  DISPLAY TIMES ******");
            Console.WriteLine("Pull Dev time: {0:hh\\:mm\\:ss}  -- {1} Records", pullTs, sourceCount);
            Console.WriteLine("Insert times:    {0:hh\\:mm\\:ss}", insertTs);
            Console.WriteLine("Overall times: {0:hh\\:mm\\:ss}", allTs);
            _writeLine("****** END DISPLAY TIMES ******");


            return tr;

        }
    }
}
