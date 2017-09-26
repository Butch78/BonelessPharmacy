using System;
using System.Threading.Tasks;

namespace BonelessPharmacyBackend
{
    /// <summary>
    /// Standard interface for defining report factories
    /// </summary>
    public interface IReportFactory
    {
        Task<string> GenerateCsv();
    }
}