using Settlements.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

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

        public bool SettlementExists(string name)
        {
            string pattern = $"%{name}%";
            return _context.Settlements
                           .Any(s => EF.Functions.Like(s.Name, pattern));
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

        public async Task<IEnumerable<Settlement>> FilterSettlements(string search = "")
        {
            var query = _context.Settlements.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                string transliteratedSearch = Transliterate(search);
                query = query.Where(s => s.Name.Contains(search) || s.Name.Contains(transliteratedSearch));
            }

            return await query.ToListAsync();
        }

         private string Transliterate(string input)
        {
            var transliterationMap = new Dictionary<string, string>
            {
               { "a", "ש" }, { "b", "נ" }, { "c", "ב" }, { "d", "ג" }, { "e", "ק" },
                { "f", "כ" }, { "g", "ע" }, { "h", "י" }, { "i", "ן" }, { "j", "ח" },
                { "k", "ל" }, { "l", "ך" }, { "m", "צ" }, { "n", "מ" }, { "o", "ם" },
                { "p", "פ" }, { "q", "/" }, { "r", "ר" }, { "s", "ד" }, { "t", "א" },
                { "u", "ו" }, { "v", "ה" }, { "w", "'" }, { "x", "ס" }, { "y", "ט" },
                { "z", "ז" },
                { "A", "ש" }, { "B", "נ" }, { "C", "ב" }, { "D", "ג" }, { "E", "ק" },
                { "F", "כ" }, { "G", "ע" }, { "H", "י" }, { "I", "ן" }, { "J", "ח" },
                { "K", "ל" }, { "L", "ך" }, { "M", "צ" }, { "N", "מ" }, { "O", "ם" },
                { "P", "פ" }, { "Q", "/" }, { "R", "ר" }, { "S", "ד" }, { "T", "א" },
                { "U", "ו" }, { "V", "ה" }, { "W", "'" }, { "X", "ס" }, { "Y", "ט" },
                { "Z", "ז" },{ ",","ת" }
            };

           var sb = new StringBuilder();

            foreach (var ch in input)
            {
                string mappedChar;
                if (transliterationMap.TryGetValue(ch.ToString(), out mappedChar))
                {
                    sb.Append(mappedChar);
                }
                else
                {
                    sb.Append(ch); 
                }
            }

            return sb.ToString();
        }
    }
}
