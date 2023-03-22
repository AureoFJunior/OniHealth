using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OniHealth.Domain.DTOs;
using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Models;
using OniHealth.Domain.Utils;
using OniHealth.Infra.Context;

namespace OniHealth.Infra.Repositories
{
    public class ConsultRepository : Repository<Consult>, IRepositoryConsult
    {
        public ConsultRepository(AppDbContext context) : base(context)
        {
        }

        public async override Task<Consult> GetByIdAsync(int id)
        {
            var query = _context.Set<Consult>().Where(e => e.Id == id).AsNoTracking();

            if (await query.AnyAsync())
                return await query.FirstOrDefaultAsync();

            return null;
        }

        public async override Task<IEnumerable<Consult>> GetAllAsync()
        {
            var query = _context.Set<Consult>();

            return await query.AnyAsync() ? await query.AsNoTracking().ToListAsync() : new List<Consult>();
        }

        public async Task<IEnumerable<ConsultAppointment>> GetConsultAppointments()
        {
            var query = (from ca in _context.Consult
                         join ct in _context.ConsultTime on ca.ConsultTimeId equals ct.Id
                         join ctp in _context.ConsultType on ca.ConsultTypeId equals ctp.Id
                         select new ConsultAppointment()
                         {
                             Id = ca.Id,
                             AppointmentTime = ct.AppointmentTime,
                             StartOfAppointment = ct.StartOfAppointment,
                             EndOfAppointment = ct.EndOfAppointment,
                             CustomerIsPresent = ca.CustomerIsPresent,
                             DoctorIsPresent = ca.DoctorIsPresent,
                             IsActive = ca.IsActive,
                             Title = ca.Title,
                             Type = ctp.Name,
                             TypeDetails = ctp.Details
                         }).AsNoTracking();

            return await query.AnyAsync() ? await query.AsNoTracking().ToListAsync() : new List<ConsultAppointment>();
        }

        public async Task<IEnumerable<ConsultAppointment>> GetLateConsultAppointments()
        {
            var query = (from ca in _context.Consult
                         join ct in _context.ConsultTime on
                         new { consultTimeId = ca.ConsultTimeId ?? 0, StartOfAppointment = true, CustomerIsPresent = ca.CustomerIsPresent.Value, IsActive = ca.IsActive }
                         equals
                         new { consultTimeId = ct.Id, StartOfAppointment = (ct.StartOfAppointment >= DateTime.Now && ct.EndOfAppointment == DateTime.MinValue), CustomerIsPresent = false, IsActive = true }
                         join ctp in _context.ConsultType on ca.ConsultTypeId equals ctp.Id
                         select new ConsultAppointment()
                         {
                             Id = ca.Id,
                             AppointmentTime = ct.AppointmentTime,
                             StartOfAppointment = ct.StartOfAppointment,
                             EndOfAppointment = ct.EndOfAppointment,
                             CustomerIsPresent = ca.CustomerIsPresent,
                             DoctorIsPresent = ca.DoctorIsPresent,
                             IsActive = ca.IsActive,
                             Title = ca.Title,
                             Type = ctp.Name,
                             TypeDetails = ctp.Details
                         }).AsNoTracking();

            return await query.AnyAsync() ? await query.AsNoTracking().ToListAsync() : new List<ConsultAppointment>();
        }

        public async Task<IEnumerable<ConsultAppointment>> SetLateConsultAppointments()
        {
            IEnumerable<ConsultAppointment> consultAppointments = await this.GetLateConsultAppointments();
            if (SharedFunctions.IsNotNullOrEmpty(consultAppointments) == true)
            {
                await SharedFunctions.EnqueueAsync<ConsultAppointment>(consultAppointments, "consultAppointmentsQueue");
            }

            return consultAppointments;
        }

        public async Task<IEnumerable<ConsultAppointment>> GetFromQueueLateConsultAppointments()
        {
            return await SharedFunctions.DequeueAsync<IEnumerable<ConsultAppointment>>("consultAppointmentsQueue");
        }

        public async Task<IEnumerable<ConsultAppointment>> GetCachedLateConsultAppointments()
        {
            MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
            var cacheKey = "consultAppointments";

            if (!cache.TryGetValue(cacheKey, out IEnumerable<Domain.Models.ConsultAppointment> cachedConsultAppointments))
            {
                return null;
            }
            else
            {
                return cachedConsultAppointments;
            }
        }
    }
}