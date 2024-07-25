using Settlements.DTOs;
using Settlements.Models;
using Settlements.Services.Interfaces;
using Settlements.Repositories;
using System.Linq;

namespace Settlements.Services;

    public class SettlementService : ISettlementService
    {
        private readonly ISettlementRepository _settlementRepository;

        public SettlementService(ISettlementRepository settlementRepository)
        {
            _settlementRepository = settlementRepository;
        }

        public IEnumerable<SettlementDTO> GetSettlements()
        {
            return _settlementRepository.GetSettlements().Select(s => new SettlementDTO
            {
                Id = s.Id,
                Name = s.Name
            });
        }

        public SettlementDTO GetSettlementById(int id)
        {
            var settlement = _settlementRepository.GetSettlementById(id);
            if (settlement == null)
            {
                return null;
            }
            return new SettlementDTO
            {
                Id = settlement.Id,
                Name = settlement.Name
            };
        }

        public  bool AddSettlement(SettlementDTO settlementDTO)
        {
            var settlement = new Settlement
            {
                Name = settlementDTO.Name
            };

            if(_settlementRepository.SettlementExists(settlement.Name))
            {
                return false;
            }
            _settlementRepository.AddSettlement(settlement);
            return true;
        }

        public void UpdateSettlement(SettlementDTO settlementDTO , int Id)
        {
            var settlement = new Settlement
            {
                Id = Id,
                Name = settlementDTO.Name
            };
            _settlementRepository.UpdateSettlement(settlement);
        }

        public void DeleteSettlement(int id)
        {
            _settlementRepository.DeleteSettlement(id);
        }
        public async Task<IEnumerable<Settlement>> FilterSettlements(string search="")
        {
            return await _settlementRepository.FilterSettlements(search);
        }


    public List<Settlement> GetSettlementsForPage(int pageNumber, int pageSize)
    {
        var allSettlements = _settlementRepository.GetSettlements();
        int skip = (pageNumber - 1) * pageSize;
        return allSettlements.Skip(skip).Take(pageSize).ToList();
    }

    public int GetTotalPages(int pageSize)
    {
        List<Settlement> totalSettlements = _settlementRepository.GetSettlements().ToList();
        var Count = totalSettlements.Count();
        return (int)Math.Ceiling((double)Count / pageSize);


    }
}
