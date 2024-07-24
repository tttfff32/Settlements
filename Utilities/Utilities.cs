using Settlements.Repositories;
using Settlements.Services;
using Settlements.Services.Interfaces;

namespace settlements.Utilities;
  public static class Utilities
    {
        public static void AddSettlementsUtilities(this IServiceCollection services)
        {
        services.AddScoped<ISettlementService, SettlementService>();
        services.AddScoped<ISettlementRepository,SettlementRepository>();
        }
    }