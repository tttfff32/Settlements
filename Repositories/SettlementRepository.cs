using Settlements.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Settlements.Repositories;

namespace Settlements.Repositories
{
    public class SettlementRepository : ISettlementRepository
    {
        private readonly SettlementsDbContext _context;

        public SettlementRepository(SettlementsDbContext context)
        {
            _context = context;
        }

        public Settlement GetSettlementById(int id)
        {
            return _context.Settlements.Find(id);
        }

        public IEnumerable<Settlement> GetSettlements()
        {
            return _context.Settlements.ToList();
        }

        public void AddSettlement(Settlement settlement)
        {
            _context.Settlements.Add(settlement);
            _context.SaveChanges();
        }

        public void UpdateSettlement(Settlement settlement)
        {
            var existingSettlement = _context.Settlements.Find(settlement.Id);
            if (existingSettlement != null)
            {
                _context.Entry(existingSettlement).CurrentValues.SetValues(settlement);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("The settlement to be updated does not exist.");
            }
        }


        public void DeleteSettlement(int id)
        {
            var settlement = _context.Settlements.Find(id);
            if (settlement != null)
            {
                _context.Settlements.Remove(settlement);
                _context.SaveChanges();
            }
        }
    }
}
