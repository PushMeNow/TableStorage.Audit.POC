using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TableStorage.Audit.BLL.Interfaces;
using TableStorage.Audit.BLL.Responses;

namespace TableStorage.Audit.POC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuditController : ControllerBase
    {
        private readonly ITableStorageService _tableStorageService;

        public AuditController(ITableStorageService tableStorageService)
        {
            _tableStorageService = tableStorageService;
        }
        
        [HttpGet]
        public Task<AuditEntryResponse[]> GetAudit()
        {
            return _tableStorageService.GetAudit();
        }
    }
}