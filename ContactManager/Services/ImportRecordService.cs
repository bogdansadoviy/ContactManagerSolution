using ContactManager.Entities;
using ContactManager.Models;
using ContactManager.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Services
{
    public class ImportRecordService
    {
        private readonly RecordRepository _recordRepository;
        public ImportRecordService(RecordRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }
        public async Task ImportData(List<RecordModel> recordModels)
        {
            var records = recordModels.Select(_ => Record.ToRecord(_)).ToList();
            await _recordRepository.AddRecords(records);
        }
    }
}
