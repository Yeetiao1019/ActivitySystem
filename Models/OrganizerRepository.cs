using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Models
{
    public class OrganizerRepository : IOrganizerRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrganizerRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public bool AddOrganizer(Organizer organizer)
        {
            _appDbContext.Organizers.Add(organizer);
            var count = _appDbContext.SaveChanges();

            return count > 0;
        }

        public bool DeleteOrganizerById(int organizerId)
        {
            var organizer = _appDbContext.Organizers.FirstOrDefault(o => o.OrganizerId == organizerId);
            if(organizer == null)
            {
                return false;
            }
            _appDbContext.Organizers.Remove(organizer);
            var count = _appDbContext.SaveChanges();

            return count > 0;
        }

        public IEnumerable<Organizer> GetOrganizer()
        {
            return _appDbContext.Organizers.ToList();
        }

        public Organizer GetOrganizerById(int organizerId)
        {
            return _appDbContext.Organizers.FirstOrDefault(o => o.OrganizerId == organizerId);
        }

        public bool UpdateOrganizer(Organizer organizer)
        {
            _appDbContext.Organizers.Update(organizer);
            var count = _appDbContext.SaveChanges();

            return count > 0;
        }
    }
}
