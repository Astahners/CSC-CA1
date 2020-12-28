using CSC_Assignment1_Task4_TalentRestfulWebService.Models;
using System.Collections.Generic;

namespace CSC_Assignment1_Task4_TalentRestfulWebService.Interfaces
{
    public interface ITalentRepository
    {
        Talent Add(Talent item);
        Talent Get(int id);
        IEnumerable<Talent> GetAll();
        void Remove(int id);
        bool Update(Talent item);
    }
}