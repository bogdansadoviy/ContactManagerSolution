using ContactManager.Entities;
using ContactManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Repositories
{
    public class RecordRepository
    {
        private ApplicationContext db;
        public RecordRepository(ApplicationContext context)
        {
            db = context;
        }

        public List<Record> GetAll()
        {
            return db.Records.ToList();
        }

        public async Task AddRecords(List<Record> records)
        {
            await db.Records.AddRangeAsync(records);
            await db.SaveChangesAsync();
        }
        public async Task RemoveById(int id)
        {
            var record = db.Records.FirstOrDefault(_ => _.Id == id);
            db.Records.Remove(record);
            await db.SaveChangesAsync();
        }

        public Record GetById(int id)
        {
            return db.Records.FirstOrDefault(_ => _.Id == id);
        }

        public void Update(Record record)
        {
            db.Records.Update(record);
            db.SaveChanges();
        }
    }
}
