using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Models
{
    public interface IOrganizerRepository
    {
        IEnumerable<Organizer> GetOrganizer();
        Organizer GetOrganizerById(int organizerId);
        bool AddOrganizer(Organizer organizer);
        bool DeleteOrganizerById(int organizerId);
        bool UpdateOrganizer(Organizer organizer);
    }
}
