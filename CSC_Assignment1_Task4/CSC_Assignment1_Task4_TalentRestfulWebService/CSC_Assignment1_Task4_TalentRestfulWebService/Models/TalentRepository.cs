using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSC_Assignment1_Task4_TalentRestfulWebService.Models
{
    public class TalentRepository
    {
        private List<Talent> talents = new List<Talent>();
        private int _nextId = 1;
        
        public TalentRepository()
        {
            talents.Add(new Talent
            {
                Id = 1,
                Name = "Barot Bellingham",
                ShortName = "Barot_Bellingham",
                Reknown = "Royal Academy of Painting and Sculpture",
                Bio = "The Artist to Watch in 2012 by the London Review, Johnathan has already sold one of the highest priced-commissions paid to an art student, ever on record. The piece, entitled Gratitude Resort, a work in oil and mixed media, was sold for $750,000 and Jonathan donated all the proceeds to Art for Peace, an organization that provides college art scholarships for creative children in developing nations."
            });

            talents.Add(new Talent
            {
                Id = 2,
                Name = "Hillary Hewitt Goldwynn-Post",
                ShortName = "Hillary_Goldwynn",
                Reknown = "New York University",
                Bio = "Hillary is a sophomore art sculpture student at New York University, and has already won all the major international prizes for new sculptors, including the Divinity Circle, the International Sculptor's Medal, and the Academy of Paris Award. Hillary's CAC exhibit features 25 abstract watercolor paintings that contain only water images including waves, deep sea, and river."
            });

            talents.Add(new Talent
            {
                Id = 3,
                Name = "Hassum Harrod",
                ShortName = "Hassum_Harrod",
                Reknown = "Art College in New Delhi",
                Bio = "The Art College in New Delhi has sponsored Hassum on scholarship for his entire undergraduate career at the university, seeing great promise in his contemporary paintings of landscapes - that use equal parts muted and vibrant tones, and are almost a contradiction in art. Hassum will be speaking on \"The use and absence of color in modern art\" during Thursday's agenda."
            });
        }

        //get all talents
        public IEnumerable<Talent> GetAll()
        {
            return talents;
        }

        //get talent by id
        public Talent Get(int id)
        {
            return talents.Find(p => p.Id == id);
        }

        //add new talent
        public Talent Add(Talent item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            item.Id = _nextId++;
            talents.Add(item);
            return item;
        }

        //delete existing talent by id
        public void Remove(int id)
        {
            talents.RemoveAll(p => p.Id == id);
        }

        //update existing talent
        public bool Update(Talent item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            int index = talents.FindIndex(p => p.Id == item.id);
            
            if (index == -1)
            {
                return false;
            }

            talents.RemoveAt(index);
            talents.Add(item);
            return true;
        }
    }
} //end of the controller class