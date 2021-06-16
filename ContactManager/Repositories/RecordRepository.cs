using ContactManager.Entities;
using ContactManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Repositories
{
    public class RecordRepository
    {
        private ApplicationContext _dbContext;
        public RecordRepository(ApplicationContext context)
        {
            _dbContext = context;
        }

        public List<Record> GetAll()
        {
            return _dbContext.Records.ToList();
        }

        public async Task AddRecords(List<Record> records)
        {
            await _dbContext.Records.AddRangeAsync(records);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveById(int id)
        {
            var record = _dbContext.Records.FirstOrDefault(_ => _.Id == id);
            _dbContext.Records.Remove(record);
            await _dbContext.SaveChangesAsync();
        }

        public Record GetById(int id)
        {
            return _dbContext.Records.FirstOrDefault(_ => _.Id == id);
        }

        public void Update(Record record)
        {
            _dbContext.Records.Update(record);
            _dbContext.SaveChanges();
        }
    }
}
