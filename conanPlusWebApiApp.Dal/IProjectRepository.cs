using conanPlusWebApiApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace conanPlusWebApiApp.Dal
{
    public interface IProjectRepository : ICommonRepository<Project>
    {
        Task<List<Project>> GetProjectsByServiceId(int serviceId);
        Task<List<Project>> GetProjectsWithRelatedDataByServiceId(int serviceId);
        Task<List<Project>> GetAllProjectsWithRelatedData(); 
    }
}
