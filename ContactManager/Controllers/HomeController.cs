using ContactManager.Entities;
using ContactManager.Models;
using ContactManager.Repositories;
using ContactManager.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly CsvReaderService _csvReader;
        private readonly ImportRecordService _importRecordService;
        private readonly RecordRepository _recordRepository;
        public HomeController(IWebHostEnvironment environment,
            ImportRecordService importRecordService,
            RecordRepository recordRepository,
            CsvReaderService csvReader)
        {
            _environment = environment;
            _csvReader = csvReader;
            _importRecordService = importRecordService;
            _recordRepository = recordRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var records = _recordRepository.GetAll();
            var modelRecords = records.Select(_ => _.ToRecordModel());

            return View(modelRecords);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _recordRepository.RemoveById(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(int id, RecordModel recordModel)
        {
            var record = Record.ToRecord(recordModel);
            record.Id = recordModel.Id;
            _recordRepository.Update(record);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var record = _recordRepository.GetById(id);

            return View(record.ToRecordModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile postedFile)
        {
            if (postedFile != null)
            {
                string filePath = SaveFileLocaly(postedFile);
                var csvFileRows = _csvReader.ReadRows(filePath, skipRows: 1);
                var records = csvFileRows.Select(_ => RecordModel.ToRecordModel(_)).ToList();
                await _importRecordService.ImportData(records);
            }

            return RedirectToAction("Index");
        }

        private string SaveFileLocaly(IFormFile postedFile)
        {
            string path = Path.Combine(_environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = Path.GetFileName(postedFile.FileName);
            string filePath = Path.Combine(path, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                postedFile.CopyTo(stream);
            }

            return filePath;
        }
    }
}
