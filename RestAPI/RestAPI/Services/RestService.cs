using Microsoft.EntityFrameworkCore;
using RestAPI.DTOs;
using RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Services
{
    public class RestService
    {
        private TripsDbContext tripsDbContext;

        public RestService(TripsDbContext tripsDbContext)
        {
            this.tripsDbContext = tripsDbContext;
        }

        public async Task<List<TripsDTO>> GetTrips()
        {
            List<TripsDTO> result = new List<TripsDTO>();

            await tripsDbContext.Trips.Include(x => x.ClientTrips)
                .ThenInclude(x => x.IdClientNavigation)
                .Include(y => y.CountryTrips)
                .ThenInclude(y => y.IdCountryNavigation)
                .ForEachAsync(t => result.Add(new TripsDTO()
                {
                    Name = t.Name,
                    Description = t.Description,
                    DateFrom = t.DateFrom,
                    DateTo = t.DateTo,
                    MaxPeople = t.MaxPeople,

                    Clients = t.ClientTrips.Select(c => new ClientBasicInfoDTO()
                    {
                        FirstName = c.IdClientNavigation.FirstName,
                        LastName = c.IdClientNavigation.LastName,
                    }).ToList(),

                    Countries = t.CountryTrips.Select(c => new CountryBasicInfoDTO()
                    {
                        Name = c.IdCountryNavigation.Name
                    }).ToList()
                }));

            result = result.OrderByDescending(x => x.DateFrom).ToList();

            return result;
        }

        public async Task DeleteClient(int Id)
        {
            Client client = await tripsDbContext.Clients
                .FindAsync(Id);

            tripsDbContext.Remove(client);
            await tripsDbContext.SaveChangesAsync();
        }

        public async Task AddClient(CustomerTripDTO customerTripDTO)
        {
            int Id = await tripsDbContext.Clients.Select(x => x.IdClient).MaxAsync() + 1;

            await tripsDbContext.Clients.AddAsync(new Client()
            {
                IdClient = Id,
                FirstName = customerTripDTO.FirstName,
                LastName = customerTripDTO.LastName,
                Email = customerTripDTO.Email,
                Telephone = customerTripDTO.Telephone,
                Pesel = customerTripDTO.Pesel,
                ClientTrips = new List<ClientTrip>()
            });

            await tripsDbContext.SaveChangesAsync();
        }

        public async Task AddClientToTrip(CustomerTripDTO customerTripDTO)
        {
            int ClientId = await tripsDbContext.Clients
                .Where(x => x.Pesel == customerTripDTO.Pesel)
                .Select(x => x.IdClient).FirstAsync();

            await tripsDbContext.ClientTrips.AddAsync(new ClientTrip()
            {
                IdClient = ClientId,
                IdTrip = customerTripDTO.IdTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = customerTripDTO.PaymentDate
            });

            await tripsDbContext.SaveChangesAsync();
        }

        public async Task<bool> ClientExists(int Id)
        {
            return await tripsDbContext.Clients.FindAsync(Id) != null;
        }

        public async Task<bool> ClientExists(string Pesel)
        {
            return await tripsDbContext.Clients.Where(x => x.Pesel == Pesel).AnyAsync();
        }

        public async Task<bool> ClientHasTours(int Id)
        {
            return await tripsDbContext.ClientTrips.Where(x => x.IdClient == Id).AnyAsync();
        }

        public async Task<bool> ClientSignedForTour(string Pesel, int IdTrip)
        {
            int Id = await tripsDbContext.Clients.Where(x => x.Pesel == Pesel).Select(x => x.IdClient).FirstAsync();

            return await tripsDbContext.ClientTrips.Where(x => x.IdClient == Id && x.IdTrip == IdTrip).AnyAsync();
        }

        public async Task<bool> TripExists(int IdTrip)
        {
            return await tripsDbContext.Trips.Where(x => x.IdTrip == IdTrip).AnyAsync();
        }
    }
}
