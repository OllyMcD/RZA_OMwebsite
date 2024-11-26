using RZA_OMwebsite.Models; // Ensure you have a model for your customers
using RZA_OMwebsite.Utilities; // For password utilities
using Microsoft.EntityFrameworkCore;

namespace RZA_OMwebsite.Pages
{
    public class PageService
    {
        private readonly TlS2303831RzaContext _context;

        public PageService(TlS2303831RzaContext context)
        {
            _context = context;
        }

        // Method to track page views
        public async Task TrackPageViewAsync(string pageUrl)
        {
            Console.WriteLine(pageUrl); 
            var pageStats = await _context.Stats
                .SingleOrDefaultAsync(ps => ps.PageUrl == pageUrl);

            if (pageStats == null)
            {
                Console.WriteLine("New Page Entry");
            }
            else
            {
                // If the page exists, increment the Page_views by 1
                pageStats.PageViews++;
            }

            await _context.SaveChangesAsync();
        }

        // Get the total active user count from the database
        public async Task<int> GetActiveUserCountAsync()
        {
            // Query to get the total number of customers (active users)
            return await _context.Customers.CountAsync();
        }

        // Get the total page views from the 'Stats' table
        public async Task<int> GetPageViewsCountAsync()
        {
            // Sum up all PageViews in the Stats table
            return await _context.Stats.SumAsync(s => s.PageViews);
        }

        // Randomly generate new users count (kept as is)
        public Task<int> GetNewUserCountAsync()
        {
            return Task.FromResult(new Random().Next(5, 100)); // Generates a number between 5 and 100
        }

        public async Task Dispose()
        {
            await _context.SaveChangesAsync();
        }

    }
}
