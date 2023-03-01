using OniHealth.Domain.Models;
using System.Collections.Generic;

namespace OniHealth.Domain.Interfaces.Repositories
{
    public interface IRepositoryConsult : IRepository<Consult>
    {

        #region Sync

        #endregion

        #region Async
        Task<IEnumerable<Consult>> GetAllAsync();
        Task<Consult> GetByIdAsync(int id);
        Task<IEnumerable<ConsultAppointment>> GetCachedLateConsultAppointments();
        Task<IEnumerable<ConsultAppointment>> GetConsultAppointments();
        Task<IEnumerable<ConsultAppointment>> GetFromQueueLateConsultAppointments();
        Task<IEnumerable<ConsultAppointment>> GetLateConsultAppointments();
        Task<IEnumerable<ConsultAppointment>> SetLateConsultAppointments();
        #endregion
    }
}