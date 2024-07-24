using Settlements.DTOs;
using Settlements.Models;

namespace Settlements.Services.Interfaces
{
    public interface ISettlementService
    {
        IEnumerable<SettlementDTO> GetSettlements();
        SettlementDTO GetSettlementById(int id);
        void AddSettlement(SettlementDTO settlement);
        void UpdateSettlement(SettlementDTO settlement,int Id);
        void DeleteSettlement(int id);
    }
}
