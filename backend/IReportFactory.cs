using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// Standard interface for defining report factories
    /// </summary>
    public interface IReportFactory
    {
        Task<string> GenerateCsv();

        Task<string> GenerateJson();
    }
}