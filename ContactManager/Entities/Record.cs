using ContactManager.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactManager.Entities
{
    [Table("Record")]
    public class Record
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Married { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }

        public static Record ToRecord(RecordModel recordmodel) 
        {
            return new Record
            {
                Name = recordmodel.Name,
                DateOfBirth = recordmodel.DateOfBirth,
                Married = recordmodel.Married,
                Phone = recordmodel.Phone,
                Salary = recordmodel.Salary
            };
        }

        public RecordModel ToRecordModel()
        {
            return new RecordModel
            {
                Id = Id,
                Name = Name,
                DateOfBirth = DateOfBirth,
                Married = Married,
                Phone = Phone,
                Salary = Salary
            };
        }
    }
}
