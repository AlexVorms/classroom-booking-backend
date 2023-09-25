

using Microsoft.EntityFrameworkCore;
using classroom_booking_backend.DAL;
using classroom_booking_backend.DataTransferModel;

namespace classroom_booking_backend.Services
{
    public interface IBuildingService
    {
        Task<Boolean> AddBuildingsInDB(List<BuildingDto> results);
        Task<List<BuildingDto>> GetBuildingList();
        Task<Boolean> AddAudiences(string id, List<AudiencesDto> results);
        Task<List<AudiencesDto>> GetAudiencesList(string id);
    }
    public class BuildingsService: IBuildingService
    {
        private readonly ApplicationDbContext _context;
        public BuildingsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Boolean> AddBuildingsInDB(List<BuildingDto> results)
        {
            var BuildingEntity = await _context
                .Building
                .FirstOrDefaultAsync();
            if (BuildingEntity != null)
            {
                return false;
            }
            else
            {
                foreach(var i in results)
                {

                    await _context.Building.AddAsync(new DAL.Entities.BuildingEntity
                    {
                        Id = i.Id,
                        Name = i.Name
                    });
                    await _context.SaveChangesAsync();

                }
                return true;
            }

        }

        public async Task<List<BuildingDto>> GetBuildingList()
        {
            var buildings = await _context
                .Building.ToListAsync();

            var listBuilding = new List<BuildingDto>();

            foreach(var i in buildings)
            {
                var buildingDto = new BuildingDto
                {
                    Id = i.Id,
                    Name = i.Name
                };
                listBuilding.Add(buildingDto);
            }
            return listBuilding;
        }

        public async Task<Boolean> AddAudiences(string id, List<AudiencesDto> results)
        {
            var AudiencesEntity = await _context
                .Audiences
                .Where(a => a.BuildingId == id)
                .ToListAsync();

            if(AudiencesEntity.Count != 0)
            {

            }
            else
            {
                foreach(var i in results)
                {
                    await _context.Audiences.AddAsync(new DAL.Entities.AudiencesEntity
                    {
                        Id = i.Id,
                        Name = i.Name,
                        BuildingId = id
                    });
                    await _context.SaveChangesAsync();
                }
            }
            return true;
        }

        public async Task<List<AudiencesDto>> GetAudiencesList(string id)
        {
            var audiences = await _context
                .Audiences
                .Where(a => a.BuildingId == id)
                .ToListAsync();

            var listAudiences = new List<AudiencesDto>();

            foreach (var i in audiences)
            {
                var audiencesDto = new AudiencesDto
                {
                    Id = i.Id,
                    Name = i.Name
                };
                listAudiences.Add(audiencesDto);
            }
            return listAudiences;
        }
    }
}
