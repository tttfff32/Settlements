using Settlements.Models;

namespace Settlements.Repositories
{
    public interface ISettlementRepository
    {
        Settlement GetSettlementById(int id);
        IEnumerable<Settlement> GetSettlements();
        void AddSettlement(Settlement settlement);
        void UpdateSettlement(Settlement settlement);
        void DeleteSettlement(int id);
    }
}
