using Application.DTOS;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DataSeeding
    {
        public static async Task AddDateSeeding(ApplicationDBContext context)
        {
            if (!context.Governorates.Any())
            {
                var governoratesData = File.ReadAllText("../Infrastructure/DataSeeding/Governorate.json");
                var governoratesObjects = JsonSerializer.Deserialize<List<Governorate>>(governoratesData);
                if (governoratesObjects?.Count() > 0)
                {

                    foreach (var governorate in governoratesObjects)
                    {
                        context.Set<Governorate>().Add(governorate);
                    }
                    context.SaveChanges();
                }
            }
            if (!context.Cities.Any())
            {
                var citiesData = File.ReadAllText("../Infrastructure/DataSeeding/Cities.json");
                var citiesObjects = JsonSerializer.Deserialize<List<CityDataDTO>>(citiesData);
                if (citiesObjects?.Count() > 0)
                {
                    foreach (var cityData in citiesObjects)
                    {
                        foreach (var cityName in cityData.Cities)
                        {
                            var city = new City
                            {
                                GovernorateID = cityData.GovernorateID,
                                Name = cityName
                            };
                            context.Set<City>().Add(city);
                        }
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
