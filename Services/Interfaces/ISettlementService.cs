using Settlements.DTOs;
using Settlements.Models;

namespace Settlements.Services.Interfaces
{
    public interface ISettlementService
    {
        IEnumerable<SettlementDTO> GetSettlements();
        List<Settlement> GetSettlementsForPage(int pageNumber, int pageSize);
        int GetTotalPages(int pageSize);

        SettlementDTO GetSettlementById(int id);
        bool AddSettlement(SettlementDTO settlement);
        void UpdateSettlement(SettlementDTO settlement, int Id);
        void DeleteSettlement(int id);
        Task<IEnumerable<Settlement>> FilterSettlements(string search = "");
    }
}
