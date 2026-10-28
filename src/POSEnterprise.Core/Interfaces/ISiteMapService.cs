using System.Threading.Tasks;

namespace PosEnterprise.Core.Interfaces;

public interface ISitemapService
{
    Task<string> GenerateSitemapXmlAsync(string baseUrl);
}