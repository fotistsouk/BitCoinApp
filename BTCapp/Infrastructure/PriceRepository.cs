using Microsoft.EntityFrameworkCore;
using BTCapp.Domain;
namespace BTCapp.Infrastructure
{
    public class PriceRepository : IPriceRepository
    {
        private readonly BTCDBContext _context;

        public PriceRepository(BTCDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }
        public async Task AddPriceAsync(Price price)
        {
            await _context.Set<Price>().AddAsync(price);
            await _context.SaveChangesAsync();
        }
        public async Task AddPricesAsync(IEnumerable<Price> prices, IEnumerable<Price> oldprices)
        {

            if (oldprices.Count() == 0)
            {
                await _context.Prices.AddRangeAsync(prices);
                await _context.SaveChangesAsync();
            }
            // Create a HashSet of existing TimePoints for faster lookup
            var existingTimePoints = new HashSet<DateTime>(oldprices.Select(p => p.TimePoint));

            // Filter out the prices that already exist in the database
            var newPrices = prices.Where(price => !existingTimePoints.Contains(price.TimePoint)).ToList();

            // Add only the new prices
            if (newPrices.Any())
            {
                await _context.Prices.AddRangeAsync(newPrices);
                await _context.SaveChangesAsync();
            }
        }

        
        public async Task<Price?> GetPriceByTimepointAsync(DateTime time)
        {
            return await _context.Prices.Where(p => p.TimePoint == time)
                                 .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Price>> GetPriceRangeAsync(DateTime start, DateTime end)
        {
            return await _context.Prices
                                 .Where(p => p.TimePoint >= start && p.TimePoint <= end)
                                 .OrderBy(p => p.TimePoint)
                                 .ToListAsync();
        }
    }

}
