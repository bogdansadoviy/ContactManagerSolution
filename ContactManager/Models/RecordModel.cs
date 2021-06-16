using System;

namespace ContactManager.Models
{
    public class RecordModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Married { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }

        public static RecordModel ToRecordModel(string[] data)
        {
            return new RecordModel()
            {
                Name = data[0],
                DateOfBirth = DateTime.Parse(data[1]),
                Married = bool.Parse(data[2]),
                Phone = data[3],
                Salary = decimal.Parse(data[4])
            };
        }
    }
}
