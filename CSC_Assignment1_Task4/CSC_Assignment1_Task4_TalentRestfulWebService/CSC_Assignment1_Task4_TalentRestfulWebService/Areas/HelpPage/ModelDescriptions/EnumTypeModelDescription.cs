using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CSC_Assignment1_Task4_TalentRestfulWebService.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}